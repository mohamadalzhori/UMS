using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
namespace UMS.Persistence;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "..", "UMS.API"));
        Console.WriteLine(path); 
        // Assuming the presentation layer is at the root of the solution directory
        var configuration = new ConfigurationBuilder()
            .SetBasePath(path) // Adjust the path to the presentation layer
            .AddJsonFile("appsettings.json")
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        var connectionString = configuration.GetConnectionString("Tenant2");
        optionsBuilder.UseNpgsql(connectionString);

        return new AppDbContext(optionsBuilder.Options, null, null);
    }
}