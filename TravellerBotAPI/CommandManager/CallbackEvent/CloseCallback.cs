using System;
using System.Collections.Generic;
using TravellerBotAPI.Support;
using VkNet.Model.Keyboard;
using VkNet.Model.RequestParams;

namespace TravellerBotAPI.Commands
{
	public class CloseCallback : CallbackEvent
	{
		public override void Process(EventMessage message)
		{
			var empty = new MessageKeyboard() {
				Buttons = new List<IEnumerable<MessageKeyboardButton>>(),
				OneTime = true
			};

			VKManager.Instance.VK.Messages.Send(new MessagesSendParams {
				RandomId = new DateTime().Millisecond,
				PeerId = message.PeerId,
				Message = "Закрыто",
				Keyboard = empty,
			});
		}
	}
}
