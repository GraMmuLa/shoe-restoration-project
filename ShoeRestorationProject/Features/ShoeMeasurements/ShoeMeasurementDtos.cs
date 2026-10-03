using ShoeRestorationProject.Features.MeasurementMetrics;
using ShoeRestorationProject.Features.MeasurementProperties;
using ShoeRestorationProject.Features.Shoes;

namespace ShoeRestorationProject.Features.ShoeMeasurements;
public record ShoeMeasurementRequest(decimal Value, int MeasurementMetricId);
public record ShoeMeasurementResponse(int Id, decimal Value,
    MeasurementMetricResponse MeasurementMetric,
    MeasurementPropertyResponse MeasurementProperty,
    ShoeResponse Shoe);
