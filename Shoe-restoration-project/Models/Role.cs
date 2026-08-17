using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shoe_restoration_project.Models
{
    [Table("Roles")]
    public class Role
    {
        [Key]
        [Column("Id", TypeName = "int")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("Name", TypeName = "nvarchar(64)")]
        public string Name { get; set; } = null!;

        public List<User> Users { get; set; } = new();
    }
}
