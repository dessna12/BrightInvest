using BrightInvest.Application.Services;
using BrightInvest.Domain.Entities;
using BrightInvest.Domain.Interfaces;

public class AssetService : IAssetService
{
	private readonly IAssetRepository _assetRepository;

	//TODO: Use AutoMapper to avoid writing all the properties 
	public AssetService(IAssetRepository assetRepository)
	{
		_assetRepository = assetRepository;
	}

	public async Task<IEnumerable<AssetDto>> GetAssetsAsync()
	{
		var assets = await _assetRepository.GetAllAssetsAsync();
		return assets.Select(asset => new AssetDto(asset.Id, asset.Ticker, asset.Name));
	}

	public async Task<AssetDto> GetAssetByIdAsync(Guid id)
	{
		var asset = await _assetRepository.GetAssetByIdAsync(id);
		if (asset == null)
			return null;

		return new AssetDto(asset.Id, asset.Ticker, asset.Name);
	}

	public async Task<AssetDto> CreateAssetAsync(AssetCreateDto assetCreateDto)
	{
		var asset = new Asset(assetCreateDto.Ticker, assetCreateDto.Name);
		await _assetRepository.AddAssetAsync(asset);

		return new AssetDto(asset.Id, asset.Ticker, asset.Name);
	}
}