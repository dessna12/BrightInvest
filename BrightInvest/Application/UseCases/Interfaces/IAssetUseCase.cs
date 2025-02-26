using BrightInvest.Application.DTOs.Assets;
using BrightInvest.Domain.Entities;

namespace BrightInvest.Application.UseCases.Interfaces
{
	public interface IAssetUseCase
	{
		Task<IEnumerable<AssetDto?>> GetAllAssetsAsync();
		Task<AssetDto> GetAssetByIdAsync(Guid id);
		Task<AssetDto> CreateAssetAsync(AssetCreateDto assetCreateDto);
		Task<AssetsCreateResponseDto> CreateAssetsAsync(List<AssetCreateDto> assetCreateDtos);
		Task<bool> UpdateAssetAsync(AssetUpdateDto assetUpdateDto);
		Task<bool> DeleteAssetAsync(Guid assetId);
	}
}
