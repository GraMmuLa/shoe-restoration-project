using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shoe_restoration_project.Models;

[Table("MeasurementProperties")]
public partial class MeasurementProperty
{
    [Key]
    [Column("Id", TypeName = "int")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [Column("Name", TypeName = "nvarchar(64)")]
    public string Name { get; set; } = null!;

    public ICollection<ShoeMeasurement> ShoeMeasurements { get; set; } = new List<ShoeMeasurement>();
}
