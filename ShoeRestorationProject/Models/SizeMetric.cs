using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoeRestorationProject.Models;

[Table("SizeMetrics")]
public class SizeMetric
{
    [Key]
    [Column("Id", TypeName = "int")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Column("Name", TypeName = "nvarchar(2)")]
    public string Name { get; set; } = null!;

    public ICollection<Size> Sizes { get; set; } = new List<Size>();
}
