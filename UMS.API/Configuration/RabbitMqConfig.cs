using UMS.API.RabbitMQ;

namespace UMS.API.Configuration;

public static class RabbitMqConfig
{
   public static IServiceCollection ConfigureRabbitMq(this IServiceCollection services)
   {
       services.AddSingleton<IRabbitMqProducer, RabbitMqProducer>();

       return services;
   } 
}