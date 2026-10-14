using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using reel_talent_api.Application.Interfaces.Repositories;
using reel_talent_api.Domain.Models;
using reel_talent_api.Domain.Models.DTO;
using reel_talent_api.Domain.Models.DTO.Requests;
using System.Security.Cryptography;
using System.Text;

namespace reel_talent_api.Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {

        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetByEmailAsync(LoginRequest req)
        {
            return await _context.Users
                            .Include(x => x.Talent)
                            .Include(x => x.CastingProfile)
                            .FirstOrDefaultAsync(x => x.Email == req.Email);
        }

        public async Task<bool> EmailExistAsync(string email)
        {
            return await _context.Users.AnyAsync(x => x.Email == email);
        }

        public async Task AddAsync(User user, SignupRequest req)
        {
            _context.Users.Add(user);

            switch (user.Role)
            {
                case UserRole.Talent:
                    var talent = user.Talent = new Talent
                    {
                        FirstName = req.FirstName,
                        LastName = req.LastName,
                        FullName = $"{req.FirstName} {req.LastName}".Trim()
                    };

                    _context.Talents.Add(talent);
                    break;

                case UserRole.Casting:
                    var casting = user.CastingProfile = new CastingProfile
                    {
                        CompanyName = req.CompanyName
                    };

                    _context.CastingProfiles.Add(casting);
                    break;

                default:
                    throw new Exception("uknown role");
                
            }
            
            await _context.SaveChangesAsync();
        }
    }
}
