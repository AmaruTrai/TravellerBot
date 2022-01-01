using System;
using System.Linq;
using TravellerBotAPI.Support;
using VkNet.Model.Keyboard;
using VkNet.Model.RequestParams;

namespace TravellerBotAPI.Commands
{
	public class GenderCallback : CallbackEvent
	{
		public override void Process(EventMessage message)
		{
			var db = new CharacterContext();
			if (db.TryGetCharacterByID(message.UserId, out var character)) {
				character.Gender = message.Payload.Gender;
				db.SaveChanges();
			}

			MessageKeyboard keyboard = null;
			if (message.Payload.IsAppearanceCallback) {
				keyboard = AppearanceCallback.GetKeyboard(AppearanceCallback.Stage.Gender, message.UserId);
			}

			var infoDB = new InfoTablesContext();
			var text = infoDB.Gender.First(v => v.ID == (int)character.Gender).Value;

			VKManager.Instance.VK.Messages.Send(new MessagesSendParams {
				RandomId = new DateTime().Millisecond,
				PeerId = message.PeerId,
				Message = text,
				Keyboard = keyboard
			});
		}
	}
}
