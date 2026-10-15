using reel_talent_api.Domain.Models;

namespace reel_talent_api.Application.Interfaces.Repositories
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshToken> GetByHashAsync(string token);
        Task RevokeAllTokensAsync(Guid userId);
        Task AddToken(RefreshToken token);

        Task RevokeCurrentToken(RefreshToken oldToken, string newToken);
        Task RevokeLogoutAsync(RefreshToken token);
    }
}
