using AutoMapper;
using CSharpFunctionalExtensions;
using SorokChatServer.Infrastructure.Entities;
using SorokChatServer.Infrastructure.Interfaces;

namespace SorokChatServer.Database.Repositories;

public class UsersRepository : BaseRepository<UserEntity>, IUsersRepository
{
    public UsersRepository(DatabaseContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<Result<UserEntity>> GetUserByEmailAsync(string email, CancellationToken cancellationToken)
    {
        return await GetByAsync(x => x.Email.Value == email, cancellationToken);
    }
}