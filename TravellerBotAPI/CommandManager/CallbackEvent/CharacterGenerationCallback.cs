using System;
using System.Collections.Generic;
using System.Linq;
using TravellerBotAPI.CommandManager.CallbackEvent.CharacterGeneration;
using TravellerBotAPI.Support;
using VkNet.Model.Keyboard;
using VkNet.Model.RequestParams;

namespace TravellerBotAPI.Commands
{
	public class CharacterGenerationCallback : CallbackEvent
	{
		private static readonly List<IGenerationStage> stages = new() {
			new HomeplanetSkillsStage(),
			new SelectSkillStage(),
			new EducationSkillsStage()
		};

		public enum Stage
		{
			SelectSkills,


			HomeplanetSkills,
			EducationSkills,
		}

		public static void RunStage(EventMessage message)
		{
			var db = new UserContext();
			var user = db.Users.First(v => v.ID == message.UserId);

			if (user.UnallocatedSkillsCount > 0 && user.UnallocatedSkills.Count > 0) {
				stages.FirstOrDefault(v => v.Stage == Stage.SelectSkills)?.Process(message);
			} else {
				stages.FirstOrDefault(v => v.Stage == user.Stage)?.Process(message);
			}

		}

		public override void Process(EventMessage message)
		{
			RunStage(message);
		}
	}
}
