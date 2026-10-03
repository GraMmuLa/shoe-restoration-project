using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoeRestorationProject.Models;

namespace ShoeRestorationProject.Context.TableConfiguration;

public class SizeConfiguration : IEntityTypeConfiguration<Size>
{
    public void Configure(EntityTypeBuilder<Size> builder)
    {
        builder.ToTable("Sizes")
            .HasKey(p => p.Id);
        
        builder.Property(p => p.Value)
            .HasColumnType("decimal(3,1)")
            .IsRequired();
        
        builder.HasOne(p => p.SizeMetric)
            .WithMany(p => p.Sizes)
            .HasForeignKey(p => p.SizeMetricId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(p=> p.Shoe)
            .WithOne(s=> s.Size)
            .HasForeignKey<Size>(p => p.ShoeId);
        
        builder.HasIndex(p => p.Value).IsUnique();
    }
}