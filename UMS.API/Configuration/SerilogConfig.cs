using Serilog;

namespace UMS.API.Configuration;

public static class SerilogConfig
{
   public static WebApplicationBuilder UseSerilog(this WebApplicationBuilder builder)
   {
      builder.Host.UseSerilog((context, configuration) =>
      {
         configuration.ReadFrom.Configuration(context.Configuration);
      });

      return builder;
   } 
}