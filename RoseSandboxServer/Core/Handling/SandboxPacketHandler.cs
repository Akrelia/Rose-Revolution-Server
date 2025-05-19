using RevolutionCore.Networking;
using RevolutionCore.SQL;
using RevolutionShared.Attributes;
using RevolutionShared.Networking.Packets;
using RevolutionShared.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoseSandboxServer.Core.Handling
{
    /// <summary>
    /// Sandbox packet handler.
    /// </summary>
    public partial class SandboxPacketHandler : PacketHandler<SandboxClient>
    {
        SandboxServer server;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="server">Server.</param>
        /// <param name="database">Database.</param>
        public SandboxPacketHandler(SandboxServer server, Database database) : base(database)
        {
            this.server = server;
        }

        /// <summary>
        /// Connect a player.
        /// </summary>
        /// <param name="client">Client.</param>
        /// <param name="packet">Packet.</param>
        /// <returns>Task.</returns>
        [PacketCommand(ClientCommands.ConnectSandbox)]
        public async Task PlayerConnected(SandboxClient client, PacketIn packet)
        {
            await SendPacket(client, SandboxPackets.ConnectionResponse());

            // TODO : Send connected player to everyone
        }

        /// <summary>
        /// Disconnect a player.
        /// </summary>
        /// <param name="client">Client.</param>
        /// <param name="packet">Packet.</param>
        /// <returns>Task.</returns>
        [PacketCommand(ClientCommands.DisconnectSandbox)]
        public async Task PlayerDisconnected(SandboxClient client, PacketIn packet)
        {
            server.Disconnect(client);

            // TODO : Send disconnected player to everyone

            await Task.CompletedTask;
        }
    }
}

/// <summary>
/// Packets for Sandbox server.
/// </summary>
public static class SandboxPackets
{
    /// <summary>
    /// Packet - Connection Response.
    /// </summary>
    /// <returns></returns>
    public static PacketOut ConnectionResponse()
    {
        PacketOut packet = new PacketOut(ServerCommands.SandboxConnectionResponse);

        return packet;
    }
}