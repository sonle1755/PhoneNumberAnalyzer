using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PhoneNumberAnalyzer.Business.Configurations;
using PhoneNumberAnalyzer.Business.Interfaces;
using PhoneNumberAnalyzer.Data;
using PhoneNumberAnalyzer.Data.Entities;

namespace PhoneNumberAnalyzer.Business.Services;

public class TokenService(AppDbContext db, IOptions<JwtOptions> jwtOptions) : ITokenService
{
    private readonly AppDbContext _db = db;
    private readonly JwtOptions _jwtOptions = jwtOptions.Value;

    public string GenerateAccessToken(Guid userId)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _jwtOptions.Issuer,
            audience: _jwtOptions.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(15),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task<string> GenerateRefreshTokenAsync(Guid userId, int maxActiveSessions = 5)
    {
        var activeSessions = await _db.RefreshTokens
        .Where(t => t.UserId == userId && t.RevokedAt == null && t.ExpiresAt > DateTime.UtcNow)
        .OrderBy(t => t.CreatedAt)
        .ToListAsync();

        if (activeSessions.Count >= maxActiveSessions)
        {
            var toRevoke = activeSessions.Take(activeSessions.Count - maxActiveSessions + 1);
            foreach (var t in toRevoke)
                t.RevokedAt = DateTime.UtcNow;
        }

        var rawToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
        _db.RefreshTokens.Add(new RefreshToken
        {
            Token = HashToken(rawToken),
            UserId = userId,
            ExpiresAt = DateTime.UtcNow.AddDays(_jwtOptions.RefreshTokenExpirationDays)
        });

        await _db.SaveChangesAsync();
        return rawToken;

    }

    private static string HashToken(string token)
    {
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(token));
        return Convert.ToBase64String(bytes);
    }

    public async Task<(string accessToken, string refreshToken)?> RefreshAsync(string rawRefreshToken)
    {
        var hashed = HashToken(rawRefreshToken);
        var stored = await _db.RefreshTokens.FirstOrDefaultAsync(t => t.Token == hashed);

        if (stored is null || !stored.IsActive)
            return null;

        var user = await _db.Users.FindAsync(stored.UserId);
        if (user is null) return null;

        // rotate IN PLACE — no new row
        var newRawRefreshToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
        stored.Token = HashToken(newRawRefreshToken);
        stored.ExpiresAt = DateTime.UtcNow.AddDays(_jwtOptions.RefreshTokenExpirationDays);
        stored.CreatedAt = DateTime.UtcNow; // optional: track "last used" separately if you need history

        await _db.SaveChangesAsync();

        var newAccessToken = GenerateAccessToken(user.Id);

        return (newAccessToken, newRawRefreshToken);

    }

    public async Task RevokeAsync(string rawRefreshToken)
    {
        var hashed = HashToken(rawRefreshToken);
        var stored = await _db.RefreshTokens.FirstOrDefaultAsync(t => t.Token == hashed);

        if (stored is not null && stored.IsActive)
        {
            stored.RevokedAt = DateTime.UtcNow;
            await _db.SaveChangesAsync();
        }
    }

}
