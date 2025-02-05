using BrightInvest.Domain.Interfaces;
using BrightInvest.Domain.Entities;
using BrightInvest.Application.UseCases.Interfaces;
using AutoMapper;

namespace BrightInvest.Application.UseCases.Assets
{
	public class AssetUseCase : IAssetUseCase
	{
		private readonly IAssetRepository _assetRepository;
		private readonly IMapper _mapper;
		public AssetUseCase(IAssetRepository assetRepository, IMapper mapper)
		{
			_assetRepository = assetRepository;
			_mapper = mapper;
		}

		public async Task<IEnumerable<AssetDto>> GetAllAssetsAsync()
		{
			var assets = await _assetRepository.GetAllAssetsAsync();
			return assets.Select(asset => _mapper.Map<AssetDto>(asset));
		}

		public async Task<AssetDto> GetAssetByIdAsync(Guid assetId)
		{
			Asset asset = await _assetRepository.GetAssetByIdAsync(assetId);
			if (asset == null)
				throw new KeyNotFoundException($"Asset with ID {assetId} not found.");

			return _mapper.Map<AssetDto>(asset);
			//return new AssetDto(asset.Id, asset.Ticker, asset.Name, asset.Currency.ToString());
		}

		public async Task<AssetDto> CreateAssetAsync(AssetCreateDto assetCreateDto) 
		{
			Asset asset = _mapper.Map<Asset>(assetCreateDto);
			await _assetRepository.AddAssetAsync(asset);

			return _mapper.Map<AssetDto>(asset);
		}

		public async Task<bool> UpdateAssetAsync(AssetUpdateDto assetUpdateDto)
		{
			Asset asset = _mapper.Map<Asset>(assetUpdateDto);
			return await _assetRepository.UpdateAssetAsync(asset);
		}

		public async Task<bool> DeleteAssetAsync(Guid assetId)
		{
			return await _assetRepository.DeleteAssetAsync(assetId);
		}
	}
}
