using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoeRestorationProject.Models;

[Table("Sizes")]
public class Size
{
    public int Id { get; set; }

    public decimal Value { get; set; }

    public int SizeMetricId { get; set; }
    public SizeMetric SizeMetric { get; set; } = null!;

    public int ShoeId { get; set; }
    public Shoe Shoe { get; set; } = null!;
}
