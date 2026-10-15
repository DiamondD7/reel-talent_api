using Microsoft.EntityFrameworkCore;
using reel_talent_api.Application.Interfaces.Repositories;
using reel_talent_api.Domain.Models;

namespace reel_talent_api.Infrastructure.Persistence.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly AppDbContext _context;
        public RefreshTokenRepository(AppDbContext context)
        {
            _context = context;   
        }

        public async Task<RefreshToken> GetByHashAsync(string token)
        {

            return await _context.RefreshTokens
                .Include(x => x.User).ThenInclude(u => u.Talent)
                .Include(x => x.User).ThenInclude(u => u.CastingProfile)
                .FirstOrDefaultAsync(x => x.TokenHash == token);
        }

        public async Task RevokeAllTokensAsync(Guid userId)
        {
            var now = DateTime.UtcNow;
            await _context.RefreshTokens.Where(x => x.UserId == userId && x.RevokedAt == null)
                .ExecuteUpdateAsync(x => x.SetProperty(x => x.RevokedAt, now));

        }

        public async Task AddToken(RefreshToken token)
        {
            _context.RefreshTokens.Add(token);
            await _context.SaveChangesAsync();

        }

        public async Task RevokeCurrentToken(RefreshToken oldToken, string newToken)
        {
            var nowCurrent = await GetByHashAsync(newToken);

            oldToken.RevokedAt = DateTime.UtcNow;
            oldToken.ReplacedByTokenId = nowCurrent.Id;

            await _context.SaveChangesAsync();
        }

        public async Task RevokeLogoutAsync(RefreshToken token)
        {
            token.RevokedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }

     
    }
}
