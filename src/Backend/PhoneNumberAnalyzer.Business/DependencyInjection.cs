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

        return services;
    }
}
