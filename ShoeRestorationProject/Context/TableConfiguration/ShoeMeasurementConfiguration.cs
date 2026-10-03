using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoeRestorationProject.Models;

namespace ShoeRestorationProject.Context.TableConfiguration;

public class ShoeMeasurementConfiguration : IEntityTypeConfiguration<ShoeMeasurement>
{
    public void Configure(EntityTypeBuilder<ShoeMeasurement> builder)
    {
        builder.ToTable("ShoeMeasurements")
            .HasKey(p => p.Id);
        
        builder.Property(p => p.Value)
            .HasMaxLength(2)
            .IsRequired();

        builder.HasOne(m => m.Shoe)
            .WithMany(s => s.ShoeMeasurements);
        
        builder.HasOne(m => m.MeasurementProperty)
            .WithMany(p => p.ShoeMeasurements);
        
        builder.HasOne(m => m.MeasurementMetric)
            .WithMany(m => m.ShoeMeasurements);
    }
}