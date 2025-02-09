using BrightInvest.Application.Services.Assets;
using BrightInvest.Application.UseCases.Interfaces;
using BrightInvest.Domain.Entities;
using BrightInvest.Domain.Interfaces;

public class AssetService : IAssetService
{
	private readonly IAssetUseCase _assetUseCase;

	//TODO: Use AutoMapper to avoid writing all the properties 
	public AssetService(IAssetUseCase assetUseCase)
	{
		_assetUseCase = assetUseCase;
	}

	public async Task<IEnumerable<AssetDto>> GetAllAssetsAsync()
	{
		var assets = await _assetUseCase.GetAllAssetsAsync();
		return assets;
	}

	public async Task<AssetDto> GetAssetByIdAsync(Guid id)
	{
		return await _assetUseCase.GetAssetByIdAsync(id);
	}

	public async Task<AssetDto> CreateAssetAsync(AssetCreateDto assetCreateDto)
	{
		return await _assetUseCase.CreateAssetAsync(assetCreateDto);
	}

	public async Task<bool> DeleteAssetAsync(Guid id)
	{
		return await _assetUseCase.DeleteAssetAsync(id);
	}
}