using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace TravellerBotAPI.Commands
{
	public class CommandManager
	{
		private static IEnumerable<IChatCommand> Commands => new List<IChatCommand>() {
			new UWPCommand()
		};

		public static bool TryGetChatCommand(string text, out IChatCommand command)
		{

			foreach (var item in Commands) {
				foreach (var key in item.Keys) {
					var match = Regex.Match(text, key, RegexOptions.IgnoreCase);
					if (match.Success) {
						command = item;
						return true;
					}
				}
			}

			command = null;
			return false;
		}
	}
}
