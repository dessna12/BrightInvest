using BrightInvest.Application.DTOs.AssetPrices;
using BrightInvest.Application.UseCases.Interfaces;

namespace BrightInvest.Application.Services.AssetPrices
{
	public class AssetPriceService : IAssetPriceService
	{

		private readonly IAssetPriceUseCase _assetPriceUseCase;

		public AssetPriceService(IAssetPriceUseCase assetPriceUseCase)
		{
			_assetPriceUseCase = assetPriceUseCase;
		}

		public async Task<IEnumerable<AssetPriceDto>> GetAllAssetPricesAsync()
		{
			return await _assetPriceUseCase.GetAllAssetPricesAsync();
		}

		public async Task<AssetPriceDto> GetAssetPriceByIdAsync(Guid id)
		{
			return await _assetPriceUseCase.GetAssetPriceByIdAsync(id);
		}

		public async Task<IEnumerable<AssetPriceDto>> GetAllAssetPricesByAssetIdAsync(Guid assetId)
		{
			return await _assetPriceUseCase.GetAllAssetPricesByAssetIdAsync(assetId);
		}

		public async Task<AssetPriceDto> CreateAssetPriceAsync(AssetPriceCreateDto assetCreateDto)
		{
			return await _assetPriceUseCase.CreateAssetPriceAsync(assetCreateDto);
		}

		public async Task<bool> DeleteAssetPriceAsync(Guid id)
		{
			return await _assetPriceUseCase.DeleteAssetPriceAsync(id);
		}

		public async Task<bool> DeleteAllAssetPricesByAssetIdAsync(Guid assetId)
		{
			return await _assetPriceUseCase.DeleteAllAssetPricesByAssetIdAsync(assetId);
		}

	}
}
