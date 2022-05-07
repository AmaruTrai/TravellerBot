using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TravellerBotAPI.DataModel
{
	public class HeightAndMass
	{
		public string ID { get; set; }
		public int BaseHeight { get; set; }
		public int WorldSize { get; set; }
		public int DeltaHeight { get; set; }
		public int BaseMass { get; set; }
		public int Dex { get; set; }
		public int DeltaMass { get; set; }
	}

	public class HairGenerateTable : Table<int, int> {}

	public class EyeGenerateTable
	{
		public int ID { get; set; }
		public int Value { get; set; }
		public int Hair { get; set; }
	}

	public class TradeCodesBackgroundSkills : Table<int, List<Skills>> {}

	public class BackgroundSkillsCount
	{
		[Key]
		public int EducationScore { get; set; }
		public int LowTech { get; set; }
		public int MidTech { get; set; }
		public int HighTech { get; set; }
	}
}
