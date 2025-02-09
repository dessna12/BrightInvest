using System;
using BrightInvest.Domain.Entities;

namespace BrightInvest.Domain.Interfaces;

public interface IAssetRepository
{
	Task<IEnumerable<Asset>> GetAllAssetsAsync();
	Task<Asset> GetAssetByIdAsync(Guid id);
	Task<Asset> GetAssetBySymbolAsync(string symbol);
	Task AddAssetAsync(Asset asset);
	Task AddAssetsAsync(IEnumerable<Asset> assets);
	Task<bool> UpdateAssetAsync(Asset asset);
	Task<bool> DeleteAssetAsync(Guid id);	
}


