using BrightInvest.Application.DTOs.AssetMetrics;

namespace BrightInvest.Application.UseCases.Interfaces
{
	public interface IAssetMetricsUseCase
	{

		Task<AssetMetricsDto> GetAssetMetricsByAssetId(Guid id);
	}
}
