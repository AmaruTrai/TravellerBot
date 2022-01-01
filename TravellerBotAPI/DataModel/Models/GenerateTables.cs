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
}
