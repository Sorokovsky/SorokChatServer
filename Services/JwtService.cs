using Microsoft.IdentityModel.Tokens;
using SorokChatServer.Interfaces;
using SorokChatServer.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace SorokChatServer.Services
{
    public class JwtService : IJwtService
    {
        private readonly int ACCESS_EXPIRATION_TIME = 1000 * 60 * 15;
        private readonly int REFRESH_EXPIRATION_TIME = 1000 * 60 * 60 * 24 * 7;

        private readonly IConfiguration _configuration;

        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public TokensModel GenerateTokens<T>(T payload)
        {
            string accessToken = GenerateAccessToken(payload);
            string refreshToken = GenerateRefreshToken(payload);
            return new TokensModel(accessToken, refreshToken);
        }

        public string GenerateAccessToken<T>(T payload)
        {
            return GenerateToken(payload, ACCESS_EXPIRATION_TIME);
        }

        public T ExtractToken<T>(string token)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            string? secretKey = _configuration["Jwt:SecretKey"];
            ArgumentNullException.ThrowIfNull(secretKey, nameof(secretKey));
            byte[] key = Encoding.ASCII.GetBytes(secretKey);

            TokenValidationParameters tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true
            };

            SecurityToken securityToken;
            ClaimsPrincipal principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            Claim? claim = principal.FindFirst("user");
            ArgumentNullException.ThrowIfNull(claim, nameof(claim));
            string json = claim.Value;
            ArgumentNullException.ThrowIfNull(json, nameof(json));
            T? payload = JsonSerializer.Deserialize<T>(json);
            ArgumentNullException.ThrowIfNull(payload, nameof(payload));
            return payload;
        }

        public string GenerateRefreshToken<T>(T payload)
        {
            return GenerateToken(payload, REFRESH_EXPIRATION_TIME);
        }

        private string GenerateToken<T>(T payload, int expirationTime)
        {
            string json = JsonSerializer.Serialize(payload);
            string? secretKey = _configuration["Jwt:SecretKey"];
            ArgumentNullException.ThrowIfNull(secretKey, nameof(secretKey));
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new JwtSecurityToken(
              _configuration["Jwt:Issuer"],
              _configuration["Jwt:Audit"],
              new List<Claim>() { new Claim("user", json) },
              expires: DateTime.Now.AddMilliseconds(expirationTime),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
