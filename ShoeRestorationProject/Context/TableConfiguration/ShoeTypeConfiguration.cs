using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoeRestorationProject.Models;

namespace ShoeRestorationProject.Context.TableConfiguration;

public class ShoeTypeConfiguration : IEntityTypeConfiguration<ShoeType>
{
    public void Configure(EntityTypeBuilder<ShoeType> builder)
    {
        builder.ToTable("ShoeTypes")
            .HasKey(p => p.Id);
        
        builder.Property(p => p.Name)
            .HasMaxLength(64)
            .IsRequired();

        builder.Property(p => p.Description)
            .HasMaxLength(2048);
        
        builder.HasIndex(p => p.Name).IsUnique();
    }
}