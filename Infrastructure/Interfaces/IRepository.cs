using System.Linq.Expressions;
using CSharpFunctionalExtensions;
using Infrastructure.Models;

namespace Infrastructure.Interfaces;

public interface IRepository<T> where T : Base
{
    public Task<Result> Create(T entity, CancellationToken cancellationToken = default);
    
    public Task<List<T>> GetMany(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
    
    public Task<Result<T, string>> GetById(long id, CancellationToken cancellationToken = default);
    
    public Task<Result<T, Result>> GetOne(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
    
    public Task<Result<T, string>> Update(Expression<Func<T, bool>> predicate, T entity);
    
    public Task<Result<T, string>> Delete(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
}