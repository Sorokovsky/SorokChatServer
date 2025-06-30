using System.Linq.Expressions;
using AutoMapper;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using SorokChatServer.Database.Entities;
using SorokChatServer.Infrastructure.Interfaces;
using SorokChatServer.Infrastructure.Models;

namespace SorokChatServer.Database.Repositories;

public abstract class BaseRepository<TM, TE> : IRepository<TM> where TM : Base where TE : BaseEntity
{
    private readonly DatabaseContext _context;
    private readonly DbSet<TE> _items;
    private readonly IMapper _mapper;

    protected BaseRepository(DatabaseContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
        _items = _context.Set<TE>();
    }

    public async Task<Result<TM>> CreateAsync(TM item, CancellationToken cancellationToken)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            var entity = _mapper.Map<TE>(item);
            var created = (await _items.AddAsync(entity, cancellationToken)).Entity;
            var createdModel = _mapper.Map<TM>(created);
            await _context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
            return Result.Success(createdModel);
        }
        catch (Exception exception)
        {
            await transaction.RollbackAsync(cancellationToken);
            return Result.Failure<TM>(exception.Message);
        }
    }

    public async Task<Result<TM>> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await GetByAsync(entity => entity.Id == id, cancellationToken);
    }

    public Task<Result<TM>> UpdateAsync(long id, TM item, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Result> DeleteAsync(long id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    private async Task<Result<TM>> GetByAsync(Expression<Func<TE, bool>> predicate, CancellationToken cancellationToken)
    {
        var candidate = await _items
            .AsNoTracking()
            .FirstOrDefaultAsync(predicate, cancellationToken);
        if (candidate is null) return Result.Failure<TM>($"{nameof(TM)} not found.");
        return Result.Success(_mapper.Map<TM>(candidate));
    }
}