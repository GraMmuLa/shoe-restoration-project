using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shoe_restoration_project.Models;

[Table("ShoeMeasurements")]
public partial class ShoeMeasurement
{
    [Key]
    [Column("Id", TypeName = "int")]
    public int Id { get; set; }

    [Column("ShoeId", TypeName = "int")]
    public int ShoeId { get; set; }

    [Column("MeasurementPropertyId", TypeName = "int")]
    public int MeasurementPropertyId { get; set; }

    [Column("MeasurementValueId", TypeName = "int")]
    public int MeasurementValueId { get; set; }

    [ForeignKey("MeasurementPropertyId")]
    public MeasurementProperty MeasurementProperty { get; set; } = null!;

    [ForeignKey("MeasurementValueId")]
    public MeasurementValue MeasurementValue { get; set; } = null!;

    [ForeignKey("ShoeId")]
    public Shoe Shoe { get; set; } = null!;
}
