using BrightInvest.Domain.Interfaces;
using BrightInvest.Domain.Entities;
using BrightInvest.Application.UseCases.Interfaces;

namespace BrightInvest.Application.UseCases.Assets
{
	public class AssetUseCase : IAssetUseCase
	{
		private readonly IAssetRepository _assetRepository;
		public AssetUseCase(IAssetRepository assetRepository)
		{
			_assetRepository = assetRepository;
		}

		public async Task<IEnumerable<AssetDto>> GetAllAssetsAsync()
		{
			var assets = await _assetRepository.GetAllAssetsAsync();
			return assets.Select(asset => new AssetDto(asset.Id, asset.Ticker, asset.Name, asset.Currency));
		}

		public async Task<AssetDto> GetAssetByIdAsync(Guid assetId)
		{
			var asset = await _assetRepository.GetAssetByIdAsync(assetId);
			if (asset == null)
				throw new KeyNotFoundException($"Asset with ID {assetId} not found.");

			return new AssetDto(asset.Id, asset.Ticker, asset.Name, asset.Currency);
		}

		public async Task<AssetDto> CreateAssetAsync(AssetCreateDto assetCreateDto) 
		{
			var asset = new Asset(assetCreateDto.Ticker, assetCreateDto.Name, assetCreateDto.Currency);
			await _assetRepository.AddAssetAsync(asset);

			return new AssetDto(asset.Id, asset.Ticker, asset.Name, asset.Currency);
		}

		public async Task<bool> DeleteAssetAsync(Guid assetId)
		{
			return await _assetRepository.DeleteAssetAsync(assetId);
		}
	}
}
