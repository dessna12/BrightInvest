using BrightInvest.Domain.Entities;
using BrightInvest.Domain.Interfaces;
using BrightInvest.Infrastructure.DataBase;
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
			return await _context.AssetPrices.ToListAsync();
		}

		public async Task<IEnumerable<AssetPrice>> GetAllAssetPricesBySymbolAsync(string symbol)
		{
			return await _context.AssetPrices
			.Where(ap => ap.Asset.Ticker == symbol)
			.Include(ap => ap.Asset)
			.ToListAsync();
		}
		public async Task<IEnumerable<AssetPrice>> GetAllAssetPricesByAssetIdAsync(Guid assetId)
		{
			return await _context.AssetPrices.Where(ap => ap.AssetId == assetId).ToListAsync();
		}

		public async Task<AssetPrice> GetAssetPriceByIdAsync(Guid id)
		{
			AssetPrice assetPrice = await _context.AssetPrices.FindAsync(id);
			if (assetPrice == null)
			{
				throw new KeyNotFoundException($"AssetPrice with ID {id} not found.");
			}
			return assetPrice;
		}

		public async Task<AssetPrice?> GetLatestPriceByAssetIdAsync(Guid assetId)
		{
			return await _context.AssetPrices
				.Where(ap => ap.AssetId == assetId)
				.OrderByDescending(ap => ap.Date)
				.LastOrDefaultAsync();
		}

		public async Task AddAssetPriceAsync(AssetPrice assetPrice)
		{
			await _context.AssetPrices.AddAsync(assetPrice);
			await _context.SaveChangesAsync();
		}

		public async Task AddAssetPricesAsync(List<AssetPrice> assetPrices)
		{
			await _context.AssetPrices.AddRangeAsync(assetPrices);
			await _context.SaveChangesAsync();
		}

		public async Task<bool> DeleteAssetPriceAsync(Guid id)
		{
			try
			{
				var assetPrice = await GetAssetPriceByIdAsync(id);
				_context.AssetPrices.Remove(assetPrice);
				await _context.SaveChangesAsync();
				return true;
			}
			catch
			{
				return false;
			}
		}

		public async Task<bool> DeleteAllAssetPricesByAssetIdAsync(Guid assetId)
		{
			try
			{
				var assetPriceList = await GetAllAssetPricesByAssetIdAsync(assetId);
				if (assetPriceList.Any())
				{
					_context.AssetPrices.RemoveRange(assetPriceList);
					await _context.SaveChangesAsync();
					return true;
				}
				return false;
			}
			catch
			{
				return false;
			}
		}


	}

}
