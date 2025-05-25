using RevolutionCore.Configurations;
using RevolutionCore.Networking;
using RevolutionShared.Networking.Packets;
using RoseSandboxServer.Core.Handling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoseSandboxServer.Core
{
    /// <summary>
    /// Sandbox server.
    /// </summary>
    public class SandboxServer : RoseServer<SandboxClient, SandboxPacketHandler>
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public SandboxServer() : base(Configuration.SandboxServerAddress, Configuration.SandboxServerPort, Configuration.SandboxServerAddress, Configuration.SandboxServerPortIsc)
        {
            packetHandler = new SandboxPacketHandler(this, database);
        }

        /// <summary>
        /// Broadcast a packet.
        /// </summary>
        /// <param name="packet">Packet.</param>
        /// <returns>Task.</returns>
        public virtual async Task BroadcastPacket(PacketOut packet)
        {
            for (int i = 0; i < clients.Count; i++)
            {
                await SendPacket(clients[i].TcpClient.GetStream(), packet);
            }
        }

        /// <summary>
        /// Broadcast a packet except one.
        /// </summary>
        /// <param name="packet">Packet.</param>
        /// <returns>Task.</returns>
        public virtual async Task BroadcastPacket(PacketOut packet, SandboxClient client)
        {
            for (int i = 0; i < clients.Count; i++)
            {
                if (clients[i] != client)
                {
                    await SendPacket(clients[i].TcpClient.GetStream(), packet);
                }
            }
        }
    }
}
