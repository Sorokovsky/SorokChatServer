using System.Linq.Expressions;
using AutoMapper;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using SorokChatServer.Infrastructure.Entities;
using SorokChatServer.Infrastructure.Interfaces;

namespace SorokChatServer.Database.Repositories;

public abstract class BaseRepository<T> : IRepository<T> where T : BaseEntity
{
    private readonly DatabaseContext _context;
    private readonly DbSet<T> _items;
    private readonly IMapper _mapper;

    protected BaseRepository(DatabaseContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
        _items = _context.Set<T>();
    }

    public async Task<Result<T>> CreateAsync(T item, CancellationToken cancellationToken)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            var created = (await _items.AddAsync(item, cancellationToken)).Entity;
            await _context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
            return Result.Success(created);
        }
        catch (Exception exception)
        {
            await transaction.RollbackAsync(cancellationToken);
            return Result.Failure<T>(exception.Message);
        }
    }

    public async Task<Result<T>> GetByIdAsync(long id, CancellationToken cancellationToken)
    {
        return await GetByAsync(entity => entity.Id == id, cancellationToken);
    }

    public async Task<Result<T>> GetByAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
    {
        var candidate = await _items
            .AsNoTracking()
            .FirstOrDefaultAsync(predicate, cancellationToken);
        if (candidate is null) return Result.Failure<T>($"{nameof(T)} not found.");
        return Result.Success(candidate);
    }

    public async Task<List<T>> GetManyAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
    {
        return await _items.AsNoTracking().Where(predicate).ToListAsync(cancellationToken);
    }

    public async Task<Result<T>> UpdateAsync(long id, T item, CancellationToken cancellationToken)
    {
        var candidateResult = await GetByIdAsync(id, cancellationToken);
        if (candidateResult.IsFailure) return candidateResult;
        var entity = candidateResult.Value!;
        var updated = _mapper.Map(item, entity);
        if (updated is null) return Result.Failure<T>("Something went wrong.");
        await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            var saved = _items.Update(updated).Entity;
            await _context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
            return Result.Success(saved);
        }
        catch (Exception exception)
        {
            await transaction.RollbackAsync(cancellationToken);
            return Result.Failure<T>(exception.Message);
        }
    }

    public async Task<Result<T>> DeleteAsync(long id, CancellationToken cancellationToken)
    {
        var candidateResult = await GetByIdAsync(id, cancellationToken);
        if (candidateResult.IsFailure) return candidateResult;
        await _items.Where(x => x.Id == id).ExecuteDeleteAsync(cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return candidateResult;
    }
}