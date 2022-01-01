using System.IO;
using Microsoft.EntityFrameworkCore;
using TravellerBotAPI.DataModel;

namespace TravellerBotAPI
{
	public class GenerateTablesContext : DbContext
	{

		public DbSet<HeightAndMass> HeightAndMass { get; set; }
		public DbSet<HairGenerateTable> HairGenerateTable { get; set; }
		public DbSet<EyeGenerateTable> EyeGenerateTable { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlite($"Data Source = TravellerDB.db;");
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{

		}
	}
}
