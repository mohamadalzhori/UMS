using Hangfire;
using UMS.Application;
using Serilog;
using UMS.API.Configuration;
using UMS.API.Middlewares;
using UMS.Persistence;
using UMS.Infrastructure.Email;

var builder = WebApplication.CreateBuilder(args);

// Serilog
builder.UseSerilog();

// Application Layer
builder.Services.AddApplication(builder.Configuration);

// Persistence Layer
builder.Services.AddPersistence(builder.Configuration);

// Custom Json Serializer for TimeOnly
builder.Services.AddCustomJsonSerializer();

// Odata
builder.Services.AddOdata();

// Global Exception Handler
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

// Swagger
builder.Services.AddSwagger();

// Api Versioning
builder.Services.ConfigureApiVersioning();

// Hangfire
builder.Services.ConfigureHangfire(builder);

// Keycloak
builder.Services.AddKeycloak();

// Azure Storage for Download
builder.Services.AddAzureStorage(builder);

// RabbitMQ
builder.Services.ConfigureRabbitMq();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.ConfigureSwaggerUi();
}

app.UseHttpsRedirection();

// Correlation ID
app.UseMiddleware<RequestContextLogging>();

// Serilog Request Logging
app.UseSerilogRequestLogging();


app.UseAuthentication();
app.UseAuthorization();

// Exception Handling
app.UseExceptionHandler();

app.MapControllers();

// Hangfire
app.UseHangfireDashboard("/jobs");


// Background Jobs Test
// RecurringJob.AddOrUpdate("Daily test message", () => EmailSender.Send("2d5edbec5d@emaildbox.pro", "test", "testis"),
    // Cron.Daily(12, 0));

// Health Checks
app.UseHealthChecks();

app.Run();