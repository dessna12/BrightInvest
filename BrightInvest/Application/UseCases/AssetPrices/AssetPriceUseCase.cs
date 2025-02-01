using BrightInvest.Application.DTOs.AssetPrice;
using BrightInvest.Domain.Entities;
using BrightInvest.Domain.Interfaces;
using BrightInvest.Infrastructure.Repository;

namespace BrightInvest.Application.UseCases.AssetPrices
{
	public class AssetPriceUseCase : IAssetPriceUseCase
	{

		private readonly IAssetPriceRepository _assetPriceRepository;

		public AssetPriceUseCase(IAssetPriceRepository assetPriceRepository)
		{
			_assetPriceRepository = assetPriceRepository;
		}

		public async Task<IEnumerable<AssetPriceDto>> GetAllAssetPricesAsync()
		{
			var assetPrices = await _assetPriceRepository.GetAllAssetPricesAsync();
			return assetPrices.Select(assetPrice => new AssetPriceDto(assetPrice.Id, assetPrice.AssetId, assetPrice.Date, assetPrice.ClosePrice));
		}

		public async Task<AssetPriceDto> GetAssetPriceByIdAsync(Guid id)
		{
			var assetPrice = await _assetPriceRepository.GetAssetPriceByIdAsync(id);
			if (assetPrice == null)
				throw new KeyNotFoundException($"Asset with ID {id} not found.");
			return new AssetPriceDto(assetPrice.Id, assetPrice.AssetId, assetPrice.Date, assetPrice.ClosePrice);
		}

		public async Task<IEnumerable<AssetPriceDto>> GetAllAssetPricesByAssetIdAsync(Guid assetId)
		{
			var assetPrices = await _assetPriceRepository.GetAllAssetPricesByAssetIdAsync(assetId);
			return assetPrices.Select(assetPrice => new AssetPriceDto(assetPrice.Id, assetPrice.AssetId, assetPrice.Date, assetPrice.ClosePrice));
		}

		public async Task<AssetPriceDto> CreateAssetPriceAsync(AssetPriceCreateDto assetCreateDto)
		{
			var assetPrice = new AssetPrice(assetCreateDto.AssetId, assetCreateDto.Date, assetCreateDto.ClosePrice);
			await _assetPriceRepository.AddAssetPriceAsync(assetPrice);

			return new AssetPriceDto(assetPrice.Id, assetPrice.AssetId, assetPrice.Date, assetPrice.ClosePrice);
		}
		public async Task<bool> DeleteAssetPriceAsync(Guid id)
		{
			return await _assetPriceRepository.DeleteAssetPriceAsync(id);
		}

		public async Task<bool> DeleteAllAssetPricesByAssetIdAsync(Guid assetId)
		{
			return await _assetPriceRepository.DeleteAllAssetPricesByAssetIdAsync(assetId);
		}


	}
}
