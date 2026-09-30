using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Scalar.AspNetCore;
using ShoeRestorationProject.Context;
using ShoeRestorationProject.Helpers;
using ShoeRestorationProject.Helpers.Implementations;
using ShoeRestorationProject.Repositories;
using ShoeRestorationProject.Repositories.Implementations;
using ShoeRestorationProject.Services;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Serilog;
using Serilog.Events;
using Mapster;

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
builder.Services.AddScoped<MeasurementValueService>();
builder.Services.AddScoped<ShoeImageService>();
builder.Services.AddScoped<ShoeService>();
builder.Services.AddScoped<ShoeTypeService>();
builder.Services.AddScoped<SizeMetricService>();
builder.Services.AddScoped<SizeService>();
builder.Services.AddScoped<SkinTypeService>();

#endregion

#region Adding Helpers

builder.Services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));

#endregion

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
{
    options.TokenValidationParameters = new()
    {
        ValidateIssuer = true,
        ValidateLifetime = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],

        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]!))
    };
});

builder.Services.AddAuthorization();

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddRouting(options => options.LowercaseUrls = true);

var app = builder.Build();

Log.Information("Application starting");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.MapGet("/", () => Results.Ok());
app.MapControllers();

Log.CloseAndFlush();
app.Run();
