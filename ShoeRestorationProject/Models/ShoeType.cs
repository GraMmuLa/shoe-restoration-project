using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ShoeRestorationProject.Models;

[Table("ShoeTypes")]
public class ShoeType
{
    [Key]
    [Column("Id", TypeName = "int")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Column("Name", TypeName = "nvarchar(64)")]
    public string Name { get; set; } = null!;

    [Column("Description", TypeName = "nvarchar(5048)")]
    public string? Description { get; set; }

    public ICollection<Shoe> Shoes { get; set; } = new List<Shoe>();
}
