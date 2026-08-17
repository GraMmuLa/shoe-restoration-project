using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shoe_restoration_project.Models
{
    [Table("Users")]
    public class User
    {
        [Key]
        [Column("Id", TypeName = "int")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("Name", TypeName = "nvarchar(128)")]
        [Required]
        public string Username { get; set; } = null!;

        [Required]
        [Column("PasswordHash", TypeName = "nvarchar(256)")]
        public string PasswordHash { get; set; } = null!;

        [Required]
        [Column("Email", TypeName = "nvarchar(128)")]
        public string Email { get; set; } = null!;

        [Required]
        [Column("RoleId", TypeName = "int")]
        public int RoleId { get; set; }

        [ForeignKey("RoleId")]
        public Role Role { get; set; } = null!;
    }
}
