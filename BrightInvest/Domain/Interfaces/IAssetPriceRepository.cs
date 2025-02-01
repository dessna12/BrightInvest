using System;
using BrightInvest.Domain.Entities;

namespace BrightInvest.Domain.Interfaces;

public interface IAssetPriceRepository
{
	Task<IEnumerable<AssetPrice?>> GetAllAssetPricesAsync();
	Task<AssetPrice> GetAssetPriceByIdAsync(Guid id);
	Task<IEnumerable<AssetPrice?>> GetAllAssetPricesByAssetIdAsync(Guid id);
	Task AddAssetPriceAsync(AssetPrice assetPrice);
	Task<bool> DeleteAssetPriceAsync(Guid id);
	Task<bool> DeleteAllAssetPricesByAssetIdAsync(Guid assetId);
}


