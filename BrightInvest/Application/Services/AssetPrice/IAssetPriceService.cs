using BrightInvest.Application.DTOs.AssetPrice;

namespace BrightInvest.Application.Services.AssetPrice
{
	public interface IAssetPriceService
	{
		Task<IEnumerable<AssetPriceDto>> GetAllAssetPricesAsync();
		Task<AssetPriceDto> GetAssetPriceByIdAsync(Guid id);
		Task<IEnumerable<AssetPriceDto>> GetAllAssetPricesByAssetIdAsync(Guid assetId);
		Task<AssetPriceDto> CreateAssetPriceAsync(AssetPriceCreateDto assetCreateDto);
		Task<bool> DeleteAssetPriceAsync(Guid id);
		Task<bool> DeleteAllAssetPricesByAssetIdAsync(Guid assetId);
	}
}
