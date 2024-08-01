using Microsoft.Extensions.Configuration;

namespace UMS.Persistence.Services;

public class TenantService(IConfiguration configuration) : ITenantService
{
    public string GetConnectionString(string tenantId)
    {
        return configuration.GetConnectionString($"Tenant{tenantId}");
    }
}