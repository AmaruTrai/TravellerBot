using System;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using VkNet.Abstractions;
using System.Net;
using System.Text;
using System.IO;

namespace TravellerBotAPI.Support
{
	public class VKManager
	{
		public static readonly VKManager Instance = new VKManager();

		private IVkApi vk;

		public IVkApi VK => vk;

		public static void Initialize(IVkApi vk)
		{
			Instance.vk = vk;
		}

		public IReadOnlyCollection<VkNet.Model.Attachments.Photo> UploadPhoto(IReadOnlyList<string> text)
		{
			var web = new WebClient();
			var groupID = long.Parse(Environment.GetEnvironmentVariable("GroupID"));
			var serverInfo = vk.Photo.GetMessagesUploadServer(groupID);
			var targetFile = $"{new Random().Next()}.jpg";
			ImageManager.GenerateUWPImage(text, targetFile);
			var responseImg = web.UploadFile(serverInfo.UploadUrl, targetFile);
			var strResponse = Encoding.ASCII.GetString(responseImg);
			var result = vk.Photo.SaveMessagesPhoto(strResponse);
			File.Delete(targetFile);
			return result;
		}

	}
}
