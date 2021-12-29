using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VkNet.Abstractions;
using VkNet.Model;
using VkNet.Model.RequestParams;
using VkNet.Utils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using TravellerBotAPI.Commands;

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
		public IActionResult Callback([FromBody] Updates updates)
		{
			// Проверяем, что находится в поле "type" 
			switch (updates.Type)
			{
				// Если это уведомление для подтверждения адреса
				case "confirmation":
					// Отправляем строку для подтверждения 
					return Ok(_configuration["Config:Confirmation"]);
				case "message_new":
					var msg = Message.FromJson(new VkResponse(updates.Object));
					if (CommandManager.TryGetChatCommand(msg.Text, out var command)) {
						command.SendReply(msg);
					}

					break;
			}
			// Возвращаем "ok" серверу Callback API
			return Ok("ok");
		}
	}
}
