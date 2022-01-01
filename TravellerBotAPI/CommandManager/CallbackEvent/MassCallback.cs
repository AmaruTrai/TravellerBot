using System;
using TravellerBotAPI.Support;
using VkNet.Model.Keyboard;
using VkNet.Model.RequestParams;

namespace TravellerBotAPI.Commands
{
	public class MassCallback : CallbackEvent
	{
		public override void Process(EventMessage message)
		{
			var db = new CharacterContext();
			if (db.TryGetCharacterByID(message.UserId, out var character) && character.GenerateMass()) {
				db.SaveChanges();
			}

			MessageKeyboard keyboard = null;
			if (message.Payload.IsAppearanceCallback) {
				keyboard = AppearanceCallback.GetKeyboard(AppearanceCallback.Stage.Mass, message.UserId);
			}

			VKManager.Instance.VK.Messages.Send(new MessagesSendParams {
				RandomId = new DateTime().Millisecond,
				PeerId = message.PeerId,
				Message = character.Mass.ToString(),
				Keyboard = keyboard
			});
		}
	}
}
