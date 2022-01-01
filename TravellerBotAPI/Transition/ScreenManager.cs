using System.Collections.Generic;
using TravellerBotAPI.Commands;
using TravellerBotAPI.Support;
using VkNet.Model.Keyboard;
using KeyboardBuilder = TravellerBotAPI.Support.KeyboardBuilder;

namespace TravellerBotAPI.Transition
{
	public enum Screen
	{
		MainMenu,
		TableMenu,
		CharacterGeneration
	}

	public class ScreenManager
	{
		public static Dictionary<Screen, ScreenKeyBoard> Screens = new Dictionary<Screen, ScreenKeyBoard>() {
			{Screen.MainMenu, new MainMenu()},
			{Screen.TableMenu, new TableMenu()},
			{Screen.CharacterGeneration, new CharacterGeneration()}
		};
	}

	public abstract class ScreenKeyBoard
	{
		public MessageKeyboard KeyBoard { get; protected set; }
		public string Text { get; protected set; }

		protected MessageKeyboardButton mainMenuButton = KeyboardBuilder.GetCallbackButton(
			"Основное меню",
			new Payload() {
				CallbackKey = nameof(SwitchCallback),
				TargetScreen = Screen.MainMenu
			});
	}
}
