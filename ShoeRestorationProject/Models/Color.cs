using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoeRestorationProject.Models;

[Table("Colors")]
public class Color
{
    [Key]
    [Column("Id", TypeName = "int")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Column("Name", TypeName = "nvarchar(64)")]
    public string Name { get; set; } = null!;

    public ICollection<Shoe> Shoes { get; set; } = new List<Shoe>();
}
