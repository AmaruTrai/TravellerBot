using Microsoft.EntityFrameworkCore;
using TravellerBotAPI.DataModel;

namespace TravellerBotAPI
{
	public class SpaceSystemsContext : DbContext
	{
		public DbSet<SpaceSystemsModel> SpaceSystems { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlite(@"Data Source = Resources\TravellerDB.db;");
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{

		}
	}
}
