using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace UMS.API.Configuration;

public static class KeycloakConfig
{
    public static IServiceCollection AddKeycloak(this IServiceCollection services)
    {
        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.Authority = "http://localhost:8080/realms/ums"; // Keycloak URL
                options.Audience = "umsclient"; // Client ID from Keycloak
                options.RequireHttpsMetadata = false; // Set to true in production

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                };
            });
        
        return services;
    }
}