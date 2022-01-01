using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TravellerBotAPI.DataModel
{
	public class Character
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
	}
}
