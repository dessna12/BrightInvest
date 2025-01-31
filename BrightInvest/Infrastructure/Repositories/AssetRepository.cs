

using BrightInvest.Domain.Entities;
using BrightInvest.Domain.Interfaces;
using BrightInvest.Infrastructure.DataBase;
using Microsoft.EntityFrameworkCore;

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
		return await _context.Assets.FindAsync(id);
	}

	public async Task AddAssetAsync(Asset asset)
	{
		await _context.Assets.AddAsync(asset);
		await _context.SaveChangesAsync();
	}

	

}