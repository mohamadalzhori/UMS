using Hangfire;
using Hangfire.PostgreSql;

namespace UMS.API.Configuration;

public static class HangfireConfig
{
    public static IServiceCollection ConfigureHangfire(this IServiceCollection services, WebApplicationBuilder builder)
    {
        services.AddHangfire(config =>
            config.UsePostgreSqlStorage(x =>
                x.UseNpgsqlConnection(builder.Configuration.GetConnectionString("DefaultConnection"))));

        services.AddHangfireServer();

        return services;
    }
}