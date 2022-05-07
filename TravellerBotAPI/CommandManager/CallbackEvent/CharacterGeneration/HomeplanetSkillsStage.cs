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
	public class HomeplanetSkillsStage : IGenerationStage
	{
		public Stage Stage => Stage.HomeplanetSkills;

		public void Process(EventMessage message)
		{
			var db = new CharacterContext();
			if (
				db.TryGetCharacterByID(message.UserId, out var character) &&
				UWP.TryParce(character.HomePlanetUWP, out var uwp)
			) {
				var dataContext = new GenerateTablesContext();
				var codes = uwp.GetTradeCodes().Select(v => (int) v);
				int count;
				var text = new StringBuilder();

				var userContext = new UserContext();
				var user = userContext.Users.First(v => v.ID == message.UserId);

				int tech = int.Parse(uwp.TechnologicalLevel, System.Globalization.NumberStyles.HexNumber);
				var eduScore = dataContext.BackgroundSkillsCount.First(v => v.EducationScore == character.EDU.Value);
				if (tech <= 5) {
					count = eduScore.LowTech;
				} else if (tech <= 11) {
					count = eduScore.MidTech;
				} else {
					count = eduScore.HighTech;
				}

				var codesSkills =  dataContext.TradeCodesBackgroundSkills.Where(v => codes.Contains(v.ID));
				var skills = new List<Skills>();
				foreach (var item in codesSkills) {
					skills = skills.Union(item.Value).ToList();
				}

				if (count >= skills.Count && skills.Count != 0) {
					text.AppendLine("Добавлены следующие навыки:");
					foreach (var skill in skills) {
						character.SetSkillAtLevel(skill, 0);
						text.AppendLine(skill.ToString());
					}
					user.UnallocatedSkillsCount = Math.Max(0, count - skills.Count);

				} else {
					user.UnallocatedSkills = skills;
					user.UnallocatedSkillsCount = Math.Max(0, count);
					text.AppendLine("Выберите навыки полученные на родной планете");
				}

				user.Stage = Stage.EducationSkills;

				db.SaveChanges();
				userContext.SaveChanges();

				VKManager.Instance.VK.Messages.Send(new MessagesSendParams {
					RandomId = new DateTime().Millisecond,
					PeerId = message.PeerId,
					Message = text.ToString(),
				});

				CharacterGenerationCallback.RunStage(message);
			}

		}
	}
}
