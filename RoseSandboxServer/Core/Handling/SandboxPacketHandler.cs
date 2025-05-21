using RevolutionCore.Configurations;
using RevolutionCore.Networking;
using RevolutionCore.SQL;
using RevolutionShared.Attributes;
using RevolutionShared.Networking.Packets;
using RevolutionShared.Packets;
using RoseSandboxServer;
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
        public async Task HandleConnection(SandboxClient client, PacketIn packet)
        {
            client.PlayerName = packet.GetString();

            await SendPacket(client, SandboxPackets.ConnectionResponse(client));
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

        /// <summary>
        /// Player sending a message in chat.
        /// </summary>
        /// <param name="client">Client.</param>
        /// <param name="packet">Packet.</param>
        /// <returns>Task.</returns>
        [PacketCommand(ClientCommands.SendNormalChat)]
        public async Task ChatMessageSent(SandboxClient client, PacketIn packet)
        {
            var message = packet.GetString(130);

            await SendPacket(client, SandboxPackets.ChatMessageSent(client, message)); // TODO : Remove this afterward and sell to all people
        }

        /// <summary>
        /// Player requesting the world.
        /// </summary>
        /// <param name="client">Client.</param>
        /// <param name="packet">Packet.</param>
        /// <returns>Task.</returns>
        [PacketCommand(ClientCommands.GetWorld)]
        public async Task WorldRequested(SandboxClient client, PacketIn packet)
        {
            await SendPacket(client, SandboxPackets.SendWorldInformations(Configuration.MOTD));
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
    public static PacketOut ConnectionResponse(SandboxClient client)
    {
        PacketOut packet = new PacketOut(ServerCommands.SandboxConnectionResponse);

        packet.Add(client.GUID.ToByteArray());
        packet.Add(client.PlayerName);

        return packet;
    }

    /// <summary>
    /// Packet - Chat Message Sent.
    /// </summary>
    /// <param name="playerName">Player's name.</param>
    /// <param name="message">Message.</param>
    /// <returns></returns>
    public static PacketOut ChatMessageSent(SandboxClient client, string message)
    {
        PacketOut packet = new PacketOut(ServerCommands.MessageReceived);

        packet.Add(client.GUID.ToByteArray());
        packet.Add(message);

        return packet;
    }

    /// <summary>
    /// Packet - World Informations.
    /// </summary>
    /// <param name="motd"></param>
    /// <returns>Packet.</returns>
    public static PacketOut SendWorldInformations(string motd)
    {
        PacketOut packet = new PacketOut(ServerCommands.SendWorld);

        packet.Add(motd);

        return packet;
    }
}