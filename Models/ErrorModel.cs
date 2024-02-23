using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SorokChatServer.Models
{
    public class ErrorModel
    {
        [JsonPropertyName("message")]
        public string Message { get; }

        [JsonPropertyName("statusCode")]
        public int StatusCode { get; }

        public ErrorModel(string message, HttpStatusCode statusCode)
        {
            Message = message;
            StatusCode = (int)statusCode;
        }
    }
}
