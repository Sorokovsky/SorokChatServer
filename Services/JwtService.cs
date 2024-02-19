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
        private readonly int ACCESS_EXPIRATION_TIME = 60 * 15;
        private readonly int REFRESH_EXPIRATION_TIME = 60 * 60 * 24 * 7;
        private readonly IConfiguration _configuration;
        private readonly SymmetricSecurityKey _jwtSecret;
        private readonly string _issuer;
        private readonly string _audit;

        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
            string secretKey = _configuration["Jwt:SecretKey"];
            _jwtSecret = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));
            _audit = _configuration["Jwt:Audit"];
            _issuer = _configuration["Jwt:Issuer"];
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
            SecurityToken security;
            ClaimsPrincipal principal = GetPrincipal(token, out security);
            string? json = principal.Claims.First(c => c.Type == "user")?.Value;
            if (json == null)
            {
                throw new Exception("Token not have user");
            }
            T? payload = JsonSerializer.Deserialize<T>(json);
            if(payload == null)
            {
                throw new Exception("Payload is null");
            }
            return payload;
        }

        public string GenerateRefreshToken<T>(T payload)
        {
            return GenerateToken(payload, REFRESH_EXPIRATION_TIME);
        }

        public bool IsTokenValid(string token)
        {
            try
            {
                SecurityToken securityToken;
                ClaimsPrincipal principal = GetPrincipal(token, out securityToken);

                if (securityToken.ValidTo > DateTime.Now)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        private string GenerateToken<T>(T payload, int expirationTime)
        {
            string json = JsonSerializer.Serialize(payload);
            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, "user_id"),
                new Claim("user", json)
            };

            var key = _jwtSecret;
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audit,
                claims: claims,
                expires: DateTime.Now.AddSeconds(expirationTime),
                signingCredentials: creds);

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(token);
            return encodedJwt;
        }

        private ClaimsPrincipal GetPrincipal(string token, out SecurityToken security)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            TokenValidationParameters validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = _jwtSecret,
                ValidateIssuer = true,
                ValidIssuer = _issuer,
                ValidateAudience = true,
                ValidAudience = _audit,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            ClaimsPrincipal principal = tokenHandler.ValidateToken(token, validationParameters, out security);
            return principal;
        }
    }
}
