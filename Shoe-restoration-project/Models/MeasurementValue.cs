using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shoe_restoration_project.Models;

[Table("MeasurementValues")]
public partial class MeasurementValue
{
    [Key]
    [Column("Id", TypeName = "int")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [Column("Value", TypeName = "decimal(3,1)")]
    public decimal Value { get; set; }

    [Column("MeasurementPropertyId", TypeName = "int")]
    public int MeasurementMetricId { get; set; }

    [ForeignKey("MeasurementMetricId")]
    public MeasurementMetric MeasurementMetric { get; set; } = null!;

    public ICollection<ShoeMeasurement> ShoeMeasurements { get; set; } = new List<ShoeMeasurement>();
}
