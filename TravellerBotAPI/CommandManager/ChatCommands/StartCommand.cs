using System;
using VkNet.Model;
using VkNet.Model.RequestParams;
using TravellerBotAPI.Support;
using System.Text;

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
			var keyboard = new KeyboardBuilder();
			keyboard.AppendCallbackButton("Help");

			VKManager.Instance.VK.Messages.Send(new MessagesSendParams {
				RandomId = new DateTime().Millisecond,
				PeerId = msg.PeerId.Value,
				Message = text,
				Keyboard = keyboard.GetKeyboard()
			});

			return true;
		}
	}
}
