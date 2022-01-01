using System;
using System.Collections.Generic;
using System.Linq;
using TravellerBotAPI.Support;
using VkNet.Model.RequestParams;

namespace TravellerBotAPI.Commands
{
	public class RollCharacteristicsCallback : CallbackEvent
	{
		public override void Process(EventMessage message)
		{
			var db= new CharacterContext();
			var character = db.CreateNewCharacter(message.UserId);
			var values = new int[6];
			values = values.Select(v => v = DiceRoller.Roll(2)).ToArray();
			character.STR = values[0];
			character.DEX = values[1];
			character.END = values[2];
			character.INT = values[3];
			character.EDU = values[4];
			character.SOC = values[5];
			db.SaveChanges();

			VKManager.Instance.VK.Messages.Send(new MessagesSendParams {
				RandomId = new DateTime().Millisecond,
				PeerId = message.PeerId,
				Message = string.Join(" ", values)
			});
		}
	}
}
