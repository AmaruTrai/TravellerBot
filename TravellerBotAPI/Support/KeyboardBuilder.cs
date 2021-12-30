using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VkNet.Enums.SafetyEnums;
using VkNet.Model.Keyboard;

namespace TravellerBotAPI.Support
{
	public class KeyboardBuilder
	{
		private List<MessageKeyboardButton> currentLine;
		private List<List<MessageKeyboardButton>> buttons { get; set; }

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
				var action = new MessageKeyboardButtonAction() {
					Label = label,
					Type = KeyboardButtonActionType.Callback,
					Payload = payload
				};
				currentLine.Add(new MessageKeyboardButton() {
					Action = action
				});
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
