using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Shoe_restoration_project.Models;

[Table("ShoeTypes")]
public partial class ShoeType
{
    [Key]
    [Column("Id", TypeName = "int")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [Column("Name", TypeName = "nvarchar(64)")]
    public string Name { get; set; } = null!;

    [Column("Description", TypeName = "nvarchar(MAX)")]
    public string? Description { get; set; }

    public ICollection<Shoe> Shoes { get; set; } = new List<Shoe>();
}
