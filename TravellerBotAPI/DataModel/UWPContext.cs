using Microsoft.EntityFrameworkCore;
using TravellerBotAPI.DataModel;

namespace TravellerBotAPI
{
	public class UWPContext : DbContext
	{
		public DbSet<StarportQuality> StarportQuality { get; set; }
		public DbSet<PlanetSize> PlanetSize { get; set; }
		public DbSet<AtmosphereType> AtmosphereType { get; set; }
		public DbSet<Hydrographic> Hydrographic { get; set; }
		public DbSet<Population> Population { get; set; }
		public DbSet<GovernmentType> GovernmentType { get; set; }
		public DbSet<LawLevel> LawLevel { get; set; }
		public DbSet<TechnologicalLevel> TechnologicalLevel { get; set; }
		public DbSet<TradeCodes> TradeCodes { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlite(@"Data Source = Resources\TravellerDB.db;");
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{

		}
	}
}
