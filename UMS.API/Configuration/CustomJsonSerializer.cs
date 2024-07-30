using UMS.Common.Converters;

namespace UMS.API.Configuration;

public static class CustomJsonSerializer
{
    public static IServiceCollection AddCustomJsonSerializer(this IServiceCollection services)
    {
        services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new TimeOnlyJsonConverter());
        });

        return services;
    }
}