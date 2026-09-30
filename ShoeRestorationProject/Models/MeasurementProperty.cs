using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoeRestorationProject.Models;

[Table("MeasurementProperties")]
public class MeasurementProperty
{
    [Key]
    [Column("Id", TypeName = "int")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Column("Name", TypeName = "nvarchar(64)")]
    public string Name { get; set; } = null!;

    public ICollection<ShoeMeasurement> ShoeMeasurements { get; set; } = new List<ShoeMeasurement>();
}
