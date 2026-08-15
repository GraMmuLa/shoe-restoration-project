using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shoe_restoration_project.Models;

[Table("SizeMetrics")]
public partial class SizeMetric
{
    [Key]
    [Column("Id", TypeName = "int")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [Column("Name", TypeName = "nvarchar(2)")]
    public string Name { get; set; } = null!;

    public ICollection<Size> Sizes { get; set; } = new List<Size>();
}
