using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using VkNet.Abstractions;
using VkNet.Utils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TravellerBotAPI.DataModel;
using TravellerBotAPI.Support;
using System;

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
					return Ok(Environment.GetEnvironmentVariable("Confirmation"));

				case "message_new":
					var msg = VkNet.Model.Message.FromJson(new VkResponse(message.Object));

					if (Commands.CommandManager.TryGetChatCommand(msg.Text, out var command)) {
						command.SendReply(msg);
					}

					break;

				case "message_event":
					var eventMessage = EventMessage.FromJson(message.Object.ToString());
#if DEBUG
					if (
						Commands.CommandManager.TryGetCallback(eventMessage.Payload.CallbackKey, out var callback)
					) {
						callback.Process(eventMessage);
					}
#else
					try {
						VKManager.Instance.VK.Messages.SendMessageEventAnswer(
							eventMessage.EventId, eventMessage.UserId, eventMessage.PeerId);
					} catch {
						_logger.Log(LogLevel.Information, "Invalid event id");
						break;
					}

					if (
						PeerContext.TryGetPeer(eventMessage.PeerId, out var peer) &&
						peer.UserID == eventMessage.UserId &&
						Commands.CommandManager.TryGetCallback(eventMessage.Payload.CallbackKey, out var callback)
					) {
						callback.Process(eventMessage);
					}
#endif
					break;
			}

			// Возвращаем "ok" серверу Callback API
			return Ok("ok");
		}
	}
}
