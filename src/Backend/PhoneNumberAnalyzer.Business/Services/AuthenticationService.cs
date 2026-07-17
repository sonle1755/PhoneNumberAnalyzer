using Microsoft.AspNetCore.Identity;
using PhoneNumberAnalyzer.Business.Interfaces;
using PhoneNumberAnalyzer.Data.Entities;
using PhoneNumberAnalyzer.Data.Interfaces;

namespace PhoneNumberAnalyzer.Business.Services;

public class AuthenticationService(IUserRepository userRepository,
                                   IUserAuthProviderRepository userAuthProviderRepository)
    : IAuthenticationService
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IUserAuthProviderRepository _userAuthProviderRepository = userAuthProviderRepository;
    private readonly PasswordHasher<User> _hasher = new();

    public async Task Login(string username, string password)
    {
        var user = await _userRepository.GetByUserNameAsync(username) ?? throw new InvalidOperationException();
        var authProviders = await _userAuthProviderRepository.GetAuthProvidersByUserIdAsync(user.Id);
        var localAuthProvider = authProviders.Single(ap => ap.Provider == "local");
        _hasher.VerifyHashedPassword(user, localAuthProvider.PasswordHash, password);
    }

    public async Task Register(string firstName,
                               string lastName,
                               string username,
                               string password,
                               string? email,
                               string? avatarUrl)
    {
        var user = await _userRepository.AddAsync(firstName, lastName, username, email, avatarUrl);
        var passwordHash = _hasher.HashPassword(user, password);
        await _userAuthProviderRepository.AddAsync(user.Id, "local", passwordHash, null);
    }
}
