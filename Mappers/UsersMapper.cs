using SorokChatServer.Database.Entities;
using SorokChatServer.Models.Users;

namespace SorokChatServer.Mappers
{
    public class UsersMapper
    {
        public static UsersModel ToModel(UsersEntity entity)
        {
            UsersModel model = new UsersModel();
            model.Id = entity.Id;
            model.Surname = entity.Surname;
            model.Email = entity.Email;
            model.Name = entity.Name;
            model.CreatedAt = entity.CreatedAt;
            model.UpdatedAt = entity.UpdatedAt;
            model.AvatarPath = entity.AvatarPath;
            return model;
        }

        public static UsersEntity ToEntity(UsersModel model)
        {
            UsersEntity entity = new UsersEntity();
            entity.Id = model.Id;
            entity.Surname = model.Surname;
            entity.Email = model.Email;
            entity.Name = model.Name;
            entity.CreatedAt = model.CreatedAt;
            entity.UpdatedAt = model.UpdatedAt;
            entity.AvatarPath = model.AvatarPath;
            return entity;
        }

        public static List<UsersModel> ToModels(List<UsersEntity> entities)
        {
            List<UsersModel> models = new List<UsersModel>();
            foreach (UsersEntity entity in entities)
            {
                models.Add(ToModel(entity));
            }
            return models;
        }

        public static List<UsersEntity> ToEntities(List<UsersModel> models)
        {
            List<UsersEntity> entities = new List<UsersEntity>();
            foreach (UsersModel model in models)
            {
                entities.Add(ToEntity(model));
            }
            return entities;
        }

        public static UsersEntity RegistrationToEntity(RegistrationModel model)
        {
            UsersEntity entity = new UsersEntity();
            entity.Surname = model.Surname;
            entity.Name = model.Name;
            entity.Email = model.Email;
            entity.AvatarPath = model.AvatarPath;
            return entity;
        }

        public static UsersEntity UpdateToEntity(UpdateUserModel model)
        {
            UsersEntity usersEntity = new UsersEntity();
            if(model.Surname != null)
            {
                usersEntity.Surname = model.Surname;
            }
            if(model.Name != null)
            {
                usersEntity.Name = model.Name;
            }
            if(model.Email != null)
            {
                usersEntity.Email = model.Email;
            }
            if(model.AvatarPath != null)
            {
                usersEntity.AvatarPath = model.AvatarPath;
            }    
            if (model.Password != null)
            { 
                usersEntity.Password = model.Password;
            }
            return usersEntity;
        }
    }
}
