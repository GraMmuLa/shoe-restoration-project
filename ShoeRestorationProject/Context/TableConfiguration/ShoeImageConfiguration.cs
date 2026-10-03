using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoeRestorationProject.Models;

namespace ShoeRestorationProject.Context.TableConfiguration;

public class ShoeImageConfiguration : IEntityTypeConfiguration<ShoeImage>
{
    public void Configure(EntityTypeBuilder<ShoeImage> builder)
    {
        builder.ToTable("ShoeImages")
            .HasKey(p => p.Id);

        builder.Property(p=>p.Name)
            .HasMaxLength(255)
            .IsRequired();
        
        builder.Property(p => p.ImageData)
            .HasMaxLength(256)
            .IsRequired();
        
        builder.HasOne(p => p.Shoe)
            .WithMany(p => p.ShoeImages)
            .HasForeignKey(p => p.ShoeId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasIndex(p => p.Name).IsUnique();
    }
}