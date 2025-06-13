using System.Linq.Expressions;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using SorokChatServer.Application.Database.Entities;
using SorokChatServer.Application.Interfaces;

namespace SorokChatServer.Application.Database.Repositories;

public abstract class BaseRepository<T> : IRepository<T> where T : BaseEntity
{
    private readonly SorokChatDatabaseContext _context;
    private readonly DbSet<T> _set;

    protected BaseRepository(SorokChatDatabaseContext context)
    {
        _context = context;
        _set = _context.Set<T>();
    }

    public async Task<List<T>> GetMany(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
    {
        return await _set.AsNoTracking().Where(predicate).ToListAsync(cancellationToken);
    }

    public async Task<Result<T, string>> GetOne(Expression<Func<T, bool>> predicate,
        CancellationToken cancellationToken)
    {
        var candidate = await _set.AsNoTracking().FirstOrDefaultAsync(predicate, cancellationToken);
        var errorResult = Result.Failure<T, string>($"{typeof(T).Name} not found.");
        var successResult = Result.Success<T, string>(candidate!);
        return candidate is null ? errorResult : successResult;
    }

    public async Task<Result<T, string>> Create(T item, CancellationToken cancellationToken)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            var created = (await _set.AddAsync(item, cancellationToken)).Entity;
            await _context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
            return Result.Success<T, string>(created);
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync(cancellationToken);
            return Result.Failure<T, string>(e.Message);
        }
    }

    public async Task<Result<T, string>> Update(Expression<Func<T, bool>> predicate, T updated,
        CancellationToken cancellationToken)
    {
        var candidateResult = await GetOne(predicate, cancellationToken);
        if (candidateResult.IsFailure) return candidateResult;
        var mergedEntity = Merge(candidateResult.Value, updated);
        var entity = _set.Update(mergedEntity).Entity;
        await _context.SaveChangesAsync(cancellationToken);
        return Result.Success<T, string>(entity);
    }

    public async Task<Result<T, string>> Delete(Expression<Func<T, bool>> predicate,
        CancellationToken cancellationToken)
    {
        var candidateResult = await GetOne(predicate, cancellationToken);
        if (candidateResult.IsFailure) return candidateResult;
        await _set.Where(predicate).ExecuteDeleteAsync(cancellationToken);
        return Result.Success<T, string>(candidateResult.Value);
    }

    private static T Merge(T oldItem, T newItem)
    {
        var baseFieldsName = typeof(BaseEntity).GetProperties().Select(x => x.Name).ToHashSet();
        var fields = typeof(T).GetProperties().ToList();
        foreach (var field in fields)
        {
            if (baseFieldsName.Contains(field.Name)) continue;
            var oldValue = field.GetValue(oldItem);
            var newValue = field.GetValue(newItem);
            var currentValue = newValue ?? oldValue;
            field.SetValue(oldItem, currentValue);
        }

        oldItem.UpdatedAt = DateTime.UtcNow;
        return oldItem;
    }
}