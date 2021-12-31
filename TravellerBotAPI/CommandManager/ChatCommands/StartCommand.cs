using System;
using VkNet.Model;
using VkNet.Model.RequestParams;
using TravellerBotAPI.Support;
using System.Text;
using TravellerBotAPI.Transition;

namespace TravellerBotAPI.Commands
{
	public class StartCommand : IChatCommand
	{
		private static string text = "Основное меню";
		public string Description => "Открыть меню";
		public string Example => "Start";
		public string[] Keys => new string[] { "start" };

		public bool SendReply(Message msg)
		{
			PeerContext.SetUserToPeer(msg.PeerId.Value, msg.FromId.Value);
			VKManager.Instance.VK.Messages.Send(new MessagesSendParams {
				RandomId = new DateTime().Millisecond,
				PeerId = msg.PeerId.Value,
				Message = text,
				Keyboard = ScreenManager.Screens[Screen.MainMenu].KeyBoard
			});

			return true;
		}
	}
}
