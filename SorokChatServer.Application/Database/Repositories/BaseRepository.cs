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
        throw new NotImplementedException();
    }

    public abstract Task<Result<T, string>> Delete(Expression<Func<T, bool>> predicate,
        CancellationToken cancellationToken);
}