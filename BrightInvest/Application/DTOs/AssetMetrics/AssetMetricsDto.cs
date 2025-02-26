namespace BrightInvest.Application.DTOs.AssetMetrics
{
	public record AssetMetricsDto(
		AssetMetricHorizon OneMonth,
		AssetMetricHorizon YTD,
		AssetMetricHorizon Max
	);

	public record AssetMetricHorizon(
		decimal AnnualisedReturn,
		decimal Volatility,
		decimal Beta,
		decimal SharpeRatio
	);
}
