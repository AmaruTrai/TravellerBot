using System;
using TravellerBotAPI.Support;
using VkNet.Model.Keyboard;
using VkNet.Model.RequestParams;

namespace TravellerBotAPI.Commands
{
	public class RollCharacteristicsCallback : CallbackEvent
	{
		public override void Process(EventMessage message)
		{
			var db= new CharacterContext();
			var character = db.CreateNewCharacter(message.UserId);
			var values = character.RollCharacteristic();
			db.SaveChanges();

			MessageKeyboard keyboard = null;
			if (message.Payload.IsAppearanceCallback) {
				keyboard = AppearanceCallback.GetKeyboard(AppearanceCallback.Stage.Characteristic, message.UserId);
			}

			VKManager.Instance.VK.Messages.Send(new MessagesSendParams {
				RandomId = new DateTime().Millisecond,
				PeerId = message.PeerId,
				Message = string.Join(" ", values),
				Keyboard = keyboard
			});
		}
	}
}
