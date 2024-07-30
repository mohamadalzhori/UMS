using Asp.Versioning;

namespace UMS.API.Configuration;

public static class ApiVersioningConfig
{
    public static IServiceCollection ConfigureApiVersioning(this IServiceCollection services)
    {
        services.AddApiVersioning(opts =>
        {
            opts.AssumeDefaultVersionWhenUnspecified = true;
            opts.DefaultApiVersion = new ApiVersion(1);
            opts.ReportApiVersions = true;
            opts.ApiVersionReader = ApiVersionReader.Combine(
                new QueryStringApiVersionReader("api-version"),
                new HeaderApiVersionReader("api-version"),
                new UrlSegmentApiVersionReader()
            );
        }).AddApiExplorer(opts =>
        {
            opts.GroupNameFormat = "'v'V";
            opts.SubstituteApiVersionInUrl = true;
        });

        return services;
    }
}