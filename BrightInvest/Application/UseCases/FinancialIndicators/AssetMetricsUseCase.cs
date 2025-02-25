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
		public AssetMetricsUseCase(IAssetMetricsService assetMetricsService, IAssetPriceRepository assetPriceRepository) { 
			_assetMetricsService = assetMetricsService;
			_assetPriceRepository = assetPriceRepository;
		}

		public async Task<AssetMetricsDto> GetAssetMetricsByAssetId(Guid id)
		{
			IEnumerable<AssetPrice?> assetPrices = await _assetPriceRepository.GetAllAssetPricesByAssetIdAsync(id);
			AssetMetricsDto assetMetricDto = _assetMetricsService.CalculateMetrics(assetPrices, assetPrices, 0.0290d);
			return assetMetricDto;
		}

	}
}
