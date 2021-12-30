using TravellerBotAPI.Support;
using System.Security.Cryptography;

namespace TravellerBotAPI.Commands
{
	public enum TableType
	{
		Aging,
		AlliesAndEnemies,
		Draft,
		Injury,
		LifeEvent,
		PreCareerEvents,
	}

	public interface ITable
	{
		public TableType TableType { get; }
		public string GetRandomValue();
	}

	public class AgingTable : ITable
	{
		public TableType TableType => TableType.Aging;
		public string GetRandomValue()
		{
			var db = new InfoTablesContext();
			var result = RandomNumberGenerator.GetInt32(-6, 2);
			return db.Aging.Find(result).Value;
		}
	}

	public class AlliesAndEnemiesTable : ITable
	{
		public TableType TableType => TableType.AlliesAndEnemies;
		public string GetRandomValue()
		{
			var db = new InfoTablesContext();
			var result = DiceRoller.Roll(1, DiceType.d66);
			return db.AlliesAndEnemies.Find(result).Value;
		}
	}

	public class DraftTable : ITable
	{
		public TableType TableType => TableType.Draft;
		public string GetRandomValue()
		{
			var db = new InfoTablesContext();
			var result = DiceRoller.Roll(1);
			return db.Draft.Find(result).Value;
		}
	}

	public class InjuryTable : ITable
	{
		public TableType TableType => TableType.Injury;
		public string GetRandomValue()
		{
			var db = new InfoTablesContext();
			var result = DiceRoller.Roll(1);
			return db.Injury.Find(result).Value;
		}
	}

	public class PreCareerEventsTable : ITable
	{
		public TableType TableType => TableType.PreCareerEvents;
		public string GetRandomValue()
		{
			var db = new InfoTablesContext();
			var result = DiceRoller.Roll(2);
			return db.PreCareerEvents.Find(result).Value;
		}
	}

	public class LifeEventTable : ITable
	{
		public TableType TableType => TableType.LifeEvent;
		public string GetRandomValue()
		{
			var db = new InfoTablesContext();
			var result = RandomNumberGenerator.GetInt32(1, 73);
			return db.LifeEvent.Find(result).Value;
		}
	}
}
