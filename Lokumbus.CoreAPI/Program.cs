using System.Text;
using FluentValidation;
using FluentValidation.AspNetCore;
using Lokumbus.CoreAPI.Configuration.Bootstrapping;
using Lokumbus.CoreAPI.Configuration.Mapping;
using Lokumbus.CoreAPI.Configuration.Mapping.Converters;
using Lokumbus.CoreAPI.Configuration.Validators;
using Lokumbus.CoreAPI.Configuration.Validators.Auth;
using Lokumbus.CoreAPI.Configuration.Validators.Create;
using Lokumbus.CoreAPI.Repositories;
using Lokumbus.CoreAPI.Repositories.Interfaces;
using Lokumbus.CoreAPI.Services;
using Lokumbus.CoreAPI.Services.Interfaces;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
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
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IAreaRepository, AreaRepository>();
builder.Services.AddScoped<IActivityRepository, ActivityRepository>();
builder.Services.AddScoped<IAlertRepository, AlertRepository>();
builder.Services.AddScoped<ICalendarRepository, CalendarRepository>();
builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<ICalendarEventAttendeeRepository, CalendarEventAttendeeRepository>();
builder.Services.AddScoped<IAlertMessageRepository, AlertMessageRepository>();
builder.Services.AddScoped<IChatMessageRepository, ChatMessageRepository>();
builder.Services.AddScoped<IDiscountRepository, DiscountRepository>();
builder.Services.AddScoped<IFriendshipRepository, FriendshipRepository>();
builder.Services.AddScoped<IInterestRepository, InterestRepository>();
// Add other repository registrations here as needed

// =====================================
// Service Registration
// =====================================

// Register all services
builder.Services.AddScoped<IAppUserService, AppUserService>();
builder.Services.AddScoped<IPersonaService, PersonaService>();
builder.Services.AddScoped<ILocationService, LocationService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IAreaService, AreaService>();
builder.Services.AddScoped<IActivityService, ActivityService>();
builder.Services.AddScoped<IAlertService, AlertService>();
builder.Services.AddScoped<ICalendarService, CalendarService>();
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<ICalendarEventAttendeeService, CalendarEventAttendeeService>();
builder.Services.AddScoped<IAlertMessageService, AlertMessageService>();
builder.Services.AddScoped<IChatMessageService, ChatMessageService>();
builder.Services.AddScoped<IDiscountService, DiscountService>();
builder.Services.AddScoped<IFriendshipService, FriendshipService>();
builder.Services.AddScoped<IInterestService, InterestService>();
// Add other service registrations here as needed

// =====================================
// FluentValidation Configuration
// =====================================

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<CreateAuthDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<LoginDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<TokenDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateAlertDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateCalendarDtoValidator>();

// =====================================
// Mapster Configuration
// =====================================

// Configure Mapster with the MappingProfile
var config = new TypeAdapterConfig();
config.Scan(typeof(DictionaryStringObjectConverter).Assembly);
config.Apply(new DictionaryStringObjectConverter());
MapsterConfiguration.RegisterMappings(config);
builder.Services.AddSingleton(config);



// =====================================
// JWT Authentication Configuration
// =====================================

var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = jwtSettings.GetValue<string>("SecretKey");

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = true;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings.GetValue<string>("Issuer"),
            ValidAudience = jwtSettings.GetValue<string>("Audience"),
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
            ClockSkew = TimeSpan.Zero
        };
    });


// =====================================
// Controllers
// =====================================

builder.Services.AddControllers();

// =====================================
// Swagger/OpenAPI Configuration
// =====================================

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    // JWT-Token in Swagger integrieren
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
        Name = "Authorization",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Scheme = "Bearer",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        BearerFormat = "JWT"
    });

    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// =====================================
// CORS Configuration (Optional)
// =====================================

// If your API needs to be accessed from different origins, configure CORS here.
// Example: Allow all origins (not recommended for production)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

// =====================================
// Hosted Services (Optional)
// =====================================

// Beispiel: Hosted Service für Kafka-Topic-Bootstrap
builder.Services.AddHostedService<BootstrapKafka>();

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