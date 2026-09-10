using Microsoft.Extensions.DependencyInjection;
using PhoneNumberAnalyzer.Business.Interfaces;
using PhoneNumberAnalyzer.Business.Services;

namespace PhoneNumberAnalyzer.Business;

public static class DependencyInjection
{
    public static IServiceCollection AddBusiness(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IPatternTemplateService, PatternTemplateService>();
        services.AddScoped<IPatternTemplateEvaluationService, PatternTemplateEvaluationService>();
        services.AddSingleton<IPatternSpecificationCache, PatternSpecificationCache>();
        services.AddScoped<IPhoneNumberExtractionService, PhoneNumberExtractionService>();

        return services;
    }
}
