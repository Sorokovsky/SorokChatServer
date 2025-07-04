using CSharpFunctionalExtensions;
using SorokChatServer.Infrastructure.Entities;

namespace SorokChatServer.Infrastructure.Interfaces;

public interface IUsersRepository : IRepository<UserEntity>
{
    public Task<Result<UserEntity>> GetUserByEmailAsync(string email, CancellationToken cancellationToken);
}