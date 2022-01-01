using System.Linq;
using TravellerBotAPI.Support;

namespace TravellerBotAPI.DataModel
{
	public enum Gender
	{
		Male,
		Female,
	}

	public enum Hair
	{
		Black,
		Brown,
		Blonde,
	}


	public enum Eye
	{
		Brown,
		Blue,
		Green,
		Hazel,
		Grey,
	}

	public enum SkinTone
	{
		PaleWhite,
		White,
		Tanned,
		Olive,
		Brown,
		DarkBrown,
	}

	public partial class Character
	{
		public bool GenerateHeight()
		{
			if (HomePlanetUWP != null && UWP.TryParce(HomePlanetUWP, out var uwp) && Gender.HasValue && STR.HasValue) {
				var db = new GenerateTablesContext();
				var genderDelta = Gender == DataModel.Gender.Female ? -10 : 0;
				var planetSizeMod = db.HeightAndMass.First(v => v.ID == uwp.PlanetSize).WorldSize;
				var baseHeight = db.HeightAndMass.First(v => v.ID == STR.Value.ToString("X")).BaseHeight;
				var result = DiceRoller.Roll(2);
				var delta = db.HeightAndMass.First(v => v.ID == result.ToString("X")).DeltaHeight;
				Height = baseHeight + planetSizeMod + delta + genderDelta;
				return true;
			}
			return false;
		}

		public bool GenerateMass()
		{
			if (HomePlanetUWP != null && END.HasValue && Gender.HasValue && DEX.HasValue) {
				var db = new GenerateTablesContext();
				var genderDelta = Gender == DataModel.Gender.Female ? -3.5f : 0;
				var baseHeight = db.HeightAndMass.First(v => v.ID == END.Value.ToString("X")).BaseMass;
				var dexMod = db.HeightAndMass.First(v => v.ID == DEX.Value.ToString("X")).Dex;
				var result = DiceRoller.Roll(2);
				var delta = db.HeightAndMass.First(v => v.ID == result.ToString("X")).DeltaMass;

				Mass = baseHeight + dexMod + delta + genderDelta;
				return true;
			}
			return false;
		}

		public void GenerateHair()
		{
			var generate = new GenerateTablesContext();
			var result = DiceRoller.Roll(4);
			var hair = generate.HairGenerateTable.First(v => v.ID == result).Value;
			Hair = (Hair)hair;
		}

		public void GenerateEye()
		{
			var generate = new GenerateTablesContext();
			var result = DiceRoller.Roll(4);
			var hair = generate.EyeGenerateTable.First(v => v.ID == result && v.Hair == (int)Hair).Value;
			Eye = (Eye)hair;
		}

		public void GenerateSkinTone()
		{
			var result = DiceRoller.Roll(1);
			SkinTone = (SkinTone)result;
		}
	}
}
