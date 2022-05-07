using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using TravellerBotAPI.Commands;
using TravellerBotAPI.DataModel;

namespace TravellerBotAPI
{
	public class UserContext : DbContext
	{
		public static void CreateNewUser(long userID)
		{
			var user = new UserModel() {
				ID = userID,
				Stage = CharacterGenerationCallback.Stage.HomeplanetSkills,
				UnallocatedSkillsCount = 0
			};

			var db = new UserContext();
			if (db.Users.Contains(user)) {
				db.Users.Update(user);
			} else {
				db.Users.Add(user);
			}

			db.SaveChanges();
		}

		public DbSet<UserModel> Users { get; set; }
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlite($"Data Source = TravellerDB.db;");
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder
				.Entity<UserModel>()
				.Property(e => e.UnallocatedSkills)
				.HasConversion(
					v => JsonConvert.SerializeObject(v),
					v => JsonConvert.DeserializeObject<List<Skills>>(v)
				);
		}
	}
}
