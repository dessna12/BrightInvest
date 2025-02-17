interface IAssetIndicatorService
{
	decimal CalculateYTDReturn(decimal lastPrice, decimal firstPriceOfYear);
	decimal CalculateBeta(List<decimal> stockReturns, List<decimal> marketReturns);
	public decimal CalculateSharpeRatio(decimal portfolioReturn, double riskFreeRate, decimal volatility);

}