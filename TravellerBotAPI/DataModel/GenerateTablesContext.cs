using System.Collections.Generic;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using TravellerBotAPI.DataModel;

namespace TravellerBotAPI
{
	public class GenerateTablesContext : DbContext
	{

		public DbSet<HeightAndMass> HeightAndMass { get; set; }
		public DbSet<HairGenerateTable> HairGenerateTable { get; set; }
		public DbSet<EyeGenerateTable> EyeGenerateTable { get; set; }
		public DbSet<TradeCodesBackgroundSkills> TradeCodesBackgroundSkills { get; set; }
		public DbSet<BackgroundSkillsCount> BackgroundSkillsCount { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlite($"Data Source = TravellerDB.db;");
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder
				.Entity<TradeCodesBackgroundSkills>()
				.Property(e => e.Value)
				.HasConversion(
					v => JsonConvert.SerializeObject(v),
					v => JsonConvert.DeserializeObject<List<Skills>>(v)
				);
		}
	}
}
