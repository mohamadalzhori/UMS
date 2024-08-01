namespace UMS.Persistence.Services;

public interface ITenantService
{
    string GetConnectionString(string tenantId); 
}