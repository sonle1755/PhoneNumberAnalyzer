using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PhoneNumberAnalyzer.Business.Configurations;
using PhoneNumberAnalyzer.Business.Interfaces;
using PhoneNumberAnalyzer.Business.Services;

namespace PhoneNumberAnalyzer.Business;

public static class DependencyInjection
{
    public static IServiceCollection AddBusiness(this IServiceCollection services,
                                                 IConfiguration configuration)
    {
        services.AddOptions<JwtOptions>().Bind(configuration.GetSection(JwtOptions.SectionName))
            .Validate(o => o.AccessTokenExpirationMinutes > 0, "AccessTokenExpirationMinutes must be positive")
            .Validate(o => o.RefreshTokenExpirationDays > 0, "RefreshTokenExpirationDays must be positive")
            .ValidateOnStart();

        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IPatternTemplateService, PatternTemplateService>();
        services.AddScoped<IPatternTemplateEvaluationService, PatternTemplateEvaluationService>();
        services.AddSingleton<IPatternSpecificationCache, PatternSpecificationCache>();
        services.AddScoped<IPhoneNumberExtractionService, PhoneNumberExtractionService>();
        services.AddScoped<ITokenService, TokenService>();

        services.AddHostedService<RefreshTokenCleanupService>();
        return services;
    }
}
