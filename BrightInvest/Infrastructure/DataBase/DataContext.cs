namespace BrightInvest.Infrastructure.DataBase
{
	using Microsoft.EntityFrameworkCore;
	using BrightInvest.Domain.Entities;

	public class DataContext : DbContext
	{
		public DataContext(DbContextOptions<DataContext> options)
			: base(options)
		{
		}

		public DbSet<Asset> Assets { get; set; }
	}
}