using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoeRestorationProject.Models;

public class MeasurementMetric
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public ICollection<ShoeMeasurement> ShoeMeasurements = new List<ShoeMeasurement>();
}
