using System.Text.Json.Serialization;

namespace SorokChatServer.Models.Users
{
    public class UpdateUserModel
    {
        [JsonPropertyName("surname")]
        public string? Surname { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("avatar_path")]
        public string? AvatarPath { get; set; }

        [JsonPropertyName("email")]
        public string? Email { get; set; }
    }
}
