using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoeRestorationProject.Models;

namespace ShoeRestorationProject.Context.TableConfiguration;

public class MeasurementMetricConfiguration : IEntityTypeConfiguration<MeasurementMetric>
{
    public void Configure(EntityTypeBuilder<MeasurementMetric> builder)
    {
        builder.ToTable("MeasurementMetrics")
            .HasKey(p => p.Id);
        
        builder.Property(p => p.Name)
            .HasMaxLength(64)
            .IsRequired();

        builder.HasIndex(p => p.Name).IsUnique();
    }
}