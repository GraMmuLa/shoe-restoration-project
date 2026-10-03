using System.Reflection;
using Microsoft.EntityFrameworkCore;
using ShoeRestorationProject.Models;

namespace ShoeRestorationProject.Context;

public class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Brand> Brands { get; set; }

    public virtual DbSet<Color> Colors { get; set; }

    public virtual DbSet<Condition> Conditions { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<MeasurementMetric> MeasurementMetrics { get; set; }

    public virtual DbSet<MeasurementProperty> MeasurementProperties { get; set; }

    public virtual DbSet<ShoeMeasurement> MeasurementValues { get; set; }

    public virtual DbSet<Shoe> Shoes { get; set; }

    public virtual DbSet<ShoeImage> ShoeImages { get; set; }

    public virtual DbSet<ShoeMeasurement> ShoeMeasurements { get; set; }

    public virtual DbSet<ShoeType> ShoeTypes { get; set; }

    public virtual DbSet<Size> Sizes { get; set; }

    public virtual DbSet<SizeMetric> SizeMetrics { get; set; }

    public virtual DbSet<SkinType> SkinTypes { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
