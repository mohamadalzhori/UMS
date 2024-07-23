using Microsoft.EntityFrameworkCore.Diagnostics;
using Serilog.Context;

namespace UMS.API.Middlewares;

public class RequestContextLogging(RequestDelegate _next)
{
    // to allow tracking across multiple services
    
    private const string CorrelationIdHeader = "X-Correlation-Id";
    
    public Task Invoke(HttpContext httpContext)
    {
        using (LogContext.PushProperty("CorrelationId", GetCorrelationId(httpContext)))
        {
            return _next(httpContext);
        }
    }

    private static string GetCorrelationId(HttpContext httpContext)
    {
       // if exists keep it, else generate one 

       httpContext.Request.Headers.TryGetValue(CorrelationIdHeader, out var correlationId);

       return correlationId.FirstOrDefault() ?? httpContext.TraceIdentifier;
    }
}