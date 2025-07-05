using AutoMapper;
using SorokChatServer.Infrastructure.Entities;
using SorokChatServer.Infrastructure.Models;

namespace SorokChatServer.Infrastructure.Mapping;

public class UsersProfile : Profile
{
    public UsersProfile()
    {
        MergeEntityMapping();
        ModelToEntityMapping();
        EntityToModelMapping();
    }

    private void MergeEntityMapping()
    {
        CreateMap<UserEntity, UserEntity>()
            .AfterMap((_, dest) => { dest.UpdatedAt = DateTime.UtcNow; })
            .ForAllMembers(option => { option.Condition((_, _, sourceMember) => sourceMember is not null); });
    }

    private void ModelToEntityMapping()
    {
        CreateMap<User, UserEntity>();
    }

    private void EntityToModelMapping()
    {
        CreateMap<UserEntity, User>().ConstructUsing(entity => User.Create(
            entity.Id,
            entity.CreatedAt,
            entity.UpdatedAt,
            entity.Name.First,
            entity.Name.Last,
            entity.Name.Middle,
            entity.Email.Value,
            entity.Password.Value
        ).Value);
    }
}