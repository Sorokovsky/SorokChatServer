using CSharpFunctionalExtensions;
using SorokChatServer.Application.Database.Entities;
using SorokChatServer.Application.Interfaces;

namespace SorokChatServer.Application.Database.Repositories;

public class UsersRepository : BaseRepository<UserEntity>, IUsersRepository
{
    public UsersRepository(SorokChatDatabaseContext context) : base(context)
    {
    }

    public async Task<Result<UserEntity, string>> GetOneById(long id, CancellationToken cancellationToken)
    {
        return await GetOne(x => x.Id == id, cancellationToken);
    }

    public async Task<Result<UserEntity, string>> GetOneByEmail(string email, CancellationToken cancellationToken)
    {
        return await GetOne(x => x.Email == email, cancellationToken);
    }
}