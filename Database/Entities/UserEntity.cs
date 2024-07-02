using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Entities
{
    [Table("users")]
    public class UserEntity : BaseEntity
    {
        [Column("email")]
        public string Email { get; set; }

        [Column("password")]
        public string Password { get; set; }

        [Column("surname")]
        public string Surname { get; set; }
    }
}
