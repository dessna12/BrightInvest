public class AssetIndicatorService : IAssetIndicatorService
{
	public decimal CalculateYTDReturn(decimal lastPrice, decimal firstPriceOfYear) =>
		(lastPrice - firstPriceOfYear) / firstPriceOfYear;

	public decimal CalculateVolatility(List<decimal> returns) =>
		returns.Count > 1 ? (decimal)Math.Sqrt((double)returns.Average(r => r * r)) : 0m;

	public decimal CalculateBeta(List<decimal> stockReturns, List<decimal> marketReturns)
	{
		if (stockReturns.Count != marketReturns.Count || stockReturns.Count == 0)
			return 0m;

		decimal covariance = stockReturns.Zip(marketReturns, (s, m) => s * m).Average();
		decimal variance = marketReturns.Average(m => m * m);
		return variance != 0 ? covariance / variance : 0m;
	}

	public decimal CalculateSharpeRatio(decimal portfolioReturn, double riskFreeRate, decimal volatility) =>
		volatility != 0 ? (portfolioReturn - (decimal)riskFreeRate) / volatility : 0m;
}