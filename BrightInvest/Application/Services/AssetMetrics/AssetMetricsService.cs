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

		public AssetMetricsDto CalculateMetrics(IEnumerable<AssetPrice> prices, IEnumerable<AssetPrice> marketPrices, double riskFreeRate)
		{
			DateTime startofYearDate = new DateTime(DateTime.UtcNow.Year, 1, 1);

			var orderedPrices = prices.OrderBy(p => p.Date).Select(p => p.ClosePrice).ToList();
			var orderedYTDPrices = prices.OrderBy(p => p.Date).Where(p => p.Date >= startofYearDate).Select(p => p.ClosePrice).ToList();
			var orderedMarketPrices = marketPrices.OrderBy(p => p.Date).Select(p => p.ClosePrice).ToList();

			return new AssetMetricsDto(
				_assetIndicatorService.CalculateReturn(orderedYTDPrices),
				_assetIndicatorService.CalculateVolatility(orderedPrices),
				_assetIndicatorService.CalculateBeta(orderedPrices, orderedMarketPrices),
				_assetIndicatorService.CalculateSharpeRatio(orderedPrices, riskFreeRate)
			);
		}
	}
}
