using reel_talent_api.Domain.Models.DTO;
using reel_talent_api.Domain.Models.DTO.Requests;

namespace reel_talent_api.Application.Interfaces.Services
{
    public interface IAuthService
    {
        Task<UserDTO> LoginAsync(LoginRequest req);
        Task<UserDTO?> SignupAsync(SignupRequest req);
    }
}
