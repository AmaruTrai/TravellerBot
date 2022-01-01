using System;
using System.Collections.Generic;
using System.Linq;
using TravellerBotAPI.Support;
using VkNet.Model.Keyboard;
using VkNet.Model.RequestParams;

namespace TravellerBotAPI.Commands
{
	public class RandomPlanetCallback : CallbackEvent
	{
		private static List<Allegiance> allegiances = new List<Allegiance>() {
			new ThirdImperium()
		};

		public override void Process(EventMessage message)
		{
			var allegiance = allegiances.First(t => t.TypeAllegiance == message.Payload.Allegiance.Value);
			var planet = allegiance.GetRandomPlanet();
			var text = $"Name: {planet.Name}\nUWP: {planet.UWP}\nAllegiance: {planet.AllegianceExt}";

			var db = new CharacterContext();
			if (db.TryGetCharacterByID(message.UserId, out var character)) {
				character.HomePlanetUWP = planet.UWP;
				db.SaveChanges();
			}

			MessageKeyboard keyboard = null;
			if (message.Payload.IsAppearanceCallback) {
				keyboard = AppearanceCallback.GetKeyboard(AppearanceCallback.Stage.HomeWorld, message.UserId);
			}

			VKManager.Instance.VK.Messages.Send(new MessagesSendParams {
				RandomId = new DateTime().Millisecond,
				PeerId = message.PeerId,
				Message = text,
				Keyboard = keyboard
			});
		}
	}
}
