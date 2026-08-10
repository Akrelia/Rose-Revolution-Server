using RevolutionShared.Networking.Packets;
using RoseSandboxServer.Core;
using RoseSandboxServer.Core.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace RoseSandboxServer.Networking.Contexts
{
    /// <summary>
    /// Context.
    /// </summary>
    public class Context
    {
        List<ContextPacket> packets;

        /// <summary>
        /// Constructor.
        /// </summary>
        public Context()
        {
            packets = new List<ContextPacket>();
        }

        /// <summary>
        /// Add player packet.
        /// </summary>
        /// <param name="packet">Packet.</param>
        /// <param name="player">Player.</param>
        public void AddPlayerPacket(PacketOut packet, SandboxClient player)
        {
            var playerContext = new PlayerContextPacket(packet, player);

            packets.Add(playerContext);
        }

        /// <summary>
        /// Add zone packet.
        /// </summary>
        /// <param name="packet">Packet.</param>
        /// <param name="zone">Zone.</param>
        public void AddZonePacket(PacketOut packet, SandboxClient originClient)
        {
            var zoneContext = new ZoneContextPacket(packet, originClient);

            packets.Add(zoneContext);
        }

        /// <summary>
        /// Add map packet.
        /// </summary>
        /// <param name="packet">Packet.</param>
        /// <param name="map">Map.</param>
        public void AddMapPacket(PacketOut packet, Map map)
        {
            var nexusContext = new MapContextPacket(packet, map);

            packets.Add(nexusContext);
        }

        /// <summary>
        /// Send all packets in this context.
        /// </summary>
        /// <param name="server">Server.</param>
        /// <returns>Task.</returns>
        public async Task SendAll(SandboxServer server)
        {
            foreach (var contextPacket in packets)
            {
                await contextPacket.Send(server);
            }
        }

        /// <summary>
        /// Get the packets to send during this tick.
        /// </summary>
        public List<ContextPacket> Packets
        {
            get { return packets; }
        }
    }
}
