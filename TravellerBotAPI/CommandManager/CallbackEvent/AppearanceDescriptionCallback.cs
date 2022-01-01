using System;
using System.Text;
using TravellerBotAPI.Support;
using TravellerBotAPI.Transition;
using VkNet.Model.RequestParams;
using System.Linq;

namespace TravellerBotAPI.Commands
{
	public class AppearanceDescriptionCallback : CallbackEvent
	{
		public override void Process(EventMessage message)
		{
			var db = new CharacterContext();
			var infoDB = new InfoTablesContext();
			var text = new StringBuilder();
			if (db.TryGetCharacterByID(message.UserId, out var character)) {
				text.AppendLine("Описание:");
				text.AppendLine($"Пол: {infoDB.Gender.First(v => v.ID == (int)character.Gender).Value}");
				text.AppendLine($"Рост: {character.Height} см");
				text.AppendLine($"Вес: {character.Mass} кг");
				text.AppendLine($"Цвет волос: {infoDB.Hair.First(v => v.ID == (int)character.Hair).Value}");
				text.AppendLine($"Цвет глаз: {infoDB.Eye.First(v => v.ID == (int) character.Eye).Value}");
				text.AppendLine($"Оттенок кожи: {infoDB.SkinTone.First(v => v.ID == (int)character.SkinTone).Value}");
			}

			var keyboard = ScreenManager.Screens[Screen.CharacterGeneration].KeyBoard;

			VKManager.Instance.VK.Messages.Send(new MessagesSendParams {
				RandomId = new DateTime().Millisecond,
				PeerId = message.PeerId,
				Message = text.ToString(),
				Keyboard = keyboard
			});
		}
	}
}
