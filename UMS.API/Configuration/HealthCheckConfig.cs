using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace UMS.API.Configuration;

public static class HealthCheckConfig
{
    public static WebApplication UseHealthChecks(this WebApplication app)
    {
        app.MapHealthChecks("healthz", new HealthCheckOptions
        {
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        });


        return app;
    }
}