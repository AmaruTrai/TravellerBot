using System;
using System.Linq;
using TravellerBotAPI.Support;
using VkNet.Model.Keyboard;
using VkNet.Model.RequestParams;

namespace TravellerBotAPI.Commands
{
	public class HairCallback : CallbackEvent
	{
		public override void Process(EventMessage message)
		{
			var db = new CharacterContext();
			if (db.TryGetCharacterByID(message.UserId, out var character)) {
				character.GenerateHair();
				db.SaveChanges();
			}

			MessageKeyboard keyboard = null;
			if (message.Payload.IsAppearanceCallback == true) {
				keyboard = AppearanceCallback.GetKeyboard(AppearanceCallback.Stage.Hair, message.UserId);
			}

			var infoDB = new InfoTablesContext();
			var text = infoDB.Hair.First(v => v.ID == (int) character.Hair).Value;

			VKManager.Instance.VK.Messages.Send(new MessagesSendParams {
				RandomId = new DateTime().Millisecond,
				PeerId = message.PeerId,
				Message = text,
				Keyboard = keyboard
			});
		}
	}
}
