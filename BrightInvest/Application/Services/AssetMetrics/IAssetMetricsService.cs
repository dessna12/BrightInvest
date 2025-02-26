using BrightInvest.Application.DTOs.AssetMetrics;
using BrightInvest.Domain.Entities;

namespace BrightInvest.Application.Services.AssetMetrics
{
	public interface IAssetMetricsService
	{
		AssetMetricHorizon CalculateMetrics(IEnumerable<AssetPrice> prices, IEnumerable<AssetPrice> marketPrices, double riskFreeRate, string horizon = "");
	}
}
