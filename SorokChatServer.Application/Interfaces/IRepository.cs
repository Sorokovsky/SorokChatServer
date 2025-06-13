using System.Linq.Expressions;
using CSharpFunctionalExtensions;
using SorokChatServer.Application.Models;

namespace SorokChatServer.Application.Interfaces;

public interface IRepository<T> where T : Base
{
    public Task<List<T>> GetMany(Expression<Func<T, bool>> predicate);

    public Task<Result<T, string>> FindOne(Expression<Func<T, bool>> predicate);

    public Task<Result<T, string>> Create(T item);

    public Task<Result<T, string>> Update(Expression<Func<T, bool>> predicate, T updated);

    public Task<Result<T, string>> Delete(Expression<Func<T, bool>> predicate);
}