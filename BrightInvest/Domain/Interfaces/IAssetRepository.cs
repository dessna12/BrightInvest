using System;
using BrightInvest.Domain.Entities;

namespace BrightInvest.Domain.Interfaces;

public interface IAssetRepository
{
	Task<IEnumerable<Asset>> GetAllAssetsAsync();
	Task<Asset> GetAssetByIdAsync(Guid id);
	Task AddAssetAsync(Asset asset);
	Task<bool> DeleteAssetAsync(Guid id);	
}


