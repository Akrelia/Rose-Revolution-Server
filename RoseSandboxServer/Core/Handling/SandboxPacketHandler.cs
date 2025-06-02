using RevolutionCore.Configurations;
using RevolutionCore.Networking;
using RevolutionCore.SQL;
using RevolutionShared.Attributes;
using RevolutionShared.Networking.Packets;
using RevolutionShared.Packets;
using RoseSandboxServer;
using RoseSandboxServer.Core.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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

            client.gender = packet.GetByte();
            client.hair = packet.GetByte();
            client.face = packet.GetByte();
            client.back = packet.GetInt();
            client.body = packet.GetInt();
            client.gloves = packet.GetInt();
            client.shoes = packet.GetInt();
            client.mask = packet.GetInt();
            client.hat = packet.GetInt();
            client.weapon = packet.GetInt();
            client.subweapon = packet.GetInt();

            await server.SendPacket(client, Packets.ConnectionResponse(client));

            await server.BroadcastPacket(Packets.PlayerConnected(client), client);
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

            await server.BroadcastPacket(Packets.PlayerDisconnected(client));
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

            await server.BroadcastPacket(Packets.ChatMessageSent(client, message));
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
            await server.SendPacket(client, Packets.SendWorldInformations(client, server.Clients));
        }

        /// <summary>
        /// Player requesting the world.
        /// </summary>
        /// <param name="client">Client.</param>
        /// <param name="packet">Packet.</param>
        /// <returns>Task.</returns>
        [PacketCommand(ClientCommands.Move)]
        public async Task PlayerMoved(SandboxClient client, PacketIn packet)
        {
            var x = packet.GetFloat();
            var y = packet.GetFloat();
            var z = packet.GetFloat();

            Vector3 position = new Vector3(x, y, z);

            client.position = position;

            var entities = server.GetNearbyEntities(client);

            await server.SendPacket(client, Packets.GetSurroundings(entities));
            await server.BroadcastPacket(Packets.PlayerMoved(client, position), client);
        }

        /// <summary>
        /// When the player ping the server.
        /// </summary>
        /// <param name="client">Client.</param>
        /// <param name="packet">Packet.</param>
        /// <returns>Task.</returns>
        [PacketCommand(ClientCommands.Ping)]
        public async Task ActionPing(SandboxClient client, PacketIn packet)
        {
            await server.SendPacket(client, Packets.Pong());
        }

        /// <summary>
        /// When the player ping the server.
        /// </summary>
        /// <param name="client">Client.</param>
        /// <param name="packet">Packet.</param>
        /// <returns>Task.</returns>
        [PacketCommand(ClientCommands.Pong)]
        public async Task ActionPong(SandboxClient client, PacketIn packet)
        {
            client.RefreshActivity();

            await Task.CompletedTask;
        }
    }
}

/// <summary>
/// Packets for Sandbox server.
/// </summary>
public static class Packets
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
    /// <param name="clients">Clients.</param>
    /// <returns>Packet.</returns>
    public static PacketOut SendWorldInformations(SandboxClient connecting, List<SandboxClient> clients)
    {
        PacketOut packet = new PacketOut(ServerCommands.SendWorld);

        packet.Add(Configuration.MOTD);

        packet.Add(clients.Count - 1);

        for (int i = 0; i < clients.Count; i++)
        {
            if (clients[i] != connecting)
            {
                var client = clients[i];

                packet.Add(client.GUID.ToByteArray());

                packet.Add(client.PlayerName);

                packet.Add(client.gender);
                packet.Add(client.hair);
                packet.Add(client.face);
                packet.Add(client.back);
                packet.Add(client.body);
                packet.Add(client.gloves);
                packet.Add(client.shoes);
                packet.Add(client.mask);
                packet.Add(client.hat);
                packet.Add(client.weapon);
                packet.Add(client.subweapon);

                packet.Add(client.position.x);
                packet.Add(client.position.y);
                packet.Add(client.position.z);
            }
        }

        return packet;
    }

    /// <summary>
    /// Packet - Player Connected.
    /// </summary>
    /// <param name="client">Client.</param>
    /// <returns>Packet.</returns>
    public static PacketOut PlayerConnected(SandboxClient client)
    {
        PacketOut packet = new PacketOut(ServerCommands.PlayerConnected);

        packet.Add(client.GUID.ToByteArray());
        packet.Add(client.PlayerName);

        packet.Add(client.gender);
        packet.Add(client.hair);
        packet.Add(client.face);
        packet.Add(client.back);
        packet.Add(client.body);
        packet.Add(client.gloves);
        packet.Add(client.shoes);
        packet.Add(client.mask);
        packet.Add(client.hat);
        packet.Add(client.weapon);
        packet.Add(client.subweapon);

        return packet;
    }

    /// <summary>
    /// Packet - Player Disconnected.
    /// </summary>
    /// <param name="client">Client.</param>
    /// <returns>Packet.</returns>
    public static PacketOut PlayerDisconnected(SandboxClient client)
    {
        PacketOut packet = new PacketOut(ServerCommands.PlayerDisconnected);

        packet.Add(client.GUID.ToByteArray());

        return packet;
    }

    /// <summary>
    /// Packet - Player Moved.
    /// </summary>
    /// <param name="client">Client.</param>
    /// <returns>Packet.</returns>
    public static PacketOut PlayerMoved(SandboxClient client, Vector3 position)
    {
        PacketOut packet = new PacketOut(ServerCommands.PlayerMoved);

        packet.Add(client.GUID.ToByteArray());

        packet.Add(position.x);
        packet.Add(position.y);
        packet.Add(position.z);

        return packet;
    }

    /// <summary>
    /// Packet - Get Surroundings.
    /// </summary>
    /// <param name="entities">Entities around.</param>
    /// <returns>Packet.</returns>
    public static PacketOut GetSurroundings(List<Entity> entities)
    {
        PacketOut packet = new PacketOut(ServerCommands.AddEntities);

        packet.Add(entities.Count);

        for (int i = 0; i < entities.Count; i++)
        {
            var entity = entities[i];

            packet.Add(entity.id);
            packet.Add(entity.dataId);
            packet.Add(entity.position.x * 100F);
            packet.Add(entity.position.y);
            packet.Add(entity.position.z * 100F);
        }

        return packet;
    }
    /// <summary>
    /// Ping Packet.
    /// </summary>
    /// <returns>Packet.</returns>
    public static PacketOut Ping()
    {
        PacketOut packet = new PacketOut(ServerCommands.Ping);

        return packet;
    }

    /// <summary>
    /// Pong Packet.
    /// </summary>
    /// <returns>Packet.</returns>
    public static PacketOut Pong()
    {
        PacketOut packet = new PacketOut(ServerCommands.Pong);

        return packet;
    }
}