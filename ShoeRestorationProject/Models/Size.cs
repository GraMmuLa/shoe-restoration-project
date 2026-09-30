using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoeRestorationProject.Models;

[Table("Sizes")]
public class Size
{
    [Key]
    [Column("Id", TypeName = "int")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Column("Name", TypeName = "decimal(3,1)")]
    public decimal Value { get; set; }

    [Column("SizeMetricId", TypeName = "int")]
    public int SizeMetricId { get; set; }

    public ICollection<Shoe> Shoes { get; set; } = new List<Shoe>();

    [ForeignKey("SizeMetricId")]
    public SizeMetric SizeMetric { get; set; } = null!;
}
