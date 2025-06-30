using System.Linq.Expressions;
using CSharpFunctionalExtensions;
using SorokChatServer.Infrastructure.Models;

namespace SorokChatServer.Infrastructure.Interfaces;

public interface IRepository<T> where T : Base
{
    public Task<Result<T>> CreateAsync(T item, CancellationToken cancellationToken);

    public Task<Result<T>> GetByAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);

    public Task<Result<T>> GetByIdAsync(int id, CancellationToken cancellationToken);

    public Task<Result<T>> UpdateAsync(long id, T item, CancellationToken cancellationToken);

    public Task<Result> DeleteAsync(long id, CancellationToken cancellationToken);
}