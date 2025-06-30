using System.Linq.Expressions;
using CSharpFunctionalExtensions;
using SorokChatServer.Infrastructure.Entities;

namespace SorokChatServer.Infrastructure.Interfaces;

public interface IRepository<T> where T : BaseEntity
{
    public Task<Result<T>> CreateAsync(T item, CancellationToken cancellationToken);

    public Task<Result<T>> GetByIdAsync(long id, CancellationToken cancellationToken);

    public Task<Result<T>> GetByAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);

    public Task<List<T>> GetManyAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);

    public Task<Result<T>> UpdateAsync(long id, T item, CancellationToken cancellationToken);

    public Task<Result<T>> DeleteAsync(long id, CancellationToken cancellationToken);
}