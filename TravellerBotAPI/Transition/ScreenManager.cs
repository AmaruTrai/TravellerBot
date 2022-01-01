using System.Collections.Generic;
using System.Net.Mime;
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

	public class TableMenu : ScreenKeyBoard
	{
		private MessageKeyboardButton GateTableButton(string text, TableType table)
		{
			return KeyboardBuilder.GetCallbackButton(
				text,
				new Payload() {
					CallbackKey = nameof(RandomTableValueCallback),
					Table = table
				});
		}

		public TableMenu()
		{
			var builder = new KeyboardBuilder();

			builder.AppendCallbackButton(GateTableButton("Aging", TableType.Aging));
			builder.AppendCallbackButton(GateTableButton("Draft", TableType.Draft));
			builder.AppendLine();

			builder.AppendCallbackButton(GateTableButton("Allies and Enemies", TableType.AlliesAndEnemies));
			builder.AppendCallbackButton(GateTableButton("Injury", TableType.Injury));
			builder.AppendLine();

			builder.AppendCallbackButton(GateTableButton("Life Event", TableType.LifeEvent));
			builder.AppendCallbackButton(GateTableButton("Pre-Career Events", TableType.PreCareerEvents));
			builder.AppendLine();

			var button = KeyboardBuilder.GetCallbackButton(
				"Случайная планета Third Imperium",
				new Payload() {
					CallbackKey = nameof(RandomPlanetCallback),
					Allegiance = PlanetAllegiance.ThirdImperium
				});

			builder.AppendCallbackButton(button);
			builder.AppendLine();

			builder.AppendCallbackButton(mainMenuButton);

			KeyBoard = builder.GetKeyboard();
			Text = "Выберите таблицу";
		}
	}

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
