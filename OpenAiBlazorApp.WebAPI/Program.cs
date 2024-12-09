using OpenAiBlazorApp.WebAPI;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using OpenAiBlazorApp.Application.Services;
using OpenAiBlazorApp.Application.Settings;
using OpenAiBlazorApp.Core.Interfaces;
using OpenAiBlazorApp.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using AutoMapper; // Add this using directive
using OpenAiBlazorApp.Application.Mappings; // Add this using directive

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Add API Versioning
builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ReportApiVersions = true;
});

// Add Versioned API Explorer
builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

// Configure MongoDB
var mongoClient = new MongoClient(builder.Configuration.GetConnectionString("MongoDb"));
var database = mongoClient.GetDatabase("PersonalFinancialDB");
builder.Services.AddSingleton(database);

// Register repositories and services
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<CategoryService>();

// Bind JWT settings from configuration
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

// Register JwtTokenService
builder.Services.AddScoped<JwtTokenService>();

builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

// Retrieve JWT settings from configuration
var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();
var secretKey = jwtSettings!.SecretKey;
var validIssuer = jwtSettings.ValidIssuer;
var validAudience = jwtSettings.ValidAudience;

if (string.IsNullOrEmpty(secretKey) || string.IsNullOrEmpty(validIssuer) || string.IsNullOrEmpty(validAudience))
{
    throw new InvalidOperationException("JWT settings are not configured properly.");
}

var key = Encoding.ASCII.GetBytes(secretKey);

// Configure JWT authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = validIssuer,
        ValidAudience = validAudience,
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

// Add Swagger generator
builder.Services.AddSwaggerGen();

// Configure Swagger options
builder.Services.ConfigureOptions<ConfigureSwaggerOptions>();

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder
            //.WithOrigins("http://localhost:5153/") // Replace with your allowed origin(s)
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod());
});

var app = builder.Build();

// Use CORS policy
app.UseCors("AllowSpecificOrigin");

// Get the provider
var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

// Enable Swagger middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI(options =>
    {
        foreach (var description in provider.ApiVersionDescriptions)
        {
            options.SwaggerEndpoint(
                $"/swagger/{description.GroupName}/swagger.json",
                $"OpenAiBlazorApp API {description.ApiVersion}"
            );
        }
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
