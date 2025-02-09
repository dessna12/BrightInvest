

using BrightInvest.Repository.Exceptions;
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
		try
		{
			return await _context.Assets.ToListAsync();
		}
		catch (Exception ex)
		{
			throw new RepositoryException($"An unexpected error occurred while fetching assets. :", ex);
		}
	}

	public async Task<Asset> GetAssetByIdAsync(Guid id)
	{
		Asset asset = await _context.Assets.FindAsync(id);

		if (asset == null)
			throw new KeyNotFoundException($"Asset with ID {id} not found.");
		return asset;
	}

	public async Task<Asset> GetAssetBySymbolAsync(string symbol)
	{
		Asset asset = await _context.Assets.Where(a => a.Ticker == symbol).FirstOrDefaultAsync(); ;
		
		if (asset == null)
			throw new KeyNotFoundException($"Asset with symbol {symbol} not found.");
		return asset;
	}

	public async Task AddAssetAsync(Asset asset)
	{
		try
		{
			bool assetExists = await _context.Assets.AnyAsync(a => a.Ticker == asset.Ticker);

			if (assetExists)
			{
				throw new RepositoryException($"An asset with ticker '{asset.Ticker}' already exists.");
			}

			await _context.Assets.AddAsync(asset);
			await _context.SaveChangesAsync();
		}
		catch (DbUpdateException ex)
		{
			throw new RepositoryException("Failed to add asset to the database.", ex);
		}
		catch (Exception ex)
		{
			throw new RepositoryException("An unexpected error occurred while adding an asset.", ex);
		}
	}

	public async Task AddAssetsAsync(IEnumerable<Asset> assets)
	{
		try
		{
			var tickers = assets.Select(a => a.Ticker).ToList();

			// Fetch existing tickers in one query
			var existingTickers = await _context.Assets
				.Where(a => tickers.Contains(a.Ticker))
				.Select(a => a.Ticker)
				.ToListAsync();

			// Filter out assets that already exist
			var newAssets = assets.Where(a => !existingTickers.Contains(a.Ticker)).ToList();

			if (newAssets.Any())
			{
				await _context.Assets.AddRangeAsync(newAssets);
				await _context.SaveChangesAsync();
			}
		}
		catch (DbUpdateException ex)
		{
			throw new RepositoryException("Failed to add assets to the database.", ex);
		}
		catch (Exception ex)
		{
			throw new RepositoryException("An unexpected error occurred while adding assets.", ex);
		}
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
		catch (DbUpdateConcurrencyException ex)
		{
			throw new RepositoryException("A concurrency error occurred while updating the asset.", ex);
		}
		catch (KeyNotFoundException ex)
		{
			throw ex;
		}
		catch (Exception ex)
		{
			throw new RepositoryException("An unexpected error occurred while updating the asset.", ex);
		}
	}

	public async Task<bool> DeleteAssetAsync(Guid assetId)
	{
		try
		{
			var existingAsset = await GetAssetByIdAsync(assetId);
			if (existingAsset == null)
				throw new KeyNotFoundException("Asset not found");

			_context.Assets.Remove(existingAsset);
			await _context.SaveChangesAsync();
			return true;
		}
		catch (DbUpdateConcurrencyException ex)
		{
			throw new RepositoryException("A concurrency error occurred while updating the asset.", ex);
		}
		catch (KeyNotFoundException ex)
		{
			throw ex;
		}
		catch (Exception ex)
		{
			throw new RepositoryException("An unexpected error occurred while updating the asset.", ex);
		}
	}
}