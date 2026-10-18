using Microsoft.IdentityModel.Tokens;
using reel_talent_api.Application.Interfaces.Repositories;
using reel_talent_api.Application.Interfaces.Services;
using reel_talent_api.Domain.Models;
using reel_talent_api.Domain.Models.DTO.Results;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace reel_talent_api.Infrastructure.Persistence.Services
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _config;
        public JwtService(IConfiguration config)
        {
            _config = config;
        }

        public async Task<TokenResult> GenerateTokens(User user)
        {
            try
            {
                var issuer = _config["JwtSettings:Issuer"];
                var audience = _config["JwtSettings:Audience"];
                var key = new SymmetricSecurityKey(Convert.FromBase64String(_config["Jwt:Key"]!));

                var tokenValidityMins = _config.GetValue<int>("JwtSettings:TokenValidityMins");
                var tokenExpiryTimeStamp = DateTime.UtcNow.AddMinutes(tokenValidityMins);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(JwtRegisteredClaimNames.Email, user.Email),
                        new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                        new Claim(JwtRegisteredClaimNames.Name, user.Role == UserRole.Talent ? user.Talent.FullName : user.CastingProfile.CompanyName),
                        new Claim(ClaimTypes.Role, user.Role.ToString())
                    }),
                    Expires = tokenExpiryTimeStamp,
                    Issuer = issuer,
                    Audience = audience,
                    SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var accessToken = tokenHandler.WriteToken(securityToken);

                var refreshToken = GenerateRefreshToken();
                var refreshTokenExpiry = DateTime.UtcNow.AddMinutes(20160);


                return new TokenResult
                {
                    AccessToken = accessToken,
                    RawRefreshToken = refreshToken,
                    AccessTokenExpiresAt = tokenExpiryTimeStamp,
                    RefreshTokenExpiresAt = refreshTokenExpiry,
                };
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new Byte[64];
            using(var numberGenerator = RandomNumberGenerator.Create())
            {
                numberGenerator.GetBytes(randomNumber);
            }

            var token = Convert.ToBase64String(randomNumber);
            


            return token;
        }

    }
}
