using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoeRestorationProject.Models;

namespace ShoeRestorationProject.Context.TableConfiguration;

public class ColorConfiguration : IEntityTypeConfiguration<Color>
{
    public void Configure(EntityTypeBuilder<Color> builder)
    {
        builder.ToTable("Colors")
            .HasKey(p => p.Id);
        
        builder.Property(p => p.Name)
            .HasMaxLength(64)
            .IsRequired();

        builder.HasIndex(p => p.Name).IsUnique();
    }
}