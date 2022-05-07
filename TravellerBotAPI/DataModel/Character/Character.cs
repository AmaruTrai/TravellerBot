using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TravellerBotAPI.Support;

namespace TravellerBotAPI.DataModel
{

	public partial class Character
	{
		[Key]
		public long UserID { get; set; }
		public long? STR { get; set; }
		public long? DEX { get; set; }
		public long? END { get; set; }
		public long? INT { get; set; }
		public long? EDU { get; set; }
		public long? SOC { get; set; }
		public string HomePlanetUWP { get; set; }
		public float? Height { get; set; }
		public float? Mass { get; set; }
		public Gender? Gender { get; set; }
		public Hair? Hair { get; set; }
		public Eye? Eye { get; set; }
		public SkinTone? SkinTone { get; set; }

		public Dictionary<Skills, int> Skills { get; set; } = new ();

		public int[] RollCharacteristic()
		{
			var values = new int[6];
			values = values.Select(v => v = DiceRoller.Roll(2)).ToArray();
			STR = values[0];
			DEX = values[1];
			END = values[2];
			INT = values[3];
			EDU = values[4];
			SOC = values[5];
			return values;
		}

		public void SetCharacteristic(List<int> values)
		{
			STR = values[0];
			DEX = values[1];
			END = values[2];
			INT = values[3];
			EDU = values[4];
			SOC = values[5];
		}

		public string GetCharacteristic()
		{
			return $"STR({STR}) DEX({DEX}) END({END}), INT({INT}), EDU({EDU}) SOC({SOC})";
		}
	}
}
