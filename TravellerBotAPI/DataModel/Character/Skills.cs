using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravellerBotAPI.DataModel
{

	[AttributeUsage(System.AttributeTargets.Field)]
	public class ParentSkillAttribute : Attribute
	{
		public Skills Parent;
	}

	[AttributeUsage(System.AttributeTargets.Field)]
	public class DescendantsSkillAttribute : Attribute
	{
		public Skills[] Descendants;
	}

	public enum Skills
	{
		Admin,
		Advocate,

		[DescendantsSkill(Descendants = new [] {Handling, Veterinary, Training})]
		Animals,
		[ParentSkill(Parent = Animals)]
		Handling,
		[ParentSkill(Parent = Animals)]
		Veterinary,
		[ParentSkill(Parent = Animals)]
		Training,

		[DescendantsSkill(Descendants = new [] { Performer, Holography, Instrument, VisualMedia, Write })]
		Art,
		[ParentSkill(Parent = Art)]
		Performer,
		[ParentSkill(Parent = Art)]
		Holography,
		[ParentSkill(Parent = Art)]
		Instrument,
		[ParentSkill(Parent = Art)]
		VisualMedia,
		[ParentSkill(Parent = Art)]
		Write,

		Astrogation,

		[DescendantsSkill(Descendants = new [] { Dexterity, Endurance, Strength })]
		Athletics,
		[ParentSkill(Parent = Athletics)]
		Dexterity,
		[ParentSkill(Parent = Athletics)]
		Endurance,
		[ParentSkill(Parent = Athletics)]
		Strength,

		Broker,
		Carouse,
		Deception,
		Diplomat,

		[DescendantsSkill(Descendants = new [] { Hovercraft, Mole, Track, Walker, Wheel })]
		Drive,
		[ParentSkill(Parent = Drive)]
		Hovercraft,
		[ParentSkill(Parent = Drive)]
		Mole,
		[ParentSkill(Parent = Drive)]
		Track,
		[ParentSkill(Parent = Drive)]
		Walker,
		[ParentSkill(Parent = Drive)]
		Wheel,

		[DescendantsSkill(Descendants = new[] { Comms, Computers, RemoteOps, Sensors })]
		Electronics,
		[ParentSkill(Parent = Electronics)]
		Comms,
		[ParentSkill(Parent = Electronics)]
		Computers,
		[ParentSkill(Parent = Electronics)]
		RemoteOps,
		[ParentSkill(Parent = Electronics)]
		Sensors,

		[DescendantsSkill(Descendants = new[] { Mdrive, Jdrive, LifeSupport, Power })]
		Engineer,
		[ParentSkill(Parent = Engineer)]
		Mdrive,
		[ParentSkill(Parent = Engineer)]
		Jdrive,
		[ParentSkill(Parent = Engineer)]
		LifeSupport,
		[ParentSkill(Parent = Engineer)]
		Power,

		Explosives,

		[DescendantsSkill(Descendants = new[] { Airship, Grav, Ornithopter, Rotor, Wing })]
		Flyer,
		[ParentSkill(Parent = Flyer)]
		Airship,
		[ParentSkill(Parent = Flyer)]
		Grav,
		[ParentSkill(Parent = Flyer)]
		Ornithopter,
		[ParentSkill(Parent = Flyer)]
		Rotor,
		[ParentSkill(Parent = Flyer)]
		Wing,

		Gambler,

		[DescendantsSkill(Descendants = new[] { Turret, Ortillery, Screen, Capital })]
		Gunner,
		[ParentSkill(Parent = Gunner)]
		Turret,
		[ParentSkill(Parent = Gunner)]
		Ortillery,
		[ParentSkill(Parent = Gunner)]
		Screen,
		[ParentSkill(Parent = Gunner)]
		Capital,

		[DescendantsSkill(Descendants = new[] { Archaic, Energy, Slug })]
		GunCombat,
		[ParentSkill(Parent = GunCombat)]
		Archaic,
		[ParentSkill(Parent = GunCombat)]
		Energy,
		[ParentSkill(Parent = GunCombat)]
		Slug,

		[DescendantsSkill(Descendants = new[] { Artillery, Portable, Vehicle })]
		HeavyWeapons,
		[ParentSkill(Parent = HeavyWeapons)]
		Artillery,
		[ParentSkill(Parent = HeavyWeapons)]
		Portable,
		[ParentSkill(Parent = HeavyWeapons)]
		Vehicle,

		Investigate,
		JackOfAllTrades,
		Language,
		Leadership,
		Mechanic,
		Medic,

		[DescendantsSkill(Descendants = new[] { Unarmed, Blade, Bludgeon, Natural })]
		Melee,
		[ParentSkill(Parent = Melee)]
		Unarmed,
		[ParentSkill(Parent = Melee)]
		Blade,
		[ParentSkill(Parent = Melee)]
		Bludgeon,
		[ParentSkill(Parent = Melee)]
		Natural,

		Navigation,
		Persuade,

		[DescendantsSkill(Descendants = new[] { SmallCraft, Spacecraft, CapitalShips })]
		Pilot,
		[ParentSkill(Parent = Pilot)]
		SmallCraft,
		[ParentSkill(Parent = Pilot)]
		Spacecraft,
		[ParentSkill(Parent = Pilot)]
		CapitalShips,
		[ParentSkill(Parent = Pilot)]

		Profession,
		Recon,

		[DescendantsSkill(Descendants = new[] { Physical_Physics, Physical_Chemistry, Physical_Electronics })]
		Physical,
		[ParentSkill(Parent = Physical)]
		Physical_Physics,
		[ParentSkill(Parent = Physical)]
		Physical_Chemistry,
		[ParentSkill(Parent = Physical)]
		Physical_Electronics,

		[DescendantsSkill(Descendants = new[] { Life_Biology, Life_Cybernetics, Life_Genetics, Life_Psionicology })]
		Life,
		[ParentSkill(Parent = Life)]
		Life_Biology,
		[ParentSkill(Parent = Life)]
		Life_Cybernetics,
		[ParentSkill(Parent = Life)]
		Life_Genetics,
		[ParentSkill(Parent = Life)]
		Life_Psionicology,

		[DescendantsSkill(Descendants = new[] { Social_Archeology, Social_Economics, Social_History, Social_Linguistics, Social_Philosophy, Social_Psychology, Social_Sophontology })]
		Social,
		[ParentSkill(Parent = Social)]
		Social_Archeology,
		[ParentSkill(Parent = Social)]
		Social_Economics,
		[ParentSkill(Parent = Social)]
		Social_History,
		[ParentSkill(Parent = Social)]
		Social_Linguistics,
		[ParentSkill(Parent = Social)]
		Social_Philosophy,
		[ParentSkill(Parent = Social)]
		Social_Psychology,
		[ParentSkill(Parent = Social)]
		Social_Sophontology,

		[DescendantsSkill(Descendants = new[] { Space_Planetology, Space_Robotics, Space_Xenology })]
		Space,
		[ParentSkill(Parent = Space)]
		Space_Planetology,
		[ParentSkill(Parent = Space)]
		Space_Robotics,
		[ParentSkill(Parent = Space)]
		Space_Xenology,

		[DescendantsSkill(Descendants = new[] { OceanShips, Personal, Sail, Submarine })]
		Seafarer,
		[ParentSkill(Parent = Seafarer)]
		OceanShips,
		[ParentSkill(Parent = Seafarer)]
		Personal,
		[ParentSkill(Parent = Seafarer)]
		Sail,
		[ParentSkill(Parent = Seafarer)]
		Submarine,

		Stealth,
		Steward,
		Streetwise,
		Survival,

		[DescendantsSkill(Descendants = new[] { Military, Naval })]
		Tactics,
		[ParentSkill(Parent = Tactics)]
		Military,
		[ParentSkill(Parent = Tactics)]
		Naval,

		VaccSuit
	}

	public static class SkillsExtension
	{
		public static bool TryGetParent(this Skills skill, out Skills? parent)
		{
			parent = null;
			var memInfo = skill.GetType().GetMember(skill.ToString());
			var attributes = memInfo[0].GetCustomAttributes(typeof(ParentSkillAttribute), false);
			if (attributes.Length > 0) {
				parent = ((ParentSkillAttribute)attributes[0]).Parent;
			}

			return parent != null;
		}

		public static bool TryGetDescendants(this Skills skill, out Skills[] descendants)
		{
			descendants = null;
			var memInfo = skill.GetType().GetMember(skill.ToString());
			var attributes = memInfo[0].GetCustomAttributes(typeof(DescendantsSkillAttribute), false);
			if (attributes.Length > 0) {
				descendants = ((DescendantsSkillAttribute)attributes[0]).Descendants;
			}
			return descendants != null;
		}
	}

	public partial class Character
	{
		public void SetSkillAtLevel(Skills skill, int level)
		{
			if (Skills.ContainsKey(skill)) {
				Skills[skill] = level;
			} else {
				Skills.Add(skill, level);
				if (skill.TryGetParent(out var parent) && !Skills.ContainsKey(parent.Value)) {
					SetSkillAtLevel(parent.Value, 0);
				}

				if (skill.TryGetDescendants(out var descendants)) {
					foreach (var item in descendants) {
						if (!Skills.ContainsKey(item)) {
							Skills.Add(item, 0);
						}
					}
				}
				
			}
		}
	}
}
