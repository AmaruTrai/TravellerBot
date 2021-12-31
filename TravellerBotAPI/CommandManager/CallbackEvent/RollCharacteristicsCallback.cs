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
			var table = tables.First(t => t.TableType == message.Payload.Table.Value);
			var result = table.GetRandomValue();

			VKManager.Instance.VK.Messages.Send(new MessagesSendParams {
				RandomId = new DateTime().Millisecond,
				PeerId = message.PeerId,
				Message = result
			});
		}
	}
}
