using System;
using TravellerBotAPI.Support;
using VkNet.Model;
using VkNet.Model.RequestParams;

namespace TravellerBotAPI.Commands
{
	public class UWPCommand : IChatCommand
	{
		public string Description => "Расшифрую код UWP";
		public string Example => "UWP XXXXXXX-X";
		public string[] Keys => new string[] { UWP.Pattern };

		public bool SendReply(Message msg)
		{
			if (!UWP.TryParce(msg.Text, out var uwp)) {
				return false;
			}

			var attachments = VKManager.Instance.UploadPhoto(uwp.GetDescription());

			VKManager.Instance.VK.Messages.Send(new MessagesSendParams
			{
				RandomId = new DateTime().Millisecond,
				PeerId = msg.PeerId.Value,
				Message = uwp.Text,
				Attachments = attachments
			});

			return true;
		}
	}
}
