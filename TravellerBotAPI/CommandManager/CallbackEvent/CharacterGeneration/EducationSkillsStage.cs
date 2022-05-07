using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TravellerBotAPI.Commands;
using TravellerBotAPI.DataModel;
using TravellerBotAPI.Support;
using VkNet.Model.RequestParams;
using Stage = TravellerBotAPI.Commands.CharacterGenerationCallback.Stage;

namespace TravellerBotAPI.CommandManager.CallbackEvent.CharacterGeneration
{
	public class EducationSkillsStage : IGenerationStage
	{
		private List<Skills> standartList = new() {
			Skills.Admin,
			Skills.Advocate,
			Skills.Art,
			Skills.Athletics,
			Skills.Carouse,
			Skills.Electronics,
			Skills.Drive,
			Skills.Engineer,
			Skills.Flyer,
			Skills.Gambler,
			Skills.GunCombat,
			Skills.Language,
			Skills.Mechanic,
			Skills.Medic,
			Skills.Melee,
			Skills.Life,
			Skills.Physical,
			Skills.Social,
			Skills.Space,
			Skills.Broker,
			Skills.VaccSuit
		};

		public Stage Stage => Stage.EducationSkills;

		public void Process(EventMessage message)
		{
			var db = new CharacterContext();
			if (
				db.TryGetCharacterByID(message.UserId, out var character)
			) {
				var userContext = new UserContext();
				var user = userContext.Users.First(v => v.ID == message.UserId);

				if (user.UnallocatedSkillsCount > 0) {
					user.UnallocatedSkills = standartList;
				}

				user.Stage = Stage.HomeplanetSkills;
				db.SaveChanges();
				userContext.SaveChanges();

				VKManager.Instance.VK.Messages.Send(new MessagesSendParams {
					RandomId = new DateTime().Millisecond,
					PeerId = message.PeerId,
					Message = "Выберите дополнительные навыки",
				});

				CharacterGenerationCallback.RunStage(message);
			}

		}
	}
}
