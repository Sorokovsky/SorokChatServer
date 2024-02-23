using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SorokChatServer.Models.Users
{
    public class LoginModel
    {
        [JsonPropertyName("email"), Required]
        public string Email { get; set; }

        [JsonPropertyName("password"), Required]
        public string Password { get; set; }
    }
}
