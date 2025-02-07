

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