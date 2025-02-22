public interface IAssetIndicatorService
{
	public decimal CalculateReturn(List<decimal> prices);
	public decimal CalculateVolatility(List<decimal> prices);
	public decimal CalculateBeta(List<decimal> stockPrices, List<decimal> marketPrices);
	public decimal CalculateSharpeRatio(List<decimal> portfolioPrices, double riskFreeRate);

}