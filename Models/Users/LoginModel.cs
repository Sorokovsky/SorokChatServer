using System.Text.Json.Serialization;

namespace SorokChatServer.Models.Users
{
    public class LoginModel
    {
        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("password")]
        public string Password { get; set; }
    }
}
