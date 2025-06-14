using CSharpFunctionalExtensions;
using SorokChatServer.Application.Models;

namespace SorokChatServer.Application.Interfaces;

public interface IUsersService
{
    public Task<Result<User, string>> GetById(long id, CancellationToken cancellationToken);

    public Task<Result<User, string>> GetByEmail(string email, CancellationToken cancellationToken);

    public Task<Result<User, string>> Create(User user, CancellationToken cancellationToken);

    public Task<Result<User, string>> Update(long id, User updatedUser, CancellationToken cancellationToken);

    public Task<Result<User, string>> Delete(long id, CancellationToken cancellationToken);
}