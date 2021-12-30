using TravellerBotAPI.Support;

namespace TravellerBotAPI.Commands
{
	public abstract class CallbackEvent
	{
		public string Key => GetType().Name;

		public abstract void Process(EventMessage message);
	}
}
