using Google.Apis.Auth;
using Microsoft.EntityFrameworkCore;
using reel_talent_api.Application.Interfaces.Repositories;
using reel_talent_api.Application.Interfaces.Services;
using reel_talent_api.Domain.Models;
using reel_talent_api.Domain.Models.DTO;
using reel_talent_api.Domain.Models.DTO.Requests;
using reel_talent_api.Domain.Models.DTO.Results;
using System.Data;
using System.Security.Cryptography;
using System.Text;

namespace reel_talent_api.Infrastructure.Persistence.Services
{
    public class AuthService : IAuthService
    {
        private readonly IRefreshTokenRepository _refreshRepo;
        private readonly IUserRepository _userRepo;
        private readonly IJwtService _jwtService;
        private readonly IConfiguration _config;
        public AuthService(IUserRepository userRepo, IJwtService jwtService, IRefreshTokenRepository refreshRepo, IConfiguration config)
        {
            _userRepo = userRepo;
            _jwtService = jwtService;
            _refreshRepo = refreshRepo;
            _config = config;
        }

        public async Task<LoginResults> LoginAsync(LoginRequest req)
        {
            var user = await _userRepo.GetByEmailAsync(req);
            if (user == null || !BCrypt.Net.BCrypt.Verify(req.Password, user.PasswordHash))
            {
                return null;
            }

            var tokenResult = await _jwtService.GenerateTokens(user);


            //SAVING TOKEN ENTITY TO DB
            var tokenEntity = new RefreshToken
            {
                UserId = user.Id,
                TokenHash = Hash(tokenResult.RawRefreshToken), //hashing refresh token
                ExpiresAt = tokenResult.RefreshTokenExpiresAt,
            };

            await _refreshRepo.AddToken(tokenEntity);

            var userDTO = new UserDTO
            {
                Email = user.Email,
                Role = user.Role,
            };
            

            return new LoginResults { User = userDTO, Tokens = tokenResult };
            //return MapToDto(user);
        }

        public async Task<UserDTO> SignupAsync(SignupRequest req)
        {
            if(await _userRepo.EmailExistAsync(req.Email))
            {
                return null;
            }


            
            var hashedPW = BCrypt.Net.BCrypt.HashPassword(req.Password);

            var user = new User
            {
                Email = req.Email,
                PasswordHash = string.IsNullOrEmpty(req.Password) ? "" : hashedPW,
                Role = req.Role,
                CreatedAt = DateTime.UtcNow
            };

            await _userRepo.AddAsync(user, req);

            //for google provider sign up
            if (req.OtherProvider==true)
            {
                var newUser = new GooglePayloadResult
                {
                    UserId = user.Id,
                    ProviderId = req.ProviderId
                };
                await _userRepo.AddProviderAsync(newUser);
            }

            return new UserDTO
            {
                Id = user.Id,
                Email = user.Email,
                Role = user.Role
            };

        }

        public async Task<LoginResults> RefreshTokenAsync(string refreshToken)
        {
            var hashToken = Hash(refreshToken);
            var checkToken = await _refreshRepo.GetByHashAsync(hashToken);

            if (checkToken == null)
            {
                return null;
            }


            //this is the one for when the token has already been revoked.
            if(checkToken.RevokedAt != null)
            {
                await _refreshRepo.RevokeAllTokensAsync(checkToken.UserId);
                return null;
            }

            if(DateTime.UtcNow > checkToken.ExpiresAt)
            {
                return null;
            }

            var generateToken = await _jwtService.GenerateTokens(checkToken.User);
            var tokenEntity = new RefreshToken
            {
                UserId = checkToken.User.Id,
                TokenHash = Hash(generateToken.RawRefreshToken), //hashing refresh token
                ExpiresAt = generateToken.RefreshTokenExpiresAt,
            };

            await _refreshRepo.AddToken(tokenEntity);
            await _refreshRepo.RevokeCurrentToken(checkToken, tokenEntity.TokenHash);

            var userDTO = new UserDTO
            {
                Email = checkToken.User.Email,
                Role = checkToken.User.Role,
            };
             

            return new LoginResults { User = userDTO, Tokens = generateToken };

        }


        public async Task LogoutAsync(string token)
        {
            var hashedToken = Hash(token);
            var currentToken = await _refreshRepo.GetByHashAsync(hashedToken);
            await _refreshRepo.RevokeLogoutAsync(currentToken);
        }

        public async Task<GooglePayloadResult> GoogleSignupAsync(GoogleSignupRequest req)
        {
            var payload = await ValidateGoogleTokenAsync(req.IdToken);
            if(payload == null || !payload.EmailVerified)
            {
                return null;
            }


            return payload;
        }

        private async Task<GooglePayloadResult> ValidateGoogleTokenAsync(string idToken)
        {
            try
            {
                var settings = new GoogleJsonWebSignature.ValidationSettings
                {
                    Audience = new[] { _config["Google:ClientId"] }
                };

                var payload = await GoogleJsonWebSignature.ValidateAsync(idToken, settings);
                var newPayload = new GooglePayloadResult
                {
                    FirstName = payload.GivenName,
                    LastName = payload.FamilyName,
                    Email = payload.Email,
                    EmailVerified = payload.EmailVerified,
                    ProviderId = payload.Subject
                    
                };


                return newPayload;
            }
            catch (InvalidJwtException)
            {
                return null;   // bad signature, expired, wrong audience, malformed
            }
        }


        private string Hash(string rawToken)
        {
            var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(rawToken));
            return Convert.ToHexString(bytes);
        }



        //private static TalentDTO MapToDto(Talent t) => new()
        //{
        //    Id = t.Id,
        //    FullName = t.FullName,
        //    Email = t.Email,
        //    Role = t.Role,
        //    Details = t.Details == null ? null : new TalentDetailsDTO
        //    {
        //        Bio = t.Details.Bio,
        //        Ethnicity = t.Details.Ethnicity,
        //        NaturalAccent = t.Details.NaturalAccent,
        //        EyeColor = t.Details.EyeColor,
        //        HairColor = t.Details.HairColor,
        //        ShoeSize = t.Details.ShoeSize,
        //        ClothingSize = t.Details.ClothingSize,
        //        Height = t.Details.Height,
        //        Weight = t.Details.Weight,
        //        Skills = t.Details.Skills.Select(s => s.Name).ToList(),
        //        Languages = t.Details.Languages.Select(l => l.Name).ToList(),
        //        Experiences = t.Details.Experiences.Select(e => new TalentExperienceDTO
        //        {
        //            Id = e.Id,
        //            Year = e.Year,
        //            Category = e.Category,
        //            Role = e.Role,
        //            Title = e.Title,
        //            Production = e.Production,
        //            DirectorOrCompany = e.DirectorOrCompany,
        //            Url = e.Url
        //        }).ToList()
        //    }
        //};
    }
}
