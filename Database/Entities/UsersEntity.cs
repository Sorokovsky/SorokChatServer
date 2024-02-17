using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SorokChatServer.Database.Entities
{
    [Table("users"), Microsoft.EntityFrameworkCore.Index(nameof(Email), IsUnique = true)]
    public class UsersEntity
    {
        [Key, Column("id"), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Column("surname")]
        public string Surname { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("email"), Required]
        public string Email { get; set; }

        [Column("password"), Required]
        public string Password { get; set; }

        [Column("avatar_path")]
        public string AvatarPath { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; }

        public UsersEntity(long id)
        {
            Id = id;
        }

        public UsersEntity()
        {

        }
    }

}
