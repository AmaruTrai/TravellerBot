using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravellerBotAPI.Commands;
using TravellerBotAPI.DataModel;
using TravellerBotAPI.Support;
using VkNet.Model.Keyboard;
using VkNet.Model.RequestParams;
using KeyboardBuilder = TravellerBotAPI.Support.KeyboardBuilder;
using Stage = TravellerBotAPI.Commands.CharacterGenerationCallback.Stage;

namespace TravellerBotAPI.CommandManager.CallbackEvent.CharacterGeneration
{
	public class SelectSkillStage : IGenerationStage
	{
		public Stage Stage => Stage.SelectSkills;

		private MessageKeyboard GenerateKeyboard(List<Skills> skills)
		{
			if (skills.Count == 0) {
				return null;
			}

			var builder = new KeyboardBuilder();
			int rowCount = 4;
			int countOnRow = skills.Count / rowCount;
			if (skills.Count % rowCount != 0) {
				countOnRow += 1;
			}


			int i = 0;
			foreach (var skill in skills) {
				if (i != 0 && i % countOnRow == 0) {
					builder.AppendLine();
				}

				var button = KeyboardBuilder.GetCallbackButton(
					skill.ToString(),
					new Payload() {
						CallbackKey = nameof(CharacterGenerationCallback),
						SelectedSkill = skill
					});
				builder.AppendCallbackButton(button);
				i++;
			}

			return builder.GetKeyboard();
		}

		public void Process(EventMessage message)
		{
			var db = new UserContext();
			var user = db.Users.First(v => v.ID == message.UserId);

			if (message.Payload.SelectedSkill.HasValue) {
				var characterContext = new CharacterContext();
				var character = characterContext.Characters.First(v => v.UserID == message.UserId);
				character.SetSkillAtLevel(message.Payload.SelectedSkill.Value, 0);
				characterContext.SaveChanges();

				user.UnallocatedSkills.Remove(message.Payload.SelectedSkill.Value);
				user.UnallocatedSkillsCount -= 1;

				if (user.UnallocatedSkillsCount <= 0) {
					user.UnallocatedSkills.Clear();
				}

				db.Update(user);
				db.SaveChanges();
			}

			var text = $"Количество доступных навыков {user.UnallocatedSkillsCount}";
			var keyboard = GenerateKeyboard(user.UnallocatedSkills);

			VKManager.Instance.VK.Messages.Send(new MessagesSendParams {
				RandomId = new DateTime().Millisecond,
				PeerId = message.PeerId,
				Message = text,
				Keyboard = keyboard,
			});

			if (user.UnallocatedSkillsCount <= 0) {
				CharacterGenerationCallback.RunStage(message);
			}
		}
	}
}
