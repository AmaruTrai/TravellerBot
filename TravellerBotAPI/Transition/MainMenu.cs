using System.Drawing;
using TravellerBotAPI.Commands;
using TravellerBotAPI.Support;
using VkNet.Enums.SafetyEnums;

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

			builder.AppendLine();
			button = KeyboardBuilder.GetCallbackButton(
				"Закрыть",
				new Payload() {
					CallbackKey = nameof(CloseCallback),
				});
			button.Color = KeyboardButtonColor.Negative;
			builder.AppendCallbackButton(button);
			KeyBoard = builder.GetKeyboard();
			Text = "Основное меню";
		}
	}
}
