using reel_talent_api.Domain.Models.DTO;
using reel_talent_api.Domain.Models.DTO.Requests;
using reel_talent_api.Domain.Models.DTO.Results;

namespace reel_talent_api.Application.Interfaces.Services
{
    public interface IAuthService
    {
        Task<LoginResults> LoginAsync(LoginRequest req);
        Task<UserDTO?> SignupAsync(SignupRequest req);
        Task<LoginResults> RefreshTokenAsync(string refreshToken);
        Task LogoutAsync(string token);
    }
}
