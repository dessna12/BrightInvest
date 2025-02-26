using System.Data;
using BrightInvest.Application.DTOs.AssetMetrics;
using BrightInvest.Application.Services.AssetPrices;
using BrightInvest.Domain.Entities;

namespace BrightInvest.Application.Services.AssetMetrics
{
	public class AssetMetricsService : IAssetMetricsService
	{
		private readonly IAssetIndicatorService _assetIndicatorService;


		public AssetMetricsService(IAssetIndicatorService assetIndicatorService)
		{ 
			_assetIndicatorService = assetIndicatorService;
		}

		public AssetMetricHorizon CalculateMetrics(IEnumerable<AssetPrice> prices, IEnumerable<AssetPrice> marketPrices, double riskFreeRate, string horizon)
		{
			DateTime startofYearDate = new DateTime(DateTime.UtcNow.Year, 1, 1);
			DateTime oneMonthAgoDate = DateTime.UtcNow.AddMonths(-1);

			List<decimal> orderedPrices;
			List<decimal> orderedMarketPrices;

			switch (horizon)
			{
				case "Max":
					orderedPrices = prices.OrderBy(p => p.Date).Select(p => p.ClosePrice).ToList();
					orderedMarketPrices = marketPrices.OrderBy(p => p.Date).Select(p => p.ClosePrice).ToList();
					break;
				case "YTD":
					orderedPrices = prices.OrderBy(p => p.Date).Where(p => p.Date >= startofYearDate).Select(p => p.ClosePrice).ToList();
					orderedMarketPrices = marketPrices.OrderBy(p => p.Date).Where(p => p.Date >= startofYearDate).Select(p => p.ClosePrice).ToList();
					break;
				case "1M":
					orderedPrices = prices.OrderBy(p => p.Date).Where(p => p.Date >= oneMonthAgoDate).Select(p => p.ClosePrice).ToList();
					orderedMarketPrices = marketPrices.OrderBy(p => p.Date).Where(p => p.Date >= oneMonthAgoDate).Select(p => p.ClosePrice).ToList();
					break;
				default:
					orderedPrices = prices.OrderBy(p => p.Date).Where(p => p.Date >= startofYearDate).Select(p => p.ClosePrice).ToList();
					orderedMarketPrices = marketPrices.OrderBy(p => p.Date).Where(p => p.Date >= startofYearDate).Select(p => p.ClosePrice).ToList();
					break;
			}


			return new AssetMetricHorizon(
				_assetIndicatorService.CalculateReturn(orderedPrices),
				_assetIndicatorService.CalculateVolatility(orderedPrices),
				_assetIndicatorService.CalculateBeta(orderedPrices, orderedMarketPrices),
				_assetIndicatorService.CalculateSharpeRatio(orderedPrices, riskFreeRate)
			);
		}
	}
}
