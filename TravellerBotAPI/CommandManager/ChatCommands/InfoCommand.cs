using System;
using VkNet.Model;
using VkNet.Model.RequestParams;
using TravellerBotAPI.Support;
using System.Linq;
using System.Text;

namespace TravellerBotAPI.Commands
{
	public class InfoCommand : IChatCommand
	{
		public string Description => "Информация о доступных командах";
		public string Example => "Help";
		public string[] Keys => new string[] { "help", "помощь", "info", "information" };

		public bool SendReply(Message msg)
		{
			var text = new StringBuilder("Доступные команды:\n");
			foreach (var command in CommandManager.Commands) {
				text.AppendLine($"{command.Example} - {command.Description}");
			}

			VKManager.Instance.VK.Messages.Send(new MessagesSendParams {
				RandomId = new DateTime().Millisecond,
				PeerId = msg.PeerId.Value,
				Message = text.ToString()
			});

			return true;
		}
	}
}
