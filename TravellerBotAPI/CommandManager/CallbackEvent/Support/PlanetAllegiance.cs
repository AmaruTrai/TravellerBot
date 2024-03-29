using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using TravellerBotAPI.DataModel;

namespace TravellerBotAPI.Commands
{
	public enum PlanetAllegiance
	{
		ThirdImperium
	}

	public abstract class Allegiance
	{
		public PlanetAllegiance TypeAllegiance { get; protected set; }
		public List<string> Keys { get; protected set; }

		public SpaceSystemsModel GetRandomPlanet()
		{
			var db = new SpaceSystemsContext();
			var selection = db.SpaceSystems.Where(s => Keys.Contains(s.Allegiance));
			var planet = selection.Skip(RandomNumberGenerator.GetInt32(0, selection.Count() - 1)).First();
			return planet;
		}
	}

	public class ThirdImperium : Allegiance
	{
		public ThirdImperium()
		{
			TypeAllegiance = PlanetAllegiance.ThirdImperium;
			Keys = new List<string>() { "ImAp", "ImDa", "ImDc", "ImDd",
				"ImDg", "ImDi", "ImDs", "ImDv", "ImLa", "ImLc", "ImLu", "ImSy", "ImVd" };
		}
	}
}
