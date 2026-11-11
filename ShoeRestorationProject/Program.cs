using System.Security.Claims;
using System.Text;
using Mapster;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using Serilog;
using Serilog.Events;
using ShoeRestorationProject.Context;
using ShoeRestorationProject.Features.Brands;
using ShoeRestorationProject.Features.Colors;
using ShoeRestorationProject.Features.Conditions;
using ShoeRestorationProject.Features.Countries;
using ShoeRestorationProject.Features.Jwt;
using ShoeRestorationProject.Features.MeasurementMetrics;
using ShoeRestorationProject.Features.MeasurementProperties;
using ShoeRestorationProject.Features.Roles;
using ShoeRestorationProject.Features.ShoeImages;
using ShoeRestorationProject.Features.ShoeMeasurements;
using ShoeRestorationProject.Features.Shoes;
using ShoeRestorationProject.Features.ShoeTypes;
using ShoeRestorationProject.Features.SizeMetrics;
using ShoeRestorationProject.Features.Sizes;
using ShoeRestorationProject.Features.SkinTypes;
using ShoeRestorationProject.Features.Users;
using ShoeRestorationProject.Helpers;
using ShoeRestorationProject.Helpers.Implementations;
using ShoeRestorationProject.Repositories;
using ShoeRestorationProject.Repositories.Implementations;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.File(
        path: "Logs/log.txt",
        rollingInterval: RollingInterval.Day,
        retainedFileCountLimit: 30,
        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
    .WriteTo.Console(
        outputTemplate: "{Timestamp:HH:mm:ss} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

#region Adding Repositories

builder.Services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));

builder.Services.AddScoped<IShoesRepository, ShoesRepository>();

builder.Services.AddMapster();

#endregion

#region Adding Services

builder.Services.AddScoped<BrandService>();
builder.Services.AddScoped<ColorService>();
builder.Services.AddScoped<ConditionService>();
builder.Services.AddScoped<CountryService>();
builder.Services.AddScoped<MeasurementMetricService>();
builder.Services.AddScoped<MeasurementPropertyService>();
builder.Services.AddScoped<ShoeMeasurementService>();
builder.Services.AddScoped<ShoeImageService>();
builder.Services.AddScoped<ShoeService>();
builder.Services.AddScoped<ShoeTypeService>();
builder.Services.AddScoped<SizeMetricService>();
builder.Services.AddScoped<SizeService>();
builder.Services.AddScoped<SkinTypeService>();
builder.Services.AddScoped<RoleService>();
builder.Services.AddScoped<UserService>();

builder.Services.AddSingleton<IJwtTokenService, JwtTokenService>();

#endregion

#region Adding Helpers

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

#endregion

builder.Services.AddOptions<JwtOptions>().BindConfiguration(JwtOptions.Section)
    .Validate((options =>
            !string.IsNullOrWhiteSpace(options.Issuer) &&
            !string.IsNullOrWhiteSpace(options.Audience) &&
            !string.IsNullOrWhiteSpace(options.SecretKey) &&
            options.ExpirationMinutes > 0), "Invalid Jwt Configuration"
        ).ValidateOnStart();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateLifetime = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        
        RoleClaimType = ClaimTypes.Role,

        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]!))
    };
    
    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            context.Token = context.Request.Cookies["jwt-token"];
            return Task.CompletedTask;
        }
    };
});

builder.Services.AddAuthorization();
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddRouting(options => options.LowercaseUrls = true);

var app = builder.Build();

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.MapGet("/", () => Results.Redirect("scalar"));
app.MapControllers();
try
{
    Log.Information("Application starting");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly {ExMessage}", ex.Message);
    throw;
}
finally
{
    Log.CloseAndFlush();
}
