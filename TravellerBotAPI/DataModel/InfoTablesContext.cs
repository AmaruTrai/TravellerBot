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
		public DbSet<HairDescription> Hair { get; set; }
		public DbSet<EyeDescription> Eye { get; set; }
		public DbSet<GenderDescription> Gender { get; set; }
		public DbSet<SkinToneDescription> SkinTone { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlite($"Data Source = TravellerDB.db;");
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{

		}
	}
}
