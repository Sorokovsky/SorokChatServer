using System.Linq.Expressions;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using SorokChatServer.Application.Interfaces;
using SorokChatServer.Application.Models;

namespace SorokChatServer.Application.Database.Repositories;

public abstract class BaseRepository<T> : IRepository<T> where T : Base
{
    private readonly SorokChatDatabaseContext _context;
    private readonly DbSet<T> _set;

    protected BaseRepository(SorokChatDatabaseContext context)
    {
        _context = context;
        _set = _context.Set<T>();
    }

    public async Task<List<T>> GetMany(Expression<Func<T, bool>> predicate)
    {
        return await _set.AsNoTracking().Where(predicate).ToListAsync();
    }

    public async Task<Result<T, string>> FindOne(Expression<Func<T, bool>> predicate)
    {
        var candidate = await _set.AsNoTracking().FirstOrDefaultAsync(predicate);
        var errorResult = Result.Failure<T, string>($"{typeof(T).Name} not found.");
        var successResult = Result.Success<T, string>(candidate!);
        return candidate is null ? errorResult : successResult;
    }

    public abstract Task<Result<T, string>> Create(T item);

    public abstract Task<Result<T, string>> Update(Expression<Func<T, bool>> predicate, T updated);

    public abstract Task<Result<T, string>> Delete(Expression<Func<T, bool>> predicate);
}