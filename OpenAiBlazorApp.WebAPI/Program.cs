
using Microsoft.Extensions.Options;
using OpenAiBlazorApp.WebAPI;
using OpenAiBlazorApp.WebAPI.Models;
using OpenAiBlazorApp.WebAPI.Services;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<PerFinsDatabaseSettings>(
    builder.Configuration.GetSection(nameof(PerFinsDatabaseSettings)));

builder.Services.AddSingleton<IPerFinsDatabaseSettings>(sp =>
    sp.GetRequiredService<IOptions<PerFinsDatabaseSettings>>().Value);

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

builder.Services.AddSingleton<CashFlowStatementService>();

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
