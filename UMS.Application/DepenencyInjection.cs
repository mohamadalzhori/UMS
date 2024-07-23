using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UMS.Application.Caching;

namespace UMS.Application
{
    public static class DepenencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(DepenencyInjection).Assembly));

            services.AddAutoMapper(typeof(DepenencyInjection).Assembly);

            var constr = configuration.GetConnectionString("Cache") ??
                         throw new ArgumentNullException(nameof(configuration));

            services.AddStackExchangeRedisCache(opts => opts.Configuration = constr);

            services.AddSingleton<ICacheService, CacheService>();

            return services;
        }
    }
}