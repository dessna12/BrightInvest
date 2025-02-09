namespace BrightInvest.Infrastructure.DataBase
{
	using Microsoft.EntityFrameworkCore;
	using BrightInvest.Domain.Entities;
	using System;

	public class DataContext : DbContext
	{
		public DataContext(DbContextOptions<DataContext> options)
			: base(options)
		{
		}
		public DbSet<Asset> Assets { get; set; }
		public DbSet<AssetPrice> AssetPrices { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<AssetPrice>()
				.HasOne(ap => ap.Asset)
				.WithMany(a => a.Prices)
				.HasForeignKey(ap => ap.AssetId);


			// Enforce uniqueness: Each AssetId can only have one entry per Date
			modelBuilder.Entity<AssetPrice>()
				.HasIndex(ap => new { ap.AssetId, ap.Date })
				.IsUnique();
		}


	}
}