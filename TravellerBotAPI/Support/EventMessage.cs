using Newtonsoft.Json;
using System;
using TravellerBotAPI.Commands;
using TravellerBotAPI.DataModel;
using TravellerBotAPI.Transition;

namespace TravellerBotAPI.Support
{
	[Serializable]
	public class Payload
	{
		/// <summary>
		/// Ключ целевого Callback.
		/// </summary>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string CallbackKey { get; set; } = null;

		/// <summary>
		/// Страница на которую необходимо переключиться.
		/// </summary>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public Screen? TargetScreen { get; set; } = null;

		/// <summary>
		/// Таблица из которой нужно взять значение.
		/// </summary>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public TableType? Table { get; set; } = null;

		/// <summary>
		/// Таблица из которой нужно взять значение.
		/// </summary>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public PlanetAllegiance? Allegiance { get; set; } = null;

		/// <summary>
		/// Таблица из которой нужно взять значение.
		/// </summary>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string TargetValue { get; set; } = null;


		/// <summary>
		/// Таблица из которой нужно взять значение.
		/// </summary>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public AppearanceCallback.Stage? AppearanceStage{ get; set; } = null;

		/// <summary>
		/// Таблица из которой нужно взять значение.
		/// </summary>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public Gender? Gender { get; set; } = null;

		/// <summary>
		/// Таблица из которой нужно взять значение.
		/// </summary>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public bool? IsAppearanceCallback { get; set; }

		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public Skills? SelectedSkill { get; set; }
	}

	[Serializable]
	public class EventMessage
	{
		/// <summary>
		/// ID сообщения в диалоге.
		/// </summary>
		[JsonProperty("conversation_message_id")]
		public long MessageId { get; set; }

		/// <summary>
		/// ID пользователя отправившего сообщение.
		/// </summary>
		[JsonProperty("user_id")]
		public long UserId { get; set; }

		/// <summary>
		/// ID диалога из которого было отправлено сообщение.
		/// </summary>
		[JsonProperty("peer_id")]
		public long PeerId { get; set; }

		/// <summary>
		/// ID события.
		/// </summary>
		[JsonProperty("event_id")]
		public string EventId { get; set; }

		[JsonProperty("payload")]
		public Payload Payload { get; set; }

		public static EventMessage FromJson(string obj)
		{
			return JsonConvert.DeserializeObject<EventMessage>(obj);
		}
	}
}
