using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using UMS.Application;
using UMS.Common.Converters;
using Microsoft.AspNetCore.OData;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using Microsoft.OpenApi.Models;
using Serilog;
using UMS.API.Middlewares;
using UMS.Domain.Users;
using UMS.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Host.UseSerilog((context, configuration) => 
    configuration.ReadFrom.Configuration(context.Configuration));


builder.Services.AddApplication(builder.Configuration);
builder.Services.AddPersistence(builder.Configuration);

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new TimeOnlyJsonConverter());
}).AddOData(options =>
    options.Select().Filter().OrderBy().Expand().Count().AddRouteComponents("odata", GetEdmModel()));

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(options =>
{
    // Add a Swagger document for each discovered API version
    var provider =builder.Services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();
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


var app = builder.Build();

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
            options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
        }
    });
    
}

app.UseHttpsRedirection();

app.UseMiddleware<RequestContextLogging>(); // push correlationId before logging

app.UseSerilogRequestLogging();

app.UseAuthorization();

app.UseExceptionHandler();

app.MapControllers();

app.Run();


static IEdmModel GetEdmModel()
{
    var builder = new ODataConventionModelBuilder();
    builder.EntitySet<Student>(nameof(Student));
    return builder.GetEdmModel();
}