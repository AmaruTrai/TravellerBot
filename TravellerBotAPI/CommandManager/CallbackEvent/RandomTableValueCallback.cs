using System;
using System.Collections.Generic;
using System.Linq;
using TravellerBotAPI.Support;
using VkNet.Model.RequestParams;

namespace TravellerBotAPI.Commands
{
	public class RandomTableValueCallback : CallbackEvent
	{
		private static List<ITable> tables = new List<ITable>() {
			new AgingTable(),
			new AlliesAndEnemiesTable(),
			new DraftTable(),
			new InjuryTable(),
			new LifeEventTable(),
			new PreCareerEventsTable()
		};

		public override void Process(EventMessage message)
		{
			var table = tables.First(t => t.TableType == message.Payload.Table.Value);
			var result = table.GetRandomValue();

			VKManager.Instance.VK.Messages.Send(new MessagesSendParams {
				RandomId = new DateTime().Millisecond,
				PeerId = message.PeerId,
				Message = result 
			});
		}
	}
}
