using System;
using BrightInvest.Domain.Entities;

namespace BrightInvest.Domain.Interfaces;

public interface IAssetPriceRepository
{
	Task<IEnumerable<AssetPrice?>> GetAllAssetPricesAsync();
	Task<IEnumerable<AssetPrice>> GetAllAssetPricesBySymbolAsync(string symbol);
	Task<IEnumerable<AssetPrice>> GetAllAssetPricesByAssetIdAsync(Guid id);
	Task<AssetPrice> GetAssetPriceByIdAsync(Guid id);
	Task<AssetPrice?> GetLatestPriceByAssetIdAsync(Guid assetId);
	Task AddAssetPriceAsync(AssetPrice assetPrice);
	Task AddAssetPricesAsync(List<AssetPrice> assetPrices);
	Task<bool> DeleteAssetPriceAsync(Guid id);
	Task<bool> DeleteAllAssetPricesByAssetIdAsync(Guid assetId);

}


