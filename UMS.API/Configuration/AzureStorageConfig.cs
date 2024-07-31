using UMS.API.AzureStorage;

namespace UMS.API.Configuration;

public static class AzureStorageConfig
{
    public static IServiceCollection AddAzureStorage(this IServiceCollection services, WebApplicationBuilder builder)
    {
        string fileStorageConnectionString = builder.Configuration.GetConnectionString("AzureBlobStorage");
        services.AddSingleton<IFileStorageService>(new AzureBlobStorageService(fileStorageConnectionString));
        return services;
    } 
}