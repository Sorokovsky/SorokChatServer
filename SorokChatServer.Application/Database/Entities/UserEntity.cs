namespace SorokChatServer.Application.Database.Entities;

public class UserEntity : BaseEntity
{
    public UserEntity(long id, DateTime createdAt, DateTime updatedAt, string firstName, string lastName, string email, string password) 
        : base(id, createdAt, updatedAt)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Password = password;
    }

    public UserEntity()
    {
    }
    
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public string Email { get; set; }
    
    public string Password { get; set; }
}