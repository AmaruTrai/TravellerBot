using System;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace TravellerBotAPI.Controllers
{
    [Serializable]
    public class Message
    {
        /// <summary>
        /// Тип события
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        /// Объект, инициировавший событие
        /// Структура объекта зависит от типа уведомления
        /// </summary>
        [JsonProperty("object")]
        public JObject Object { get; set; }

        /// <summary>
        /// ID сообщества, в котором произошло событие
        /// </summary>
        [JsonProperty("group_id")]
        public long GroupId { get; set; }

        /// <summary>
        /// ID события
        /// </summary>
        [JsonProperty("event_id")]
        public string EventId { get; set; }
	}
}
