using Serilog;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Feedback360.Api.Middleware;
using Feedback360.Application;
using Feedback360.Application.Contracts;
using Feedback360.Infrastructure;
using Feedback360.Persistence;
using Feedback360.Identity;
using Feedback360.Api.Services;
using Feedback360.Api.Extensions;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using Feedback360.Api.SwaggerHelper;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.DependencyInjection.Extensions;

var builder = WebApplication.CreateBuilder(args);



//SERILOG IMPLEMENTATION

IConfiguration configurationBuilder = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile(
        $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json",
        optional: true)
    .Build();

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configurationBuilder)
    .CreateBootstrapLogger().Freeze();

new LoggerConfiguration()
    .ReadFrom.Configuration(configurationBuilder)
    .CreateLogger();

builder.Host.UseSerilog((ctx, lc) => lc
        .WriteTo.Console()
        .ReadFrom.Configuration(ctx.Configuration));


// Add services to the container.

//readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";


IConfiguration Configuration = builder.Configuration;
string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var services = builder.Services;

string Urls = Configuration.GetSection("URLWhiteListings").GetSection("URLs").Value;
services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        builder =>
        {
            builder.WithOrigins(Urls).AllowAnyHeader().AllowAnyMethod();
        });
});
services.AddCors(c =>
{
    c.AddPolicy("AllowOrigin", options => options.WithOrigins("https://localhost:7207"));
});
services.AddApplicationServices();

services.AddInfrastructureServices(Configuration);
services.AddPersistenceServices(Configuration);
//services.AddIdentityServices(Configuration);
services.AddScoped<ILoggedInUserService, LoggedInUserService>();
services.AddScoped<ISendMailService, SendMailService>();
services.AddSwaggerExtension();
services.AddSwaggerVersionedApiExplorer();
services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
services.AddSwaggerGen(options => options.OperationFilter<SwaggerDefaultValues>());
services.AddControllers();
services.AddDataProtection()
    .PersistKeysToFileSystem(new DirectoryInfo(@"bin\debug\configuration"));
services.AddHealthcheckExtensionService(Configuration);
services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
ConfigurationManager configuration = builder.Configuration;
builder.Services.AddAuthentication(au =>
{
    au.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    au.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidIssuer = configuration["JwtSettings:Issuer"],
        ValidAudience = configuration["JwtSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]))
    };

});

var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    try
    {
        Log.Information("Application Starting");
    }
    catch (Exception ex)
    {
        Log.Warning(ex, "An error occured while starting the application");
    }
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseSwagger();

// Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),  
// specifying the Swagger JSON endpoint.  
IApiVersionDescriptionProvider provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

app.UseSwaggerUI(
options =>
{
                // build a swagger endpoint for each discovered API version  
    foreach (var description in provider.ApiVersionDescriptions)
    {
        options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
    }
});

app.UseCustomExceptionHandler();

app.UseCors("Open");
app.UseCors(options => options.WithOrigins("https://localhost:7207"));


app.UseAuthorization();
app.MapControllers();

//adding endpoint of health check for the health check ui in UI format
app.MapHealthChecks("/healthz", new HealthCheckOptions
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

//map healthcheck ui endpoing - default is /healthchecks-ui/
app.MapHealthChecksUI();

app.Run();

//For Integration test
public partial class Program { }
