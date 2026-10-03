using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoeRestorationProject.Models;

namespace ShoeRestorationProject.Context.TableConfiguration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users")
            .HasKey(p => p.Id);
        
        builder.Property(p=>p.Username)
            .HasMaxLength(128)
            .IsRequired();
        
        builder.Property(p=>p.PasswordHash)
            .HasMaxLength(256)
            .IsRequired();

        builder.Property(p => p.Email)
            .HasMaxLength(128)
            .IsRequired();
        
        builder.HasOne(p=>p.Role)
            .WithMany(p=>p.Users)
            .HasForeignKey(p=>p.RoleId);
        
        builder.HasIndex(p=>p.Username).IsUnique();
    }
}