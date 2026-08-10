using RevolutionCore.Configurations;
using RevolutionCore.Networking;
using RevolutionCore.SQL;
using RevolutionCore.Utils;
using RevolutionShared.Attributes;
using RevolutionShared.Data;
using RevolutionShared.Networking.Packets;
using RevolutionShared.Packets;
using RevolutionShared.Rose.Data;
using RoseSandboxServer;
using RoseSandboxServer.Core.Data.Entities;
using System;
using System.Collections.Generic;
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
        public SandboxPacketHandler(SandboxServer server, Database database) : base(server.Configuration, database)
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
            var playerName = packet.GetString();

            client.Account = new Account(playerName, AccountRight.GameMaster); // Everyone is a GM in the Sandbox.

        //    var apparence = packet.DeserializeRecordNew<CharacterAppearance>();

            var startingMap = server.Maps[server.Configuration.StartingMapID];

            startingMap.AddPlayer(client);

            var entities = server.Maps[client.map].GetNearbyEntities(client);

            await server.SendPacket(client, Packets.ConnectionResponse(client, startingMap.GetDefaultSpawn(), startingMap.MapData.ID, entities, server.Configuration.MOTD));

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
            await server.SendPacket(client, Packets.SendWorldInformations(client, server.Clients, server.Configuration.MOTD));
        }

        /// <summary>
        /// Player moving.
        /// </summary>
        /// <param name="client">Client.</param>
        /// <param name="packet">Packet.</param>
        /// <returns>Task.</returns>
        [PacketCommand(ClientCommands.Move)]
        public async Task PlayerMoved(SandboxClient client, PacketIn packet)
        {
            var position = new WorldPosition();

            position.ReadFromPacket(packet);

            client.player.position = position;

            // var entities = server.Maps[client.map].GetNearbyEntities(client);

            var entities = new List<Entity>();

            await server.SendPacket(client, Packets.AddEntities(entities));
            await server.BroadcastPacket(Packets.PlayerMoved(client, position), client);
        }

        /// <summary>
        /// Player executing GM command.
        /// </summary>
        /// <param name="client">Client.</param>
        /// <param name="packet">Packet.</param>
        /// <returns>Task.</returns>
        [PacketCommand(ClientCommands.GMCommandSpawnMonster)]
        public async Task GMSpawnMonster(SandboxClient client, PacketIn packet)
        {
            var monsterID = packet.GetInt();
            var amount = packet.GetShort();

            if (amount > 100)
            {
                amount = 100;
            }

            var map = server.Maps[client.map];

            var spawnedEntities = new List<Entity>();

            for (int i = 0; i < amount; i++)
            {
                var randomPosition = map.GetRandomPointAround(10, client.player.position);

                if (server.GameData.enemies.ContainsKey(monsterID) == false)
                {
                    await server.SendPacket(client, Packets.GMCommandExecuted(client, $"Monster Spawn {monsterID} x {amount} (Failed: Monster ID does not exist)"));
                
                    return;
                }

                var entity = map.SpawnEntityByID(server.GameData.enemies[monsterID], randomPosition);

                spawnedEntities.Add(entity);
            }

            await server.BroadcastPacket(Packets.GMCommandExecuted(client, $"Monster Spawn {monsterID} x {amount}"));
            await server.BroadcastPacket(Packets.AddEntities(spawnedEntities));
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
    public static PacketOut ConnectionResponse(SandboxClient client, MapSpawn spawn, int startingMapID, List<Entity> entities, string motd)
    {
        PacketOut packet = new PacketOut(ServerCommands.SandboxConnectionResponse);

        packet.Add(client.ID);
        packet.Add(client.Account.username);
        packet.Add(startingMapID);

        packet.Add(motd);

        packet.Add(spawn.position.x);
        packet.Add(spawn.position.y);
        packet.Add(spawn.position.z);

        packet.Add(entities);

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

        packet.Add(client.ID);
        packet.Add(message);

        return packet;
    }

    /// <summary>
    /// Packet - World Informations.
    /// </summary>
    /// <param name="clients">Clients.</param>
    /// <returns>Packet.</returns>
    public static PacketOut SendWorldInformations(SandboxClient connecting, List<SandboxClient> clients, string motd)
    {
        PacketOut packet = new PacketOut(ServerCommands.SendWorld);

        packet.Add(motd);

        packet.Add(clients.Count - 1);

        for (int i = 0; i < clients.Count; i++)
        {
            if (clients[i] != connecting)
            {
                var client = clients[i];

                packet.Add(client.ID);

                packet.Add(client.Account.username);

                packet.AddNew(client.player.Appearance);

                packet.Add(client.player.position.x);
                packet.Add(client.player.position.y);
                packet.Add(client.player.position.z);
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

        packet.Add(client.ID);
        packet.Add(client.Account.username);

        packet.SerializeRecordNew(client.player.Appearance);

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

        packet.Add(client.ID);

        return packet;
    }

    /// <summary>
    /// Packet - Player Moved.
    /// </summary>
    /// <param name="client">Client.</param>
    /// <returns>Packet.</returns>
    public static PacketOut PlayerMoved(SandboxClient client, WorldPosition position)
    {
        PacketOut packet = new PacketOut(ServerCommands.PlayerMoved);

        packet.Add(client.ID);

        packet.Add(position.x);
        packet.Add(position.y);
        packet.Add(position.z);

        return packet;
    }

    /// <summary>
    /// Packet - Add Entities.
    /// </summary>
    /// <param name="entities">Entities around.</param>
    /// <returns>Packet.</returns>
    public static PacketOut AddEntities(List<Entity> entities)
    {
        PacketOut packet = new PacketOut(ServerCommands.AddEntities);

        packet.Add(entities);

        return packet;
    }

    /// <summary>
    /// Packet - GM Command Executed.
    /// </summary>
    /// <param name="commandName">Name.</param>
    /// <returns>Packet.</returns>
    public static PacketOut GMCommandExecuted(SandboxClient author, string commandName)
    {
        PacketOut packet = new PacketOut(ServerCommands.GMCommandExecuted);

        packet.Add(author.Account.username);
        packet.Add(commandName);

        return packet;
    }

    /// <summary>
    /// Packet - Update entity.
    /// </summary>
    /// <param name="entity">Entity.</param>
    /// <returns>Packet.</returns>
    public static PacketOut UpdateEntity(Entity entity)
    {
        PacketOut packet = new PacketOut(ServerCommands.EntityUpdate);

        packet.Add(entity.id);

        packet.Add(entity.position);

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