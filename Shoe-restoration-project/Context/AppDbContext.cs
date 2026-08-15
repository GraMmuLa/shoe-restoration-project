using Microsoft.EntityFrameworkCore;
using Shoe_restoration_project.Models;

namespace Shoe_restoration_project.Context;

public partial class AppDbContext : DbContext
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

    public virtual DbSet<MeasurementValue> MeasurementValues { get; set; }

    public virtual DbSet<Shoe> Shoes { get; set; }

    public virtual DbSet<ShoeImage> ShoeImages { get; set; }

    public virtual DbSet<ShoeMeasurement> ShoeMeasurements { get; set; }

    public virtual DbSet<ShoeType> ShoeTypes { get; set; }

    public virtual DbSet<Size> Sizes { get; set; }

    public virtual DbSet<SizeMetric> SizeMetrics { get; set; }

    public virtual DbSet<SkinType> SkinTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Brand>(entity =>
        {
            entity.HasIndex(e => e.Name, "IX_Brands_Name").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(128);

            entity.HasOne(d => d.Country).WithMany(p => p.Brands)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Brands_Countries");
        });

        modelBuilder.Entity<Color>(entity =>
        {
            entity.HasIndex(e => e.Name, "IX_Colors_Name").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(64);
        });

        modelBuilder.Entity<Condition>(entity =>
        {
            entity.HasIndex(e => e.Name, "IX_Conditions_Name").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(64);
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasIndex(e => e.IsoCode, "IX_Countries_IsoCode").IsUnique();

            entity.HasIndex(e => e.Name, "IX_Countries_Name").IsUnique();

            entity.Property(e => e.IsoCode)
                .HasMaxLength(2)
                .IsFixedLength();
            entity.Property(e => e.Name).HasMaxLength(64);
        });

        modelBuilder.Entity<MeasurementMetric>(entity =>
        {
            entity.HasIndex(e => e.Name, "IX_MeasurementMetrics_Name").IsUnique();

            entity.Property(e => e.Name)
                .HasMaxLength(8)
                .IsFixedLength();
        });

        modelBuilder.Entity<MeasurementProperty>(entity =>
        {
            entity.HasIndex(e => e.Name, "IX_MeasurementProperties_Name").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(64);
        });

        modelBuilder.Entity<MeasurementValue>(entity =>
        {
            entity.HasIndex(e => e.Value, "IX_MeasurementValues_Name").IsUnique();

            entity.Property(e => e.Value).HasColumnType("decimal(3, 1)");

            entity.HasOne(d => d.MeasurementMetric).WithMany(p => p.MeasurementValues)
                .HasForeignKey(d => d.MeasurementMetricId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Measurement_Values_Measurement_Metrics");
        });

        modelBuilder.Entity<Shoe>(entity =>
        {
            entity.HasIndex(e => e.BrandId, "IX_Shoes_BrandId");

            entity.HasIndex(e => e.Title, "IX_Shoes_Title").IsUnique();

            entity.Property(e => e.Title).HasMaxLength(64);

            entity.HasOne(d => d.Brand).WithMany(p => p.Shoes)
                .HasForeignKey(d => d.BrandId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Shoes_Brands");

            entity.HasOne(d => d.Color).WithMany(p => p.Shoes)
                .HasForeignKey(d => d.ColorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Shoes_Colors");

            entity.HasOne(d => d.Condition).WithMany(p => p.Shoes)
                .HasForeignKey(d => d.ConditionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Shoes_Conditions");

            entity.HasOne(d => d.ShoeType).WithMany(p => p.Shoes)
                .HasForeignKey(d => d.ShoeTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Shoes_Shoe_Types");

            entity.HasOne(d => d.Size).WithMany(p => p.Shoes)
                .HasForeignKey(d => d.SizeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Shoes_Sizes");

            entity.HasOne(d => d.SkinType).WithMany(p => p.Shoes)
                .HasForeignKey(d => d.SkinTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Shoes_Skin_Types");
        });

        modelBuilder.Entity<ShoeImage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Shoe_Ima__3214EC07501FBD4C");

            entity.HasIndex(e => e.Name, "IX_ShoeImages_Name").IsUnique();

            entity.HasIndex(e => e.ShoeId, "IX_ShoeImages_ShoeId");

            entity.Property(e => e.Id).HasDefaultValueSql("(newsequentialid())", "DF_ShoeImages_Id");
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.ImageData).HasColumnName("ImageData");

            entity.HasOne(d => d.Shoe).WithMany(p => p.ShoeImages)
                .HasForeignKey(d => d.ShoeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Shoe_Images_Shoes");
        });

        modelBuilder.Entity<ShoeMeasurement>(entity =>
        {
            entity.HasOne(d => d.MeasurementProperty).WithMany(p => p.ShoeMeasurements)
                .HasForeignKey(d => d.MeasurementPropertyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Shoe_Measurements_Measurement_Properties");

            entity.HasOne(d => d.MeasurementValue).WithMany(p => p.ShoeMeasurements)
                .HasForeignKey(d => d.MeasurementValueId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Shoe_Measurements_Measurement_Values");

            entity.HasOne(d => d.Shoe).WithMany(p => p.ShoeMeasurements)
                .HasForeignKey(d => d.ShoeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Shoe_Measurements_Shoes");
        });

        modelBuilder.Entity<ShoeType>(entity =>
        {
            entity.HasIndex(e => e.Name, "IX_ShoeTypes_Name").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(64);
        });

        modelBuilder.Entity<Size>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Shoe_Sizes");

            entity.HasIndex(e => e.Value, "IX_Sizes_Name").IsUnique();

            entity.Property(e => e.Value).HasColumnType("decimal(3, 1)");

            entity.HasOne(d => d.SizeMetric).WithMany(p => p.Sizes)
                .HasForeignKey(d => d.SizeMetricId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sizes_Size_Metrics");
        });

        modelBuilder.Entity<SizeMetric>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Metrics");

            entity.HasIndex(e => e.Name, "IX_SizeMetrics_Name").IsUnique();

            entity.Property(e => e.Name)
                .HasMaxLength(2)
                .IsFixedLength();
        });

        modelBuilder.Entity<SkinType>(entity =>
        {
            entity.HasIndex(e => e.Name, "IX_SkinTypes_Name").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(64);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
