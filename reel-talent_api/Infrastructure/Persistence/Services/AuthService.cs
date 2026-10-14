using reel_talent_api.Application.Interfaces.Repositories;
using reel_talent_api.Application.Interfaces.Services;
using reel_talent_api.Domain.Models;
using reel_talent_api.Domain.Models.DTO;
using reel_talent_api.Domain.Models.DTO.Requests;
using reel_talent_api.Domain.Models.DTO.Results;
using System.Data;

namespace reel_talent_api.Infrastructure.Persistence.Services
{
    public class AuthService : IAuthService
    {
        private readonly IRefreshTokenRepository _refreshRepo;
        private readonly IUserRepository _userRepo;
        private readonly IJwtService _jwtService;
        public AuthService(IUserRepository userRepo, IJwtService jwtService, IRefreshTokenRepository refreshRepo)
        {
            _userRepo = userRepo;
            _jwtService = jwtService;
            _refreshRepo = refreshRepo;
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
                TokenHash = BCrypt.Net.BCrypt.HashPassword(tokenResult.RawRefreshToken), //hashing refresh token
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
                PasswordHash = hashedPW,
                Role = req.Role,
                CreatedAt = DateTime.UtcNow
            };

            await _userRepo.AddAsync(user, req);

            return new UserDTO
            {
                Id = user.Id,
                Email = user.Email,
                Role = user.Role
            };

        }

        public async Task<string> RefreshTokenHashing(string rft)
        {
            return BCrypt.Net.BCrypt.HashPassword(rft);
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
