using PhoneNumberAnalyzer.Data.Entities;

namespace PhoneNumberAnalyzer.Data.Interfaces;

public interface IUserAuthProviderRepository
{
    public Task AddAsync(Guid userId,
                         string provider,
                         string passwordHash,
                         string? providerUserId);

    public Task<IEnumerable<UserAuthProvider>> GetAuthProvidersByUserIdAsync(Guid userId);
}
