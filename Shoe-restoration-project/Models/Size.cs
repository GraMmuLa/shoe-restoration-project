using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shoe_restoration_project.Models;

[Table("Sizes")]
public partial class Size
{
    [Key]
    [Column("Id", TypeName = "int")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [Column("Name", TypeName = "decimal(3,1)")]
    public decimal Value { get; set; }

    [Column("SizeMetricId", TypeName = "int")]
    public int SizeMetricId { get; set; }

    public ICollection<Shoe> Shoes { get; set; } = new List<Shoe>();

    [ForeignKey("SizeMetricId")]
    public SizeMetric SizeMetric { get; set; } = null!;
}
