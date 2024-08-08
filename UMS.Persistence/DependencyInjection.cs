using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UMS.Persistence.Services;

namespace UMS.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddSingleton<ITenantService, TenantService>(); 
            services.AddHttpContextAccessor();
            services.AddDbContext<AppDbContext>(options => { });
            
            // services.AddDbContext<AppDbContext>(options =>
                // options.UseNpgsql(
                    // configuration.GetConnectionString("DefaultConnection")));

            services.AddHealthChecks()
                .AddNpgSql(configuration.GetConnectionString("DefaultConnection")!)
                .AddRedis(configuration.GetConnectionString("Cache")!)
                .AddUrlGroup(new Uri("http://localhost:8081"), name: "seq", tags: new[] { "services" });        
        
            return services;
        }
    }
}
