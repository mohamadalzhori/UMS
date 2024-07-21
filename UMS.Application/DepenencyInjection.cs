using Microsoft.Extensions.DependencyInjection;
namespace UMS.Application
{
    public static class DepenencyInjection
    {

        public static IServiceCollection AddApplication(this IServiceCollection services)
        {

            services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(DepenencyInjection).Assembly));

            services.AddAutoMapper(typeof(DepenencyInjection).Assembly);

            return services;
        }
    }
}
