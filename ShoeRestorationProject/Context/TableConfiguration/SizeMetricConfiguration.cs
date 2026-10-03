using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoeRestorationProject.Models;

namespace ShoeRestorationProject.Context.TableConfiguration;

public class SizeMetricConfiguration : IEntityTypeConfiguration<SizeMetric>
{
    public void Configure(EntityTypeBuilder<SizeMetric> builder)
    {
        builder.ToTable("SizeMetrics")
            .HasKey(p => p.Id);
        
        builder.Property(p => p.Name)
            .HasMaxLength(64)
            .IsRequired();
        
        builder.HasIndex(p => p.Name).IsUnique();
    }
}