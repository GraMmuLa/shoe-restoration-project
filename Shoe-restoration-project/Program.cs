using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Shoe_restoration_project.Context;
using Shoe_restoration_project.Helpers;
using Shoe_restoration_project.Helpers.Implementations;
using Shoe_restoration_project.Repositories;
using Shoe_restoration_project.Repositories.Implementation;
using Shoe_restoration_project.Repositories.Implementations;
using Shoe_restoration_project.Services;
using Shoe_restoration_project.Services.Implementations;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

#region Adding Repositories

builder.Services.AddScoped<IBrandsRepository, BrandsRepository>();
builder.Services.AddScoped<IColorsRepository, ColorsRepository>();
builder.Services.AddScoped<IConditionsRepository, ConditionsRepository>();
builder.Services.AddScoped<ICountriesRepository, CountriesRepository>();
builder.Services.AddScoped<IMeasurementMetricsRepository, MeasurementMetricsRepository>();
builder.Services.AddScoped<IMeasurementPropertiesRepository, MeasurementPropertiesRepository>();
builder.Services.AddScoped<IMeasurementValuesRepository, MeasurementValuesRepository>();
builder.Services.AddScoped<IShoeImagesRepository, ShoeImagesRepository>();
builder.Services.AddScoped<IShoesRepository, ShoesRepository>();
builder.Services.AddScoped<IShoeTypesRepository, ShoeTypesRepository>();
builder.Services.AddScoped<ISizeMetricsRepository, SizeMetricsRepository>();
builder.Services.AddScoped<ISizesRepository, SizesRepository>();
builder.Services.AddScoped<ISkinTypesRepository, SkinTypesRepository>();
builder.Services.AddScoped<IShoeMeasurementsRepository, ShoeMeasurementsRepository>();

#endregion

#region Adding Services

builder.Services.AddScoped<IBrandsService, BrandsService>();
builder.Services.AddScoped<IColorsService, ColorsService>();
builder.Services.AddScoped<IConditionsService, ConditionsService>();
builder.Services.AddScoped<ICountriesService, CountriesService>();
builder.Services.AddScoped<IMeasurementMetricsService, MeasurementMetricsService>();
builder.Services.AddScoped<IMeasurementPropertiesService, MeasurementPropertiesService>();
builder.Services.AddScoped<IMeasurementValuesService, MeasurementValuesService>();
builder.Services.AddScoped<IShoeImagesService, ShoeImagesService>();
builder.Services.AddScoped<IShoesService, ShoesService>();
builder.Services.AddScoped<IShoeTypesService, ShoeTypesService>();
builder.Services.AddScoped<ISizeMetricsService, SizeMetricsService>();
builder.Services.AddScoped<ISizesService, SizesService>();
builder.Services.AddScoped<ISkinTypesService, SkinTypesService>();
builder.Services.AddScoped<IShoeMeasurementsService, ShoeMeasurementsService>();

#endregion

#region Adding Helpers

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

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
builder.Services.AddRouting(options => options.LowercaseUrls = true);

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/", () => Results.Ok());
app.MapControllers();

app.Run();
