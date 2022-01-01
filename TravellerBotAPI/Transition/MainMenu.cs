using TravellerBotAPI.Commands;
using TravellerBotAPI.Support;

namespace TravellerBotAPI.Transition
{
	public class MainMenu : ScreenKeyBoard
	{

		public MainMenu()
		{
			var builder = new KeyboardBuilder();
			var button = KeyboardBuilder.GetCallbackButton(
				"Меню таблиц",
				new Payload() {
					CallbackKey = nameof(SwitchCallback),
					TargetScreen = Screen.TableMenu
				});

			builder.AppendCallbackButton(button);
			builder.AppendLine();
			button = KeyboardBuilder.GetCallbackButton(
				"Генерация персонажа",
				new Payload() {
					CallbackKey = nameof(SwitchCallback),
					TargetScreen = Screen.CharacterGeneration
				});

			builder.AppendCallbackButton(button);
			KeyBoard = builder.GetKeyboard();
			Text = "Основное меню";
		}
	}
}
