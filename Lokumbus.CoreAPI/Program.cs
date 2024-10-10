using FluentValidation;
using FluentValidation.AspNetCore;
using Lokumbus.CoreAPI.Configuration.Mapping;
using Lokumbus.CoreAPI.Configuration.Validators;
using Lokumbus.CoreAPI.Repositories;
using Lokumbus.CoreAPI.Repositories.Interfaces;
using Lokumbus.CoreAPI.Services;
using Lokumbus.CoreAPI.Services.Interfaces;
using Mapster;
using MongoDB.Driver;

// Create a builder for the WebApplication
var builder = WebApplication.CreateBuilder(args);

// =====================================
// Logging Configuration
// =====================================
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// =====================================
// Configuration Settings
// =====================================

// The existing appsettings.json already contains MongoDbSettings and KafkaSettings.
// These will be accessed via the configuration system.

// =====================================
// MongoDB Configuration
// =====================================

// Register MongoClient as a singleton
builder.Services.AddSingleton<IMongoClient, MongoClient>(_ =>
{
    var mongoSettings = builder.Configuration.GetSection("MongoDbSettings");
    var connectionString = mongoSettings.GetValue<string>("ConnectionString");
    return new MongoClient(connectionString);
});

// Register IMongoDatabase as a singleton
builder.Services.AddSingleton<IMongoDatabase>(sp =>
{
    var mongoSettings = builder.Configuration.GetSection("MongoDbSettings");
    var databaseName = mongoSettings.GetValue<string>("DatabaseName");
    var client = sp.GetRequiredService<IMongoClient>();
    return client.GetDatabase(databaseName);
});

// =====================================
// Repository Registration
// =====================================

// Register all repositories
builder.Services.AddScoped<IAppUserRepository, AppUserRepository>();
builder.Services.AddScoped<IPersonaRepository, PersonaRepository>();
builder.Services.AddScoped<ILocationRepository, LocationRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
// Add other repository registrations here as needed

// =====================================
// Service Registration
// =====================================

// Register all services
builder.Services.AddScoped<IAppUserService, AppUserService>();
builder.Services.AddScoped<IPersonaService, PersonaService>();
builder.Services.AddScoped<ILocationService, LocationService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
// Add other service registrations here as needed

// =====================================
// FluentValidation Configuration
// =====================================

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<CreatePersonaDtoValidator>();

// =====================================
// Mapster Configuration
// =====================================
// Mapster Configuration
// Mapster Configuration
var config = TypeAdapterConfig.GlobalSettings;
MapsterConfiguration.RegisterMappings(config);
builder.Services.AddSingleton(config);


// =====================================
// Controllers
// =====================================

builder.Services.AddControllers();

// =====================================
// Swagger/OpenAPI Configuration
// =====================================

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// =====================================
// CORS Configuration (Optional)
// =====================================

// If your API needs to be accessed from different origins, configure CORS here.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder=>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

// =====================================
// Build the Application
// =====================================

var app = builder.Build();

// =====================================
// Configure the HTTP Request Pipeline
// =====================================

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Use HTTPS Redirection Middleware
app.UseHttpsRedirection();

// Use CORS Middleware (if configured)
app.UseCors("AllowAll");

// Use Authorization Middleware
app.UseAuthorization();

// Map Controllers
app.MapControllers();

// Run the application
app.Run();