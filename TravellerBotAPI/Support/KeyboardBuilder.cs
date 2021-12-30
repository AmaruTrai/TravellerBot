using System.Collections.Generic;
using Newtonsoft.Json;
using VkNet.Enums.SafetyEnums;
using VkNet.Model.Keyboard;

namespace TravellerBotAPI.Support
{
	public class KeyboardBuilder
	{
		private List<MessageKeyboardButton> currentLine;
		private List<List<MessageKeyboardButton>> buttons { get; set; }

		public static MessageKeyboardButton GetCallbackButton(string label, string payload = null)
		{
			var action = new MessageKeyboardButtonAction() {
				Label = label,
				Type = KeyboardButtonActionType.Callback,
				Payload = payload
			};

			return new MessageKeyboardButton() { Action = action };
		}

		public static MessageKeyboardButton GetCallbackButton(string label, Payload payload = null)
		{
			var action = new MessageKeyboardButtonAction() {
				Label = label,
				Type = KeyboardButtonActionType.Callback,
				Payload = JsonConvert.SerializeObject(payload)
			};

			return new MessageKeyboardButton() { Action = action };
		}

		public KeyboardBuilder()
		{
			buttons = new List<List<MessageKeyboardButton>>();
			currentLine = new List<MessageKeyboardButton>();
			buttons.Add(currentLine);
		}

		public bool AppendLine()
		{
			if (buttons.Count < 5) {
				currentLine = new List<MessageKeyboardButton>();
				buttons.Add(currentLine);
				return true;
			}

			return false;
		}

		public bool AppendCallbackButton(string label, string payload = null)
		{
			if (buttons.Count < 10) {
				currentLine.Add(GetCallbackButton(label, payload));
				return true;
			}

			return false;
		}

		public bool AppendCallbackButton(MessageKeyboardButton button)
		{
			if (buttons.Count < 10) {
				currentLine.Add(button);
				return true;
			}

			return false;
		}

		public MessageKeyboard GetKeyboard()
		{
			return new MessageKeyboard() {Buttons = buttons};
		}
	}
}
