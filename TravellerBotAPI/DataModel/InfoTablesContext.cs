using System.IO;
using Microsoft.EntityFrameworkCore;
using TravellerBotAPI.DataModel;

namespace TravellerBotAPI
{
	public class InfoTablesContext : DbContext
	{
		public DbSet<Aging> Aging { get; set; }
		public DbSet<AlliesAndEnemies> AlliesAndEnemies { get; set; }
		public DbSet<Draft> Draft { get; set; }
		public DbSet<Injury> Injury { get; set; }
		public DbSet<LifeEvent> LifeEvent { get; set; }
		public DbSet<PreCareerEvents> PreCareerEvents { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlite($"Data Source = TravellerDB.db;");
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{

		}
	}
}
