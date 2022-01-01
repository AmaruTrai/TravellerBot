using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace TravellerBotAPI.Commands
{
	public class CommandManager
	{
		public static readonly IEnumerable<IChatCommand> Commands;

		private static readonly IEnumerable<CallbackEvent> CallbackEvents;

		static CommandManager()
		{
			var types = Assembly.GetExecutingAssembly().GetExportedTypes();
			var events = types.Where(i => i.IsSubclassOf(typeof(CallbackEvent)));
			CallbackEvents = events.Select(i => (CallbackEvent)Activator.CreateInstance(i));
			var commands = types.Where(i => i.IsAssignableTo(typeof(IChatCommand)) && !i.IsInterface);
			Commands = commands.Select(i => (IChatCommand)Activator.CreateInstance(i));
		}

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

		public static bool TryGetCallback(string text, out CallbackEvent callback)
		{
			foreach (var item in CallbackEvents) {
				if (item.Key == text) {
					callback = item;
					return true;
				}
			}

			callback = null;
			return false;
		}
	}
}
