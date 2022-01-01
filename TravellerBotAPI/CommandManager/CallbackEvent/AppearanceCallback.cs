using System;
using TravellerBotAPI.DataModel;
using TravellerBotAPI.Support;
using VkNet.Model.Keyboard;
using VkNet.Model.RequestParams;
using KeyboardBuilder = TravellerBotAPI.Support.KeyboardBuilder;

namespace TravellerBotAPI.Commands
{
	public class AppearanceCallback : CallbackEvent
	{
		public enum Stage
		{
			Characteristic,
			Gender,
			HomeWorld,
			Height,
			Mass,
			Hair,
			Eye,
			SkinTone
		}

		public static MessageKeyboard GetKeyboard(Stage stage, long userID)
		{
			var db = new CharacterContext();
			db.TryGetCharacterByID(userID, out var character);

			var builder = new KeyboardBuilder();
			MessageKeyboardButton button = null;

			bool appendNext = false;
			var nextStage = Stage.Characteristic;
			var description = false;

			switch (stage) {
				case Stage.Characteristic:
					appendNext = character != null && character.STR.HasValue;
					nextStage = Stage.Gender;
					button = KeyboardBuilder.GetCallbackButton(
						"Сгенерировать характеристики",
						new Payload() {
							CallbackKey = nameof(RollCharacteristicsCallback),
							IsAppearanceCallback = true
						});
					builder.AppendCallbackButton(button);
					break;

				case Stage.Gender:
					appendNext = character != null && character.Gender.HasValue;
					nextStage = Stage.HomeWorld;
					button = KeyboardBuilder.GetCallbackButton(
						"Мужчина",
						new Payload() {
							CallbackKey = nameof(GenderCallback),
							Gender = Gender.Male,
							IsAppearanceCallback = true
						});
					builder.AppendCallbackButton(button);
					button = KeyboardBuilder.GetCallbackButton(
						"Женщина",
						new Payload() {
							CallbackKey = nameof(GenderCallback),
							Gender = Gender.Female,
							IsAppearanceCallback = true
						});
					builder.AppendCallbackButton(button);
					break;

				case Stage.HomeWorld:
					appendNext = character?.HomePlanetUWP != null;
					nextStage = Stage.Height;
					button = KeyboardBuilder.GetCallbackButton(
						"Случайная планета",
						new Payload() {
							CallbackKey = nameof(RandomPlanetCallback),
							Allegiance = PlanetAllegiance.ThirdImperium,
							IsAppearanceCallback = true
						});
					builder.AppendCallbackButton(button);
					break;

				case Stage.Height:
					appendNext = character != null && character.Height.HasValue;
					nextStage = Stage.Mass;
					button = KeyboardBuilder.GetCallbackButton(
						"Сгенерировать рост",
						new Payload() {
							CallbackKey = nameof(HeightCallback),
							IsAppearanceCallback = true
						});
					builder.AppendCallbackButton(button);
					break;

				case Stage.Mass:
					appendNext = character != null && character.Mass.HasValue;
					nextStage = Stage.Hair;
					button = KeyboardBuilder.GetCallbackButton(
						"Сгенерировать вес",
						new Payload() {
							CallbackKey = nameof(MassCallback),
							IsAppearanceCallback = true
						});
					builder.AppendCallbackButton(button);
					break;


				case Stage.Hair:
					appendNext = character != null && character.Hair.HasValue;
					nextStage = Stage.Eye;
					button = KeyboardBuilder.GetCallbackButton(
						"Сгенерировать цвет волос",
						new Payload() {
							CallbackKey = nameof(HairCallback),
							IsAppearanceCallback = true
						});
					builder.AppendCallbackButton(button);
					break;


				case Stage.Eye:
					appendNext = character != null && character.Eye.HasValue;
					nextStage = Stage.SkinTone;
					button = KeyboardBuilder.GetCallbackButton(
						"Сгенерировать цвет глаз",
						new Payload() {
							CallbackKey = nameof(EyeCallback),
							IsAppearanceCallback = true
						});
					builder.AppendCallbackButton(button);
					break;

				case Stage.SkinTone:
					description= character != null && character.SkinTone.HasValue;
					nextStage = Stage.SkinTone;
					button = KeyboardBuilder.GetCallbackButton(
						"Сгенерировать оттенок кожи",
						new Payload() {
							CallbackKey = nameof(SkinToneCallback),
							IsAppearanceCallback = true
						});
					builder.AppendCallbackButton(button);
					break;

			}

			if (appendNext) {
				builder.AppendLine();
				button = KeyboardBuilder.GetCallbackButton(
					"Продолжить",
					new Payload() {
						CallbackKey = nameof(AppearanceCallback),
						AppearanceStage = nextStage
					});

				builder.AppendCallbackButton(button);
			} else if (description) {
				builder.AppendLine();
				button = KeyboardBuilder.GetCallbackButton(
					"Вывести описание",
					new Payload() {
						CallbackKey = nameof(AppearanceDescriptionCallback),
					});

				builder.AppendCallbackButton(button);
			}

			return builder.GetKeyboard();
		}

		public override void Process(EventMessage message)
		{
			string text = string.Empty;

			switch (message.Payload.AppearanceStage) {
				case Stage.Characteristic:
					text = "Сгенерируйте характеристики";
					break;
				case Stage.Gender:
					text = "Выберите пол персонажа";
					break;
				case Stage.HomeWorld:
					text = "Выберите родной мир";
					break;
				case Stage.Height:
					text = "Сгенерируйте рост персонажа";
					break;
				case Stage.Mass:
					text = "Сгенерируйте вес персонажа";
					break;
				case Stage.Hair:
					text = "Сгенерируйте цвет волос персонажа";
					break;
				case Stage.Eye:
					text = "Сгенерируйте цвет глаз персонажа";
					break;
				case Stage.SkinTone:
					text = "Сгенерируйте оттенок кожи персонажа";
					break;
			}

			var keyboard = GetKeyboard(message.Payload.AppearanceStage.Value, message.UserId);

			VKManager.Instance.VK.Messages.Send(new MessagesSendParams {
				RandomId = new DateTime().Millisecond,
				PeerId = message.PeerId,
				Message = text,
				Keyboard = keyboard
			});

		}
	}
}
