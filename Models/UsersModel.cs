using System.Text.Json.Serialization;

namespace SorokChatServer.Models
{
    [Serializable]
    public class UsersModel
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName ("name")]
        public string Name { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("surname")]
        public string Surname { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt { get; set;}

        [JsonPropertyName("avatar_path")]
        public string AvatarPath { get; set; }
    }
}
