using reel_talent_api.Domain.Models;
using reel_talent_api.Domain.Models.DTO;
using reel_talent_api.Domain.Models.DTO.Requests;

namespace reel_talent_api.Application.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetByEmailAsync(LoginRequest req);
        Task<bool> EmailExistAsync(string email);
        Task AddAsync(User user, SignupRequest req);
    }
}
