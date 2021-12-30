using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc.Routing;
using VkNet.Abstractions;
using VkNet.Model;
using VkNet.Utils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using TravellerBotAPI.Commands;
using TravellerBotAPI.Support;
using VkNet.Enums.SafetyEnums;
using VkNet.Model.Keyboard;
using VkNet.Model.RequestParams;

namespace TravellerBotAPI.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class CallbackController : ControllerBase
	{

		/// <summary>
		/// Конфигурация VK API
		/// </summary>
		private readonly IVkApi _vkApi;

		/// <summary>
		/// Конфигурация приложения
		/// </summary>
		private readonly IConfiguration _configuration;

		/// <summary>
		/// Конфигурация логгера
		/// </summary>
		private readonly ILogger<CallbackController> _logger;

		public CallbackController(IVkApi vkApi, IConfiguration configuration, ILogger<CallbackController> logger)
		{
			_configuration = configuration;
			_vkApi = vkApi;
			_logger = logger;
		}

		// GET: api/<CallbackController>
		[HttpGet]
		public IEnumerable<string> Get()
		{
			return new string[] { "value1", "value2" };
		}

		// POST api/<CallbackController>
		[HttpPost]
		public IActionResult Callback([FromBody] Message message)
		{
			// Проверяем, что находится в поле "type" 
			switch (message.Type)
			{
				// Если это уведомление для подтверждения адреса
				case "confirmation":
					// Отправляем строку для подтверждения 
					return Ok(_configuration["Config:Confirmation"]);

				case "message_new":
					var msg = VkNet.Model.Message.FromJson(new VkResponse(message.Object));
					if (CommandManager.TryGetChatCommand(msg.Text, out var command)) {
						command.SendReply(msg);
					}

					break;

				case "message_event":
					var responce = new VkResponse(message.Object);
					long userId = responce["user_id"];
					string eventId = responce["event_id"];
					long peerId = responce["peer_id"];

					VKManager.Instance.VK.Messages.SendMessageEventAnswer(eventId, userId, peerId);

					break;
			}

			// Возвращаем "ok" серверу Callback API
			return Ok("ok");
		}
	}
}
