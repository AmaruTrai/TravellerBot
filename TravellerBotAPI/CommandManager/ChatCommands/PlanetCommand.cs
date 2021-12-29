using System;
using System.Collections.Generic;
using VkNet.Model;
using VkNet.Model.RequestParams;
using TravellerBotAPI.Support;
using System.Linq;
using System.Text.RegularExpressions;
using VkNet.Model.Attachments;

namespace TravellerBotAPI.Commands
{
	public class PlanetCommand : IChatCommand
	{
		private readonly string pattern = @"Planet[ ]+.+";
		public string Description => "Информация о планете";
		public string Example => "Planet Capital";
		public string[] Keys => new string[] { pattern };

		public bool SendReply(Message msg)
		{
			var context = new SpaceSystemsContext();
			var name = Regex.Match(msg.Text, pattern).Value;
			name = Regex.Replace(name, @"Planet[ ]+", string.Empty);
			var systems = context.SpaceSystems.Where(s => s.Name == name);

			var text = $"Не удалось найти планету {name} в базе данных.";
			IEnumerable<MediaAttachment> attachments = null;
			if (systems.Count() == 1 && UWP.TryParce(systems.First().UWP, out var uwp)) {
				text = $"Планета {name}, {uwp.Text}";
				attachments = VKManager.Instance.UploadPhoto(uwp.GetDescription());

			} else if (systems.Count() > 1) {
				text = $"В базе данных найдено {systems.Count()} совпадений, введите UWP.";
			}

			VKManager.Instance.VK.Messages.Send(new MessagesSendParams {
				RandomId = new DateTime().Millisecond,
				PeerId = msg.PeerId.Value,
				Message = text,
				Attachments = attachments
			});
			return true;
		}
	}
}
