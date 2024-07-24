using System.Globalization;
using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using Hangfire;
using Hangfire.PostgreSql;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Localization;
using UMS.Application;
using UMS.Common.Converters;
using Microsoft.AspNetCore.OData;
using Microsoft.Extensions.Options;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using Microsoft.OpenApi.Models;
using Serilog;
using UMS.API.Middlewares;
using UMS.Domain.Users;
using UMS.Persistence;
using UMS.Infrastructure.Email;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddLocalization();
builder.Services.Configure<RequestLocalizationOptions>(
    options =>
    {
        var supportedCultures = new List<CultureInfo>
        {
            new CultureInfo("en-US"),
            new CultureInfo("en-AU"),
            new CultureInfo("de-DE")
        };

        options.DefaultRequestCulture = new RequestCulture(culture: "en-US", uiCulture: "en-US");
        options.SupportedCultures = supportedCultures;
        options.SupportedUICultures = supportedCultures;
    });


builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));


builder.Services.AddApplication(builder.Configuration);
builder.Services.AddPersistence(builder.Configuration);

builder.Services.AddControllers()
    .AddJsonOptions(options => { options.JsonSerializerOptions.Converters.Add(new TimeOnlyJsonConverter()); })
    .AddOData(options =>
        options.Select().Filter().OrderBy().Expand().Count().AddRouteComponents("odata", GetEdmModel()));







builder.Services.AddExceptionHandler<GlobalExceptionHandler>();


builder.Services.AddProblemDetails();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(options =>
{
    // Add a Swagger document for each discovered API version
    var provider = builder.Services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();
    foreach (var description in provider.ApiVersionDescriptions)
    {
        options.SwaggerDoc(description.GroupName, new OpenApiInfo()
        {
            Title = $"API {description.ApiVersion}",
            Version = description.ApiVersion.ToString()
        });
    }
});


builder.Services.AddApiVersioning(opts =>
{
    opts.AssumeDefaultVersionWhenUnspecified = true;
    opts.DefaultApiVersion = new ApiVersion(1);
    opts.ReportApiVersions = true;
    opts.ApiVersionReader = ApiVersionReader.Combine(
        new QueryStringApiVersionReader("api-version"),
        new HeaderApiVersionReader("api-version"),
        new UrlSegmentApiVersionReader()
    );
}).AddApiExplorer(opts =>
{
    opts.GroupNameFormat = "'v'V";
    opts.SubstituteApiVersionInUrl = true;
});

builder.Services.AddHangfire(config =>
    config.UsePostgreSqlStorage(x =>
        x.UseNpgsqlConnection(builder.Configuration.GetConnectionString("DefaultConnection"))));

builder.Services.AddHangfireServer();

var app = builder.Build();
var localizeOptions = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
app.UseRequestLocalization(localizeOptions.Value);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    // app.UseSwaggerUI();
    app.UseSwaggerUI(options =>
    {
        // Build a swagger endpoint for each discovered API version
        var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
        foreach (var description in provider.ApiVersionDescriptions)
        {
            options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                description.GroupName.ToUpperInvariant());
        }
    });
}

app.UseHttpsRedirection();

app.UseMiddleware<RequestContextLogging>(); // push correlationId before logging

app.UseSerilogRequestLogging();


app.UseAuthorization();

app.UseExceptionHandler();

app.MapControllers();

app.UseHangfireDashboard("/jobs");

RecurringJob.AddOrUpdate("Daily test message", () => EmailSender.Send("2d5edbec5d@emaildbox.pro", "test", "test"),
    Cron.Daily(12, 0));

app.MapHealthChecks("healthz", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.Run();


static IEdmModel GetEdmModel()
{
    var builder = new ODataConventionModelBuilder();
    builder.EntitySet<Student>(nameof(Student));
    return builder.GetEdmModel();
}