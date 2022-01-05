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
		public string GetDescription(int result);
	}

	public class AgingTable : ITable
	{
		public TableType TableType => TableType.Aging;
		public string GetRandomValue()
		{
			var db = new InfoTablesContext();
			var result = RandomNumberGenerator.GetInt32(-6, 2);
			return GetDescription(result) + db.Aging.Find(result).Value;
		}

		public string GetDescription(int result)
		{
			return $"Aging ({result}):\n";
		}
	}

	public class AlliesAndEnemiesTable : ITable
	{
		public TableType TableType => TableType.AlliesAndEnemies;
		public string GetRandomValue()
		{
			var db = new InfoTablesContext();
			var result = DiceRoller.Roll(1, DiceType.d66);
			return GetDescription(result) + db.AlliesAndEnemies.Find(result).Value;
		}
		public string GetDescription(int result)
		{
			return $"Allies and Enemies ({result}):\n";
		}
	}

	public class DraftTable : ITable
	{
		public TableType TableType => TableType.Draft;
		public string GetRandomValue()
		{
			var db = new InfoTablesContext();
			var result = DiceRoller.Roll(1);
			return GetDescription(result) + db.Draft.Find(result).Value;
		}
		public string GetDescription(int result)
		{
			return $"Draft ({result}):\n";
		}
	}

	public class InjuryTable : ITable
	{
		public TableType TableType => TableType.Injury;
		public string GetRandomValue()
		{
			var db = new InfoTablesContext();
			var result = DiceRoller.Roll(2);
			return GetDescription(result) + db.Injury.Find(result).Value;
		}
		public string GetDescription(int result)
		{
			return $"Injury ({result}):\n";
		}
	}

	public class PreCareerEventsTable : ITable
	{
		public TableType TableType => TableType.PreCareerEvents;
		public string GetRandomValue()
		{
			var db = new InfoTablesContext();
			var result = DiceRoller.Roll(2);
			return GetDescription(result) + db.PreCareerEvents.Find(result).Value;
		}

		public string GetDescription(int result)
		{
			return $"Pre-Career Events ({result}):\n";
		}
	}

	public class LifeEventTable : ITable
	{
		public TableType TableType => TableType.LifeEvent;
		public string GetRandomValue()
		{
			var db = new InfoTablesContext();
			var result = RandomNumberGenerator.GetInt32(1, 73);
			return GetDescription(result) + db.LifeEvent.Find(result).Value;
		}
		public string GetDescription(int result)
		{
			return $"Life Event ({result}):\n";
		}
	}
}
