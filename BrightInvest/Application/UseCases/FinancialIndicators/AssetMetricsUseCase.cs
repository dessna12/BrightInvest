using BrightInvest.Application.DTOs.AssetMetrics;
using BrightInvest.Application.Services.AssetMetrics;
using BrightInvest.Application.UseCases.Interfaces;
using BrightInvest.Domain.Entities;
using BrightInvest.Domain.Interfaces;

namespace BrightInvest.Application.UseCases.FinancialIndicators
{
	public class AssetMetricsUseCase : IAssetMetricsUseCase
	{
		private readonly IAssetMetricsService _assetMetricsService;
		private readonly IAssetPriceRepository _assetPriceRepository;
		private const double RISK_FREE_RATE = 0.0290d;
		public AssetMetricsUseCase(IAssetMetricsService assetMetricsService, IAssetPriceRepository assetPriceRepository) { 
			_assetMetricsService = assetMetricsService;
			_assetPriceRepository = assetPriceRepository;
		}

		public async Task<AssetMetricsDto> GetAssetMetricsByAssetId(Guid id)
		{
			IEnumerable<AssetPrice?> assetPrices = await _assetPriceRepository.GetAllAssetPricesByAssetIdAsync(id);
			AssetMetricsDto assetMetricDto = new AssetMetricsDto(
				_assetMetricsService.CalculateMetrics(assetPrices, assetPrices, RISK_FREE_RATE, "1M"),
				_assetMetricsService.CalculateMetrics(assetPrices, assetPrices, RISK_FREE_RATE, "YTD"),
				_assetMetricsService.CalculateMetrics(assetPrices, assetPrices, RISK_FREE_RATE, "Max")
			);
				
			return assetMetricDto;
		}

	}
}
