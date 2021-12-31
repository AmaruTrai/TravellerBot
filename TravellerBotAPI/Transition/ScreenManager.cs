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
		TableMenu
	}

	public class ScreenManager
	{
		public static Dictionary<Screen, ScreenKeyBoard> Screens = new Dictionary<Screen, ScreenKeyBoard>() {
			{Screen.MainMenu, new MainMenu()},
			{Screen.TableMenu, new TableMenu()},
		};
	}

	public abstract class ScreenKeyBoard
	{
		public MessageKeyboard KeyBoard { get; protected set; }
		public string Text { get; protected set; }
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
			KeyBoard = builder.GetKeyboard();
			Text = "Основное меню";
		}
	}

	public class TableMenu : ScreenKeyBoard
	{
		public TableMenu()
		{
			var builder = new KeyboardBuilder();

			var button = KeyboardBuilder.GetCallbackButton(
				"Aging",
				new Payload() {
					CallbackKey = nameof(RandomTableValueCallback),
					Table = TableType.Aging
				});

			builder.AppendCallbackButton(button);

			button = KeyboardBuilder.GetCallbackButton(
				"Draft",
				new Payload() {
					CallbackKey = nameof(RandomTableValueCallback),
					Table = TableType.Draft
				});

			builder.AppendCallbackButton(button);

			builder.AppendLine();

			button = KeyboardBuilder.GetCallbackButton(
				"Allies and Enemies",
				new Payload() {
					CallbackKey = nameof(RandomTableValueCallback),
					Table = TableType.AlliesAndEnemies
				});

			builder.AppendCallbackButton(button);

			button = KeyboardBuilder.GetCallbackButton(
				"Injury",
				new Payload() {
					CallbackKey = nameof(RandomTableValueCallback),
					Table = TableType.Injury
				});

			builder.AppendCallbackButton(button);

			builder.AppendLine();

			button = KeyboardBuilder.GetCallbackButton(
				"Life Event",
				new Payload() {
					CallbackKey = nameof(RandomTableValueCallback),
					Table = TableType.LifeEvent
				});

			builder.AppendCallbackButton(button);

			button = KeyboardBuilder.GetCallbackButton(
				"Pre-Career Events",
				new Payload() {
					CallbackKey = nameof(RandomTableValueCallback),
					Table = TableType.PreCareerEvents
				});

			builder.AppendCallbackButton(button);


			builder.AppendLine();

			button = KeyboardBuilder.GetCallbackButton(
				"Случайная планета Third Imperium",
				new Payload() {
					CallbackKey = nameof(RandomPlanetCallback),
					Allegiance = PlanetAllegiance.ThirdImperium
				});

			builder.AppendCallbackButton(button);

			builder.AppendLine();

			button = KeyboardBuilder.GetCallbackButton(
				"Основное меню",
				new Payload() {
					CallbackKey = nameof(SwitchCallback),
					TargetScreen = Screen.MainMenu
				});

			builder.AppendCallbackButton(button);
			KeyBoard = builder.GetKeyboard();
			Text = "Выберите таблицу";
		}
	}
}
