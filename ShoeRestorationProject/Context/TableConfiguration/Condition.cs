using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoeRestorationProject.Models;

namespace ShoeRestorationProject.Context.TableConfiguration;

public class ConditionConfiguration : IEntityTypeConfiguration<Condition>
{
    public void Configure(EntityTypeBuilder<Condition> builder)
    {
        builder.ToTable("Conditions")
            .HasKey(p => p.Id);
        
        builder.Property(p => p.Name)
            .HasMaxLength(64)
            .IsRequired();
        
        builder.HasIndex(p => p.Name).IsUnique();
    }
}