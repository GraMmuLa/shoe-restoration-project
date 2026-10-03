using ShoeRestorationProject.Features.SizeMetrics;

namespace ShoeRestorationProject.Features.Sizes;
public record SizeRequest(decimal Value, int SizeMetricId);
public record SizeResponse(int Id, decimal Value, SizeMetricResponse
    SizeMetric);
