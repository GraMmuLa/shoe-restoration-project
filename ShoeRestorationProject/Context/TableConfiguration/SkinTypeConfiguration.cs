using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoeRestorationProject.Models;

namespace ShoeRestorationProject.Context.TableConfiguration;

public class SkinTypeConfiguration : IEntityTypeConfiguration<SkinType>
{
    public void Configure(EntityTypeBuilder<SkinType> builder)
    {
        builder.ToTable("SkinTypes")
            .HasKey(p => p.Id);
        
        builder.Property(p => p.Name)
            .HasMaxLength(64)
            .IsRequired();
        
        builder.HasIndex(p => p.Name).IsUnique();
    }
}