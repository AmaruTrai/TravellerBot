using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using TravellerBotAPI.Support;
using VkNet.Model;
using VkNet.Model.RequestParams;

namespace TravellerBotAPI.Commands
{
	public class UCP
	{
		private static readonly string UCPPattern = @"\bUCP[:]?[ ]*";
		private static readonly string CodPattern;
		public static readonly string Pattern;

		static UCP()
		{
			var builder = new StringBuilder();
			builder.Insert(0, @"[1-9A-Z][ ]?", 6);
			CodPattern = builder.ToString();
			Pattern = UCPPattern + CodPattern;
		}

		public static bool TryParce(string text, out List<string> list)
		{
			list = null;
			var match = Regex.Match(text, CodPattern, RegexOptions.IgnoreCase);
			if (match.Success) {
				text = match.Value;
				var matches = Regex.Matches(text, @"[1-9A-Z]", RegexOptions.IgnoreCase);
				list = matches.ToList().Select(x => x.Value).ToList();
				return true;
			}
			return false;
		}
	}

	public class UCPCommand : IChatCommand
	{
		public string Description => "Установлю значение характеристик";
		public string Example => "UCP: XXXXXX";
		public string[] Keys => new string[] { UCP.Pattern };

		public bool SendReply(Message msg)
		{
			if (!UCP.TryParce(msg.Text, out var characteristics)) {
				return false;
			}

			var db = new CharacterContext();
			var character = db.CreateNewCharacter(msg.UserId.Value);
			character.SetCharacteristic(characteristics.Select(c => Convert.ToInt32(c, 16)).ToList());
			db.SaveChanges();

			UserContext.CreateNewUser(msg.UserId.Value);

			VKManager.Instance.VK.Messages.Send(new MessagesSendParams
			{
				RandomId = new DateTime().Millisecond,
				PeerId = msg.PeerId.Value,
				Message = $"Текущие характеристики {character.GetCharacteristic()}"
			});

			return true;
		}
	}
}
