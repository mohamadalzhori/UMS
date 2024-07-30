using Microsoft.AspNetCore.OData;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using UMS.Domain.Users;

namespace UMS.API.Configuration;

public static class OdataConfig
{
    public static IServiceCollection AddOdata(this IServiceCollection services)
    {
        services.AddControllers().AddOData(options =>
            options.Select().Filter().OrderBy().Expand().Count().AddRouteComponents("odata", GetEdmModel()));

        return services;
    }

    static IEdmModel GetEdmModel()
    {
        var builder = new ODataConventionModelBuilder();
        builder.EntitySet<Student>(nameof(Student));
        return builder.GetEdmModel();
    }
}