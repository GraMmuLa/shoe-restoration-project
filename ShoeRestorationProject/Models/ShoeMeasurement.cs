using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoeRestorationProject.Models;

public class ShoeMeasurement
{
    public int Id { get; set; }

    public decimal Value { get; set; }
    
    public int ShoeId { get; set; }
    
    public Shoe Shoe { get; set; } = null!;
    
    public int MeasurementPropertyId { get; set; }
    
    public MeasurementProperty MeasurementProperty { get; set; } = null!;
    
    public int MeasurementMetricId { get; set; }
    
    public MeasurementMetric MeasurementMetric { get; set; } = null!;
}
