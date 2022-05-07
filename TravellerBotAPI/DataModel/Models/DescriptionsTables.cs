namespace TravellerBotAPI.DataModel
{
	public class Table<TKey, TValue> 
	{
		public TKey ID { get; set; }
		public TValue Value { get; set; }
	}

	public class StarportQuality : Table<string, string> { }
	public class PlanetSize : Table<string, string> { }
	public class AtmosphereType : Table<string, string> { }
	public class Hydrographic : Table<string, string> { }
	public class Population : Table<string, string> { }
	public class GovernmentType : Table<string, string> { }
	public class LawLevel : Table<string, string> { }
	public class TechnologicalLevel : Table<string, string> { }
	public class TradeCodes : Table<int, string> { }

	public class Aging : Table<int, string> { }
	public class AlliesAndEnemies : Table<int, string> { }
	public class Draft : Table<int, string> { }
	public class Injury : Table<int, string> { }
	public class LifeEvent : Table<int, string> { }
	public class PreCareerEvents : Table<int, string> { }
	public class HairDescription : Table<int, string> { }
	public class EyeDescription : Table<int, string> { }
	public class GenderDescription : Table<int, string> { }
	public class SkinToneDescription : Table<int, string> { }

}
