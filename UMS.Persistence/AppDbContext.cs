using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using UMS.Domain.Classes;
using UMS.Domain.Courses;
using UMS.Domain.Users;
using UMS.Persistence.Services;

namespace UMS.Persistence;

public class AppDbContext(
    DbContextOptions<AppDbContext> options,
    ITenantService _tenantService,
    IHttpContextAccessor _httpContextAccessor)
    : DbContext(options)
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var tenantId = _httpContextAccessor?.HttpContext?.Items["TenantId"] as string;
            if (!string.IsNullOrEmpty(tenantId) && _tenantService != null)
            {
                var connectionString = _tenantService.GetConnectionString(tenantId);
                optionsBuilder.UseNpgsql(connectionString);
            }
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // applying the config in the persistence assembly
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }


    public DbSet<Course> Courses { get; set; }
    public DbSet<Class> Classes { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<ClassEnrollment> ClassEnrollments { get; set; }
    public DbSet<Session> Sessions { get; set; }
}