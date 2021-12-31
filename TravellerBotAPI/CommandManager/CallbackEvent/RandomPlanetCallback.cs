using System;
using System.Collections.Generic;
using System.Linq;
using TravellerBotAPI.Support;
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
			var result = allegiance.GetRandomPlanet();

			VKManager.Instance.VK.Messages.Send(new MessagesSendParams {
				RandomId = new DateTime().Millisecond,
				PeerId = message.PeerId,
				Message = result
			});
		}
	}
}
