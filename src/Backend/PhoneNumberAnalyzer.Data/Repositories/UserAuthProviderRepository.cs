using Microsoft.EntityFrameworkCore;
using PhoneNumberAnalyzer.Data.Entities;
using PhoneNumberAnalyzer.Data.Interfaces;

namespace PhoneNumberAnalyzer.Data.Repositories;

public class UserAuthProviderRepository(AppDbContext db) : IUserAuthProviderRepository
{
    private readonly AppDbContext _db = db;

    public async Task AddAsync(int userId,
                               string provider,
                               string passwordHash,
                               string? providerUserId)
    {
        var user = UserAuthProvider.Create(userId, provider, passwordHash, providerUserId);
        await _db.UserAuthProviders.AddAsync(user);
        await _db.SaveChangesAsync();
    }

    public async Task<IEnumerable<UserAuthProvider>> GetAuthProvidersByUserIdAsync(int userId)
    {
        return await _db.UserAuthProviders.AsNoTracking().Where(ap => ap.UserId == userId).ToListAsync();
    }
}
