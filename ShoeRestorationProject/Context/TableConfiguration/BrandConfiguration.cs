using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoeRestorationProject.Models;

namespace ShoeRestorationProject.Context.TableConfiguration;

public class BrandConfiguration : IEntityTypeConfiguration<Brand>
{
    public void Configure(EntityTypeBuilder<Brand> builder)
    {
        builder.ToTable("Brands")
            .HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .HasMaxLength(64)
            .IsRequired();

        builder.Property(p => p.Description)
            .HasMaxLength(64);

        builder.HasOne(p => p.Country)
            .WithMany(p => p.Brands)
            .HasForeignKey(p => p.CountryId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(p => p.Name).IsUnique();
    }
}