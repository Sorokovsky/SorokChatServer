using System.Text.Json.Serialization;

namespace SorokChatServer.Models.Users
{
    public class RegistrationModel : LoginModel
    {
        [JsonPropertyName("avatar_path")]
        public string AvatarPath { get; set; } = "";

        [JsonPropertyName("surname")]
        public string Surname { get; set; } = "";

        [JsonPropertyName("name")]
        public string Name { get; set; } = "";
    }
}
