namespace BrightInvest.Application.DTOs.AssetMetrics
{
	public record AssetMetricsDto(
		decimal YTDReturn,
		decimal Volatility,
		decimal Beta,
		decimal SharpeRatio
	);
}
