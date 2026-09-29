namespace PhoneNumberAnalyzer.Business.Interfaces;

public interface ITokenService
{
    public string GenerateAccessToken(Guid userId);

    public Task<string> GenerateRefreshTokenAsync(Guid userId, int maxActiveSessions = 5);

    Task<(string accessToken, string refreshToken)?> RefreshAsync(string rawRefreshToken);

    Task RevokeAsync(string rawRefreshToken);

}
