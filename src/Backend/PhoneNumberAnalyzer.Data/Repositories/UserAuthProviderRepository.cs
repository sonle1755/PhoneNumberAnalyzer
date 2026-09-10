using Microsoft.EntityFrameworkCore;
using PhoneNumberAnalyzer.Data.Entities;
using PhoneNumberAnalyzer.Data.Interfaces;

namespace PhoneNumberAnalyzer.Data.Repositories;

public class UserAuthProviderRepository(AppDbContext db) : IUserAuthProviderRepository
{
    private readonly AppDbContext _db = db;

    public async Task AddAsync(Guid userId,
                               string provider,
                               string passwordHash,
                               string? providerUserId)
    {
        var authProvider = new UserAuthProvider()
        {
            UserId = userId,
            Provider = provider,
            PasswordHash = passwordHash,
            ProviderUserId = providerUserId,
        };

        await _db.UserAuthProviders.AddAsync(authProvider);
        await _db.SaveChangesAsync();
    }

    public async Task<IEnumerable<UserAuthProvider>> GetAuthProvidersByUserIdAsync(Guid userId)
    {
        return await _db.UserAuthProviders.AsNoTracking().Where(ap => ap.UserId == userId).ToListAsync();
    }
}
