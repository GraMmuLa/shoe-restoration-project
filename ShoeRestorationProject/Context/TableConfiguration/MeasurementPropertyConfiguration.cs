using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoeRestorationProject.Models;

namespace ShoeRestorationProject.Context.TableConfiguration;

public class MeasurementPropertyConfiguration : IEntityTypeConfiguration<MeasurementProperty>
{
    public void Configure(EntityTypeBuilder<MeasurementProperty> builder)
    {
        builder.ToTable("MeasurementProperties")
            .HasKey(p => p.Id);
        
        builder.Property(p => p.Name)
            .HasMaxLength(64)
            .IsRequired();
        
        builder.HasIndex(p => p.Name).IsUnique();
    }
}