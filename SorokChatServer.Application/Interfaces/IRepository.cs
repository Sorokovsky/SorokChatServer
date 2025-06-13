using System.Linq.Expressions;
using CSharpFunctionalExtensions;
using SorokChatServer.Application.Database.Entities;

namespace SorokChatServer.Application.Interfaces;

public interface IRepository<T> where T : BaseEntity
{
    public Task<List<T>> GetMany(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);

    public Task<Result<T, string>> GetOne(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);

    public Task<Result<T, string>> Create(T item, CancellationToken cancellationToken);

    public Task<Result<T, string>> Update(Expression<Func<T, bool>> predicate, T updated,
        CancellationToken cancellationToken);

    public Task<Result<T, string>> Delete(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);
}