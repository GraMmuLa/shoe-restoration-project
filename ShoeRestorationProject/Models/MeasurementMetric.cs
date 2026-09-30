using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoeRestorationProject.Models;

[Table("MeasurementMetrics")]
public class MeasurementMetric
{
    [Key]
    [Column("Id", TypeName = "int")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Column("Name", TypeName = "nvarchar(8)")]
    public string Name { get; set; } = null!;

    public ICollection<MeasurementValue> MeasurementValues { get; set; } = new List<MeasurementValue>();
}
