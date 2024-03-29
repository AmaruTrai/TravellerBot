using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;

namespace TravellerBotAPI.Support
{
	public enum TradeCod
	{
		Agricultural,
		Asteroid,
		Barren,
		Desert,
		FluidOceans,
		Garden,
		HighPopulation,
		HighTech,
		IceCapped,
		Industrial,
		LowPopulation,
		LowTech,
		NonAgricultural,
		NonIndustrial,
		Poor,
		Rich,
		Vacuum,
		WaterWorld
	}

	public class UWP
	{
		private static readonly string UWPPattern = @"\bUWP[:]?[ ]*";
		private static readonly string CodPattern;
		public static readonly string Pattern;

		public string StarportQuality { get; private set; }
		public string PlanetSize { get; private set; }
		public string AtmosphereType { get; private set; }
		public string Hydrographic { get; private set; }
		public string Population { get; private set; }
		public string GovernmentType { get; private set; }
		public string LawLevel { get; private set; }
		public string TechnologicalLevel { get; private set; }

		public string Text { get; private set; }

		static UWP()
		{
			var builder = new StringBuilder();
			builder.Insert(0, @"[0-9A-Z][ ]?", 7);
			builder.Append(@"[ ]?[-–][ ]?[0-9A-Z]");
			CodPattern = builder.ToString();
			Pattern = UWPPattern + CodPattern;
		}

		public static bool TryParce(string text, out UWP uwp)
		{
			uwp = null;
			var match = Regex.Match(text, CodPattern, RegexOptions.IgnoreCase);
			if (match.Success) {
				text = match.Value;
				var matches = Regex.Matches(text, @"[0-9A-Z]", RegexOptions.IgnoreCase);
				var list = matches.ToList().Select(x => x.Value).ToList();
				uwp = new UWP(list);
				return true;
			}
			return false;
		}

		public IReadOnlyList<string> GetDescription()
		{
			var db = new UWPContext();
			var temp = new List<string>();
			temp.Add(db.StarportQuality.Find(StarportQuality).Value);
			temp.Add(db.PlanetSize.Find(PlanetSize).Value);
			temp.Add(db.AtmosphereType.Find(AtmosphereType).Value);
			temp.Add(db.Hydrographic.Find(Hydrographic).Value);
			temp.Add(db.Population.Find(Population).Value);
			temp.Add(db.GovernmentType.Find(GovernmentType).Value);
			temp.Add(db.LawLevel.Find(LawLevel).Value);
			temp.Add(db.TechnologicalLevel.Find(TechnologicalLevel).Value);

			var reply = new List<string>();
			foreach (var line in temp) {
				reply.AddRange(line.Split("\n"));
			}
			reply.AddRange(GetTradeCodesDescription());

			return reply;
		}

		public IReadOnlyList<TradeCod> GetTradeCodes()
		{
			var codes = new List<TradeCod>();
			var atmo = Convert.ToInt32(AtmosphereType, 16);
			var hydro = Convert.ToInt32(Hydrographic, 16);
			var pop = Convert.ToInt32(Population, 16);
			var size = Convert.ToInt32(PlanetSize, 16);
			var gov = Convert.ToInt32(GovernmentType, 16);
			var law = Convert.ToInt32(LawLevel, 16);
			var tech = Convert.ToInt32(TechnologicalLevel, 16);

			if (InRange(atmo, 4, 10) && InRange(hydro, 4, 9) && InRange(pop, 5, 8)) {
				codes.Add(TradeCod.Agricultural);
			}

			if (size == 0 && atmo == 0 && hydro == 0) {
				codes.Add(TradeCod.Asteroid);
			}

			if (pop == 0 && gov == 0 && law == 0) {
				codes.Add(TradeCod.Barren);
			}

			if (InRange(atmo, 2, 10) && hydro == 0) {
				codes.Add(TradeCod.Desert);
			}

			if (atmo >= 10 && hydro >= 1) {
				codes.Add(TradeCod.FluidOceans);
			}

			if (
				InRange(size, 6, 9) &&
				(new int[]{5, 6, 8}).Contains(atmo) &&
				InRange(hydro, 5, 8)
			) {
				codes.Add(TradeCod.Garden);
			}

			if (pop >= 9) {
				codes.Add(TradeCod.HighPopulation);
			}

			if (InRange(atmo, 0, 2) && hydro >= 1) {
				codes.Add(TradeCod.IceCapped);
			}

			if (
				(new int[] { 0, 1, 2, 4, 7, 9, 10, 11, 12 }).Contains(atmo) &&
				pop >= 9
			) {
				codes.Add(TradeCod.Industrial);
			}

			if (InRange(pop, 1, 4)) {
				codes.Add(TradeCod.LowPopulation);
			}

			if (InRange(atmo, 0, 4) && InRange(hydro, 0, 4) && pop >= 6) {
				codes.Add(TradeCod.NonAgricultural);
			}

			if (InRange(pop, 4, 7)) {
				codes.Add(TradeCod.NonIndustrial);
			}

			if (InRange(atmo, 2, 6) && InRange(hydro, 0, 4)) {
				codes.Add(TradeCod.Poor);
			}

			if (
				(new int[] {6, 8}).Contains(atmo) &&
				InRange(pop, 6, 9) &&
				InRange(gov, 4, 10)
			) {
				codes.Add(TradeCod.Rich);
			}

			if (atmo == 0) {
				codes.Add(TradeCod.Vacuum);
			}

			if ((InRange(atmo, 3, 10) || atmo >= 13) && hydro >= 10) {
				codes.Add(TradeCod.WaterWorld);
			}

			if (tech >= 12) {
				codes.Add(TradeCod.HighTech);
			}

			if (pop >= 1 && tech <= 5) {
				codes.Add(TradeCod.LowTech);
			}

			return codes;
		}

		public IReadOnlyList<string> GetTradeCodesDescription()
		{
			var db = new UWPContext();
			var reply = new List<string>();
			foreach (var cod in GetTradeCodes()) {
				reply.AddRange(db.TradeCodes.Find((int)cod).Value.Split("\n"));
			}
			return reply;
		}

		private UWP(List<string> values)
		{
			Text = $"UWP {string.Join("", values.Take(7))}-{values[7]}";
			StarportQuality = values[0];
			PlanetSize = values[1];
			AtmosphereType = values[2];
			Hydrographic = values[3];
			Population = values[4];
			GovernmentType = values[5];
			LawLevel = values[6];
			TechnologicalLevel = values[7];
		}

		private bool InRange(int i, int min, int max) => min <= i && i <= max;
	}
}
