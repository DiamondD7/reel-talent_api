using reel_talent_api.Domain.Models;
using reel_talent_api.Domain.Models.DTO.Results;

namespace reel_talent_api.Application.Interfaces.Services
{
    public interface IJwtService
    {
        Task<TokenResult> GenerateTokens(User user);
    }
}
