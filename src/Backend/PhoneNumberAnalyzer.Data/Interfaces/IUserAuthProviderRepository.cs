using PhoneNumberAnalyzer.Data.Entities;

namespace PhoneNumberAnalyzer.Data.Interfaces;

public interface IUserAuthProviderRepository
{
    public Task AddAsync(int userId,
                         string provider,
                         string passwordHash,
                         string? providerUserId);

    public Task<IEnumerable<UserAuthProvider>> GetAuthProvidersByUserIdAsync(int userId);
}
