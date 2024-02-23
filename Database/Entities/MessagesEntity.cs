using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SorokChatServer.Database.Entities
{
    [Table("messages")]
    public class MessagesEntity
    {
        [Key, Column("id"), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [ForeignKey("users"), Column("author_id")]
        public long AuthorId { get; set; }

        [Column("author")]
        public UsersEntity Author { get; set; }

        [Column("text")]
        public string Text { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; }
    }
}
