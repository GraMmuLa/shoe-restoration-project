using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoeRestorationProject.Models;

namespace ShoeRestorationProject.Context.TableConfiguration;

public class ShoeConfiguration : IEntityTypeConfiguration<Shoe>
{
    public void Configure(EntityTypeBuilder<Shoe> builder)
    {
        builder.ToTable("Shoes")
            .HasKey(p => p.Id);

        builder.Property(p => p.Title)
            .HasMaxLength(64)
            .IsRequired();

        builder.Property(p => p.Description)
            .HasMaxLength(2048)
            .IsRequired();
        
        builder.HasOne(p => p.Brand)
            .WithMany(b => b.Shoes)
            .HasForeignKey(p => p.BrandId);
        
        builder.HasOne(p=> p.Color)
            .WithMany(c=> c.Shoes)
            .HasForeignKey(p => p.ColorId);
        
        builder.HasOne(p=> p.Condition)
            .WithMany(c=> c.Shoes)
            .HasForeignKey(p => p.ConditionId);

        builder.HasOne(p => p.SkinType)
            .WithMany(p => p.Shoes)
            .HasForeignKey(p=>p.SkinTypeId);

        builder.HasOne(p => p.ShoeType)
            .WithMany(p => p.Shoes)
            .HasForeignKey(p => p.ShoeTypeId);
    }
}