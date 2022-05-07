using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TravellerBotAPI.DataModel;
using System.Linq;
using Newtonsoft.Json;

namespace TravellerBotAPI
{
	public class CharacterContext : DbContext
	{
		public DbSet<Character> Characters { get; set; }

		public Character CreateNewCharacter(long userID)
		{
			if (TryGetCharacterByID(userID, out var character)) {
				Remove(character);
			}

			character = new Character() {UserID = userID};
			Add(character);
			SaveChanges();
			return character;
		}

		public bool TryGetCharacterByID(long userID, out Character character)
		{
			character = Characters.FirstOrDefault(c => c.UserID == userID);
			if (character != null) {
				return true;
			}

			return false;
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlite($"Data Source = TravellerDB.db;");
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder
				.Entity<Character>()
				.Property(e => e.Skills)
				.HasConversion(
					v => JsonConvert.SerializeObject(v),
					v => JsonConvert.DeserializeObject<Dictionary<Skills, int>>(v)
				);
		}
	}
}
