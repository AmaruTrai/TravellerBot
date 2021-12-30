using System;
using TravellerBotAPI.Support;
using TravellerBotAPI.Transition;
using VkNet.Model.RequestParams;

namespace TravellerBotAPI.Commands
{
	public class SwitchCallback : CallbackEvent
	{
		public override void Process(EventMessage message)
		{
			var screen = ScreenManager.Screens[message.Payload.TargetScreen.Value];
			VKManager.Instance.VK.Messages.Send(new MessagesSendParams {
				RandomId = new DateTime().Millisecond,
				PeerId = message.PeerId,
				Message = screen.Text,
				Keyboard = screen.KeyBoard
			});
		}
	}
}
