using reel_talent_api.Application.Interfaces.Repositories;
using reel_talent_api.Application.Interfaces.Services;
using reel_talent_api.Domain.Models;
using reel_talent_api.Domain.Models.DTO;
using reel_talent_api.Domain.Models.DTO.Requests;

namespace reel_talent_api.Infrastructure.Persistence.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepo;
        public AuthService(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task<UserDTO> LoginAsync(LoginRequest req)
        {
            var user = await _userRepo.GetByEmailAsync(req);
            if (user == null || !BCrypt.Net.BCrypt.Verify(req.Password, user.PasswordHash))
            {
                return null;
            }


            return new UserDTO
            {
                Email = user.Email,
                Role = user.Role
            };
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
