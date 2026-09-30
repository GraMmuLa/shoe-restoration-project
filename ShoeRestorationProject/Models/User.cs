using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoeRestorationProject.Models
{
    [Table("Users")]
    public class User
    {
        [Key]
        [Column("Id", TypeName = "int")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("Name", TypeName = "nvarchar(128)")]
        public string Username { get; set; } = null!;

        [Column("PasswordHash", TypeName = "nvarchar(256)")]
        public string PasswordHash { get; set; } = null!;

        [Column("Email", TypeName = "nvarchar(128)")]
        public string Email { get; set; } = null!;

        [Column("RoleId", TypeName = "int")]
        public int RoleId { get; set; }

        [ForeignKey("RoleId")]
        public Role Role { get; set; } = null!;
    }
}
