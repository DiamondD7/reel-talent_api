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


        public async Task AddToken(RefreshToken token)
        {
            _context.RefreshTokens.Add(token);
            await _context.SaveChangesAsync();

        }
    }
}
