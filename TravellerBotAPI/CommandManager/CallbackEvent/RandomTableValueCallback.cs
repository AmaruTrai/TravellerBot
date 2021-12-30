using System;
using TravellerBotAPI.Support;
using TravellerBotAPI.Transition;
using VkNet.Model.RequestParams;
using System.Security.Cryptography;

namespace TravellerBotAPI.Commands
{
	public enum TableType
	{
		Aging,
		AlliesAndEnemies,
		Draft,
		Injury,
		LifeEvent,
		PreCareerEvents,
	}

	public interface ITable
	{
		public string GetRandomValue();
	}

	public class AgingTable : ITable
	{
		public string GetRandomValue()
		{
			var db = new InfoTablesContext();
			var result = RandomNumberGenerator.GetInt32(-6, 2);
			return db.Aging.Find(result).Value;
		}
	}

	public class RandomTableValueCallback : CallbackEvent
	{
		public override void Process(EventMessage message)
		{
			var table = message.Payload.Table.Value;
			var a = new AgingTable().GetRandomValue();

			VKManager.Instance.VK.Messages.Send(new MessagesSendParams {
				RandomId = new DateTime().Millisecond,
				PeerId = message.PeerId,
				Message = a
			});
		}
	}
}
