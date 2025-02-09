using BrightInvest.Domain.Entities;
using BrightInvest.Domain.Interfaces;
using BrightInvest.Infrastructure.DataBase;
using BrightInvest.Repository.Exceptions;
using Microsoft.EntityFrameworkCore;
using NuGet.ContentModel;

namespace BrightInvest.Infrastructure.Repositories
{
	public class AssetPriceRepository : IAssetPriceRepository
	{

		private readonly DataContext _context;
		public AssetPriceRepository(DataContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<AssetPrice?>> GetAllAssetPricesAsync()
		{
			try {
				return await _context.AssetPrices.ToListAsync();
			}
			catch (Exception ex)
			{
				throw new RepositoryException($"An unexpected error occurred while fetching assets prices. :", ex);
			}
}

		public async Task<IEnumerable<AssetPrice>> GetAllAssetPricesBySymbolAsync(string symbol)
		{
			try { 
				return await _context.AssetPrices
				.Where(ap => ap.Asset.Ticker == symbol)
				.Include(ap => ap.Asset)
				.ToListAsync();
			}
			catch (Exception ex)
			{
				throw new RepositoryException($"An unexpected error occurred while fetching assets prices. :", ex);
			}
		}
		public async Task<IEnumerable<AssetPrice>> GetAllAssetPricesByAssetIdAsync(Guid assetId)
		{
			try
			{
				return await _context.AssetPrices.Where(ap => ap.AssetId == assetId).ToListAsync();
			}
			catch (Exception ex)
			{
				throw new RepositoryException($"An unexpected error occurred while fetching assets prices. :", ex);
			}
		}

		public async Task<AssetPrice> GetAssetPriceByIdAsync(Guid id)
		{
			try
			{
				AssetPrice assetPrice = await _context.AssetPrices.FindAsync(id);
				if (assetPrice == null)
					throw new KeyNotFoundException($"AssetPrice with ID {id} not found.");
				return assetPrice;
			}
			catch (KeyNotFoundException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw new RepositoryException($"An unexpected error occurred while fetching assets prices. :", ex);
			}
		}

		public async Task<AssetPrice?> GetLatestPriceByAssetIdAsync(Guid assetId)
		{
			try
			{
				return await _context.AssetPrices
				.Where(ap => ap.AssetId == assetId)
				.OrderBy(ap => ap.Date)
				.LastOrDefaultAsync();
			}
			catch (Exception ex)
			{
				throw new RepositoryException($"An unexpected error occurred while fetching assets prices. :", ex);
			}
		}

		public async Task AddAssetPriceAsync(AssetPrice assetPrice)
		{
			try
			{
				await _context.AssetPrices.AddAsync(assetPrice);
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateException ex)
			{
				throw new RepositoryException("Duplicate data detected, skipping insert.");
			}
			catch (Exception ex)
			{
				throw new RepositoryException($"Fail to add assets prices :", ex);
			}
		}

		public async Task AddAssetPricesAsync(List<AssetPrice> assetPrices)
		{
			try
			{
				await _context.AssetPrices.AddRangeAsync(assetPrices);
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateException ex)
			{
				throw new RepositoryException("Duplicate data detected, skipping insert.");
			}
			catch (Exception ex)
			{
				throw new RepositoryException($"Fail to add assets prices :", ex);
			}
		}

		public async Task<bool> DeleteAssetPriceAsync(Guid id)
		{
			try
			{
				var existingAsset = await GetAssetPriceByIdAsync(id);
				if (existingAsset == null)
					throw new KeyNotFoundException("Asset not found");

				_context.AssetPrices.Remove(existingAsset);
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

		public async Task<bool> DeleteAllAssetPricesByAssetIdAsync(Guid assetId)
		{
			try
			{
				IEnumerable<AssetPrice> assetPriceList = await GetAllAssetPricesByAssetIdAsync(assetId);
				if (!assetPriceList.Any())
					throw new KeyNotFoundException("Asset not found");

					_context.AssetPrices.RemoveRange(assetPriceList);
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

}
