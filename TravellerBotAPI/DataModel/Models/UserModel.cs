using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Stage = TravellerBotAPI.Commands.CharacterGenerationCallback.Stage;

namespace TravellerBotAPI.DataModel
{
	public class UserModel
	{
		[Key]
		public long ID { get; set; }
		public Stage Stage { get; set; } = Stage.HomeplanetSkills;
		public long UnallocatedSkillsCount { get; set; } = 0;
		public List<Skills> UnallocatedSkills { get; set; } = new ();
	}
}
