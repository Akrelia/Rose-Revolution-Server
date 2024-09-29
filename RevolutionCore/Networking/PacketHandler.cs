using RevolutionCore.Configurations;
using RevolutionCore.SQL;
using RevolutionCore.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevolutionCore.Networking
{
    /// <summary>
    /// A packet handler.
    /// </summary>
    public abstract partial class PacketHandler<T> where T : RoseClient
    {
        /// <summary>
        /// Database instance.
        /// </summary>
        protected Database database;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="database">Database instance.</param>
        public PacketHandler(Database database)
        {
            this.database = database;

            Initialize();
        }

        /// <summary>
        /// Handle the packet.
        /// </summary>
        /// <param name="packet">Packet to handle.</param>
        /// <param name="client">Client.</param>
        public async Task Handle(Packet packet, T client)
        {
            if (Handlings.ContainsKey(packet.Command))
            {
                await Handlings[packet.Command](client, packet);
            }

            else
            {
                Logger.LogWarning($"Received unknown packet command : {packet.CommandString} - from {client.IP}");
            }
        }

        /// <summary>
        /// Handle the packet.
        /// </summary>
        /// <param name="packet">Packet to handle.</param>
        /// <param name="server">Client.</param>
        public async Task HandleIsc(Packet packet, IscServer server)
        {
            if (IscHandlings.ContainsKey(packet.Command))
            {
                await IscHandlings[packet.Command](server, packet);
            }

            else
            {
                Logger.LogWarning($"Received unknown isc packet command : {packet.CommandString} (size: {packet.Size}) - from {server.Name}");
            }
        }

        /// <summary>
        /// Send a packet.
        /// </summary>
        /// <param name="packet">Packet to send.</param>
        /// <param name="client">Client to send packet.</param>
        /// <returns>Task.</returns>
        public async Task SendPacket(Packet packet, RoseClient client)
        {
            Logger.LogMessage(Configuration.Verbose, "PACKET-OUT", packet.ToString());

            var size = packet.Size;

      //      packet.EncodeClient();

            await client.TcpClient.GetStream().WriteAsync(packet.Buffer, 0, size);
        }

        /// <summary>
        /// Send a packet.
        /// </summary>
        /// <param name="packet">Packet to send.</param>
        /// <param name="roseServer">Rose server to send packet.</param>
        /// <returns>Task.</returns>
        public async Task SendIscPacket(Packet packet, IscServer roseServer)
        {
            Logger.LogMessage(Configuration.Verbose, "S-PACKET-OUT", packet.ToString());

            var size = packet.Size;

        //    packet.EncodeServer();

            await roseServer.TcpClient.GetStream().WriteAsync(packet.Buffer, 0, size);
        }
    }
}
