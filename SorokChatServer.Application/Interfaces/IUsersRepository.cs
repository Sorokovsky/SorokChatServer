using CSharpFunctionalExtensions;
using SorokChatServer.Application.Database.Entities;

namespace SorokChatServer.Application.Interfaces;

public interface IUsersRepository : IRepository<UserEntity>
{
    public Task<Result<UserEntity, string>> GetOneByEmail(string email, CancellationToken cancellationToken);

    public Task<Result<UserEntity, string>> GetOneById(long id, CancellationToken cancellationToken);
}