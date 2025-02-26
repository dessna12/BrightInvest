public sealed record AssetMetrics(decimal YtdReturn, decimal Volatility, decimal Beta, decimal SharpeRatio)
{
	public static AssetMetrics Empty => new(0m, 0m, 0m, 0m);
}