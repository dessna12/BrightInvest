public class AssetIndicatorService : IAssetIndicatorService
{
	private List<decimal> CalculateReturns(List<decimal> prices)
	{
		return prices.Skip(1)  // Skip the first price (no return for the first point)
					 .Zip(prices, (current, previous) => (current - previous) / previous)  // Calculate the returns
					 .ToList();
	}

	public decimal CalculateReturn(List<decimal> prices)
	{
		if (prices == null || prices.Count < 2)
			throw new ArgumentException("Price list must contain at least two values.");

		decimal firstPrice = prices.First();
		decimal lastPrice = prices.Last();

		return (lastPrice - firstPrice) / firstPrice;
	}

	public decimal CalculateVolatility(List<decimal> prices)
	{
		if (prices.Count < 2) return 0m;

		var returns = CalculateReturns(prices);
		return (decimal)Math.Sqrt((double)returns.Average(r => r * r));
	}

	public decimal CalculateBeta(List<decimal> stockPrices, List<decimal> marketPrices)
	{

		if (stockPrices.Count != marketPrices.Count || stockPrices.Count == 0 || marketPrices.Count == 0)
			return 0m;

		var stockReturns = CalculateReturns(stockPrices);
		var marketReturns = CalculateReturns(marketPrices);

		decimal covariance = stockReturns.Zip(marketReturns, (s, m) => s * m).Average();
		decimal variance = marketReturns.Average(m => m * m);
		return variance != 0 ? covariance / variance : 0m;
	}

	public decimal CalculateSharpeRatio(decimal portfolioReturn, double riskFreeRate, decimal volatility) =>
		volatility != 0 ? (portfolioReturn - (decimal)riskFreeRate) / volatility : 0m;




}