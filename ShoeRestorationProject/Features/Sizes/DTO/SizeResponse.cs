using ShoeRestorationProject.Features.SizeMetrics.DTO;

namespace ShoeRestorationProject.Features.Sizes.DTO;

public record SizeResponse(int Id, decimal Value, SizeMetricResponse
    SizeMetric);
