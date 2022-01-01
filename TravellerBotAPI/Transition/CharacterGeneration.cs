using TravellerBotAPI.Commands;
using TravellerBotAPI.Support;

namespace TravellerBotAPI.Transition
{
	public class CharacterGeneration : ScreenKeyBoard
	{
		public CharacterGeneration()
		{
			var builder = new KeyboardBuilder();

			var button = KeyboardBuilder.GetCallbackButton(
				"Сгенерировать характеристки персонажа",
				new Payload() {
					CallbackKey = nameof(RollCharacteristicsCallback),
				});

			builder.AppendCallbackButton(button);
			builder.AppendLine();

			builder.AppendCallbackButton(mainMenuButton);

			KeyBoard = builder.GetKeyboard();
			Text = "Выберите таблицу";
		}
	}
}
