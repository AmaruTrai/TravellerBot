using System.ComponentModel.DataAnnotations;

namespace TravellerBotAPI.DataModel
{
	public class PeerModel
	{
		[Key]
		public long PeerID { get; set; }
		public long UserID { get; set; }
		public long LastRollTime { get; set; }
	}
}
