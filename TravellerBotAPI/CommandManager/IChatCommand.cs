using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravellerBotAPI.Controllers;
using VkNet.Model;

namespace TravellerBotAPI.Commands
{
	public interface IChatCommand
	{
		public string Description { get; }
		public string Example { get; }
		public string[] Keys { get; }
		public bool SendReply(Message msg);
	}
}
