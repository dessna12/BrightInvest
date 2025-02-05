

using BrightInvest.Domain.Entities;
using BrightInvest.Domain.Interfaces;
using BrightInvest.Infrastructure.DataBase;
using Microsoft.EntityFrameworkCore;
using static MudBlazor.CategoryTypes;

namespace BrightInvest.Infrastructure.Repository;

public class AssetRepository : IAssetRepository
{

	private readonly DataContext _context;
	public AssetRepository(DataContext context)
	{
		_context = context;
	}

	public async Task<IEnumerable<Asset>> GetAllAssetsAsync()
	{
		return await _context.Assets.ToListAsync();
	}

	public async Task<Asset> GetAssetByIdAsync(Guid id)
	{
		Asset asset = await _context.Assets.FindAsync(id);
		if (asset == null)
		{
			throw new KeyNotFoundException($"Asset with ID {id} not found.");
		}
		return asset;
	}

	public async Task<Asset> GetAssetBySymbolAsync(string symbol)
	{
		Asset asset = await _context.Assets.Where(a => a.Ticker == symbol).FirstOrDefaultAsync(); ;
		if (asset == null)
		{
			throw new KeyNotFoundException($"Asset with symbol {symbol} not found.");
		}
		return asset;
	}

	public async Task AddAssetAsync(Asset asset)
	{
		await _context.Assets.AddAsync(asset);
		await _context.SaveChangesAsync();
	}

	public async Task<bool> UpdateAssetAsync(Asset asset)
	{
		try
		{
			var existingAsset = await _context.Assets.FindAsync(asset.Id);
			if (existingAsset == null)
				throw new KeyNotFoundException("Asset not found");

			_context.Entry(existingAsset).CurrentValues.SetValues(asset);
			await _context.SaveChangesAsync();
			return true;
		}
		catch
		{
			return false;
		}
	}

	public async Task<bool> DeleteAssetAsync(Guid assetId)
	{
		try
		{
			var asset = await GetAssetByIdAsync(assetId);
			_context.Assets.Remove(asset);
			await _context.SaveChangesAsync();
			return true;
		}
		catch 
		{
			return false;
		}
	}

	

}