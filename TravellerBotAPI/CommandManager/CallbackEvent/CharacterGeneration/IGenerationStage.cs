using TravellerBotAPI.Commands;
using TravellerBotAPI.Support;

namespace TravellerBotAPI.CommandManager.CallbackEvent.CharacterGeneration
{
	interface IGenerationStage
	{
		public CharacterGenerationCallback.Stage Stage { get; }
		public void Process(EventMessage message);
	}
}
