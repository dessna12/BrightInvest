using BrightInvest.Application.DTOs.AssetPrices;

namespace BrightInvest.Application.UseCases.Interfaces
{
	public interface IAssetPriceUseCase
	{
		Task<IEnumerable<AssetPriceDto>> GetAllAssetPricesAsync();
		Task<AssetPriceDto> GetAssetPriceByIdAsync(Guid id);
		Task<IEnumerable<AssetPriceDto>> GetAllAssetPricesByAssetIdAsync(Guid assetID);
		Task<AssetPriceDto> CreateAssetPriceAsync(AssetPriceCreateDto assetCreateDto);
		Task<bool> DeleteAssetPriceAsync(Guid id);
		Task<bool> DeleteAllAssetPricesByAssetIdAsync(Guid assetId);

	}
}
