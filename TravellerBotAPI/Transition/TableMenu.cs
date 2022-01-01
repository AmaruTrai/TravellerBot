using TravellerBotAPI.Commands;
using TravellerBotAPI.Support;
using VkNet.Model.Keyboard;
using KeyboardBuilder = TravellerBotAPI.Support.KeyboardBuilder;

namespace TravellerBotAPI.Transition
{

	public class TableMenu : ScreenKeyBoard
	{
		private MessageKeyboardButton GateTableButton(string text, TableType table)
		{
			return Support.KeyboardBuilder.GetCallbackButton(
				text,
				new Payload() {
					CallbackKey = nameof(RandomTableValueCallback),
					Table = table
				});
		}

		public TableMenu()
		{
			var builder = new Support.KeyboardBuilder();

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
}
