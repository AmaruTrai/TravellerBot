using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace TravellerBotAPI.Support
{
	public enum DiceType
	{
		d3,
		d6,
		d66
	}

	public interface IDice
	{
		public int Roll();

		public DiceType DiceTye
		{
			get;
		}
	}

	public class D3 : IDice
	{
		public int Roll() => RandomNumberGenerator.GetInt32(1, 4);
		public DiceType DiceTye => DiceType.d3;
	}

	public class D6 : IDice
	{
		public int Roll() => RandomNumberGenerator.GetInt32(1, 7);
		public DiceType DiceTye => DiceType.d6;
	}

	public class D66 : IDice
	{
		public int Roll()
		{
			return RandomNumberGenerator.GetInt32(1, 7) * 10 + RandomNumberGenerator.GetInt32(1, 7);
		}

		public DiceType DiceTye => DiceType.d66;
	}

	public class DiceRoller
	{
		public static List<IDice> Dice = new List<IDice>() {new D3(), new D6(), new D66()};

		public static int Roll(int count, DiceType diceType = DiceType.d6)
		{
			var dice = Dice.First(d => d.DiceTye == diceType);
			int result = 0;
			for (int i = 0; i < count; i++) {
				result += dice.Roll();
			}

			return result;
		}
	}
}
