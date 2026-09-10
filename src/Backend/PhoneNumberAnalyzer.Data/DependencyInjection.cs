using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PhoneNumberAnalyzer.Data.Interfaces;
using PhoneNumberAnalyzer.Data.Repositories;

namespace PhoneNumberAnalyzer.Data;

public static class DependencyInjection
{
    public static IServiceCollection AddData(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString =
            configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(connectionString));

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserAuthProviderRepository, UserAuthProviderRepository>();
        services.AddScoped<IPatternTemplateRepository, PatternTemplateRepository>();
        services.AddScoped<IPatternRuleGroupRepository, PatternRuleGroupRepository>();
        return services;
    }
}
