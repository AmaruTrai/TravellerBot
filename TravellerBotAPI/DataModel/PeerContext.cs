using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using TravellerBotAPI.DataModel;

namespace TravellerBotAPI
{
	public class PeerContext : DbContext
	{
		private static readonly PeerContext instant = new PeerContext();

		[MethodImpl(MethodImplOptions.Synchronized)]
		public static bool TryGetPeer(long peerID, out PeerModel peer)
		{
			peer = instant.Peer.FirstOrDefault(p => p.PeerID == peerID);
			if (peer == null) {
				return false;
			}

			return true;
		}

		[MethodImpl(MethodImplOptions.Synchronized)]
		public static void SetUserToPeer(long peerID, long userID)
		{
			if (TryGetPeer(peerID, out var peer)) {
				peer.UserID = userID;
			} else {
				instant.Add<PeerModel>(new PeerModel() { PeerID = peerID, UserID = userID });
			}
			instant.SaveChanges();
		}

		public DbSet<PeerModel> Peer { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlite($"Data Source = TravellerDB.db;");
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{

		}
	}
}
