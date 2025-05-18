using Microsoft.EntityFrameworkCore;
using UserContacts.Dal;
using UserContacts.Dal.Entities;

namespace UserContacts.Repository.Services;

public class RefreshTokenRepository(MainContext _mainContext) : IRefreshTokenRepository
{
    public async Task AddRefreshToken(RefreshToken refreshToken)
    {
        await _mainContext.RefreshTokens.AddAsync(refreshToken);
        await _mainContext.SaveChangesAsync();
    }

    public async Task<RefreshToken> SelectRefreshToken(string refreshToken, long userId)
    {
        return await _mainContext.RefreshTokens.FirstOrDefaultAsync(rt => rt.Token == refreshToken && rt.UserId == userId);
    }
}
