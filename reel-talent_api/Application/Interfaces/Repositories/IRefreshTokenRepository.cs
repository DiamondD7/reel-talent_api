using reel_talent_api.Domain.Models;

namespace reel_talent_api.Application.Interfaces.Repositories
{
    public interface IRefreshTokenRepository
    {
        Task AddToken(RefreshToken token);
    }
}
