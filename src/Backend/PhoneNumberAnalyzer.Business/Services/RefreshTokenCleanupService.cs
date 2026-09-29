using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PhoneNumberAnalyzer.Data;

namespace PhoneNumberAnalyzer.Business.Services;

public class RefreshTokenCleanupService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<RefreshTokenCleanupService> _logger;
    private static readonly TimeSpan Interval = TimeSpan.FromHours(24);

    public RefreshTokenCleanupService(IServiceProvider serviceProvider, ILogger<RefreshTokenCleanupService> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            using var scope = _serviceProvider.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            var cutoff = DateTimeOffset.UtcNow;
            var deleted = await db.RefreshTokens
                                  .Where(t => t.ExpiresAt < cutoff || t.RevokedAt != null)
                                  .ExecuteDeleteAsync(cancellationToken);

            _logger.LogInformation("Cleaned up {Count} expired/revoked refresh tokens", deleted);

            await Task.Delay(Interval, cancellationToken);
        }
    }
}
