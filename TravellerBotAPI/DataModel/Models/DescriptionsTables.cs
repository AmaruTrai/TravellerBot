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
	public class TradeCodes : Table<string, string> { }

}
