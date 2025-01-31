namespace BrightInvest.Application.Services
{
	public interface IAssetService
	{
		Task<IEnumerable<AssetDto>> GetAssetsAsync();
		Task<AssetDto?> GetAssetByIdAsync(Guid id);
		Task<AssetDto> CreateAssetAsync(AssetCreateDto assetDto);
		//Task<bool> DeleteAssetAsync(Guid id);
	}
}
