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
