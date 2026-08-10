using LinqToDB;
using RevolutionCore.Configurations;
using RevolutionCore.Networking;
using RevolutionCore.Services;
using RevolutionCore.Utils;
using RevolutionShared.Data;
using RevolutionShared.Networking.Packets;
using RoseSandboxServer.Core.Handling;
using RoseSandboxServer.Core.World;
using RoseSandboxServer.Networking.Contexts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RoseSandboxServer.Core
{
    /// <summary>
    /// Sandbox server.
    /// </summary>
    public partial class SandboxServer : RoseServer<SandboxClient, SandboxPacketHandler, SandboxConfiguration>
    {
        Dictionary<int, Map> maps;
        GameData gameData;
        SandboxDatabase db = new SandboxDatabase();

        /// <summary>
        /// Constructor.
        /// </summary>
        public SandboxServer()
        {
            gameData = new GameData();

            packetHandler = new SandboxPacketHandler(this, database);

            maps = new Dictionary<int, Map>();

            InitializeMaps();
        }

        /// <summary>
        /// Tick rate.
        /// </summary>
        /// <param name="time">Time.</param>
        /// <returns>Task.</returns>
        public override async Task TickRate(double time)
        {
            foreach (Map map in maps.Values)
            {
                TickContext context = new TickContext(time);

                await map.Update(context);

                for (int i = 0; i < context.Packets.Count; i++)
                {
                    await context.Packets[i].Send(this);
                }
            }
        }

        /// <summary>
        /// Initialize the client.
        /// </summary>
        /// <param name="client">Client.</param>
        public override void InitializeClient(SandboxClient client)
        {
            base.InitializeClient(client);

            client.map = configuration.StartingMapID;

            var spawn = maps[client.map].GetDefaultSpawn();

            client.player.position = spawn.position;
        }

        /// <summary>
        /// Load and build.
        /// </summary>
        public void InitializeMaps()
        {
            Logger.BeginSection("LOADING DATA");

            string dataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, configuration.GameDataPath);

            gameData.Load(dataPath, configuration);

            foreach (var mapData in gameData.maps.Values)
            {
                var map = new Map(mapData);

                maps.Add(mapData.ID, map);
            }

            foreach (var map in maps.Values)
            {
                var spawnData = gameData.GetSpawner(map.MapData.ID);

                if (spawnData != null)
                {
                    foreach (var spawner in map.MapData.spawnData.Spawners)
                    {
                        var spawn = spawner.Basic.PickUp();

                        var worldPosition = new WorldPosition(spawner.Settings.WorldX, spawner.Settings.WorldY, spawner.Settings.WorldZ);

                        for (int i = 0; i < spawn.Count; i++)
                        {
                            var position = map.GetRandomPointAround(spawner.Settings.Range, worldPosition);

                            var enemyData = gameData.GetEnemy(spawn.ID);

                            if (enemyData != null)
                            {
                                map.SpawnEntityByID(enemyData, position);
                            }
                        }
                    }
                }
            }

            Logger.LogImportantMessage($"Maps created : {maps.Count} ");
            Logger.LogImportantMessage($"Monsters spawned : {maps.Values.Sum(map => map.Entities.Count)}");

            Logger.EndSection();
        }

        /// <summary>
        /// Disconnect a client from the server.
        /// </summary>
        /// <param name="client">Client.</param>
        public override void Disconnect(SandboxClient client)
        {
            base.Disconnect(client);

            if (maps.ContainsKey(client.map))
            {
                var map = maps[client.map];

                if (map.Players.ContainsKey(client.ID))
                {
                    map.Players.Remove(client.ID);
                }

                else
                {
                    Logger.LogImportantMessage($"Player {client.Account.username} does not exist in map {map.MapData.mapName}");
                }
            }

            else
            {
                Logger.LogImportantMessage($"Map {client.map} does not exist (this shouldn't happen");
            }
        }

        /// <summary>
        /// Send a map-wide packet to all players in.
        /// </summary>
        /// <param name="mapID">Map ID.</param>
        /// <param name="packet">Packet.</param>
        /// <returns>Task.</returns>
        public async Task SendMapPacket(int mapID, PacketOut packet)
        {
            if (maps.ContainsKey(mapID))
            {
               await SendMapPacket(maps[mapID], packet);
            }

            else
            {
                Logger.LogError($"Map {mapID} does not exist (this shouldn't happen)");
            }
        }

        /// <summary>
        /// Send a map-wide packet to all players in.
        /// </summary>
        /// <param name="map">Map.</param>
        /// <param name="packet">Packet.</param>
        /// <returns>Task.</returns>
        public async Task SendMapPacket(Map map, PacketOut packet)
        {
            foreach (var player in map.Players.Values)
            {
                await SendPacket(player, packet);
            }
        }

        /// <summary>
        /// Send a zone-wide packet to all players in the zone.
        /// </summary>
        /// <param name="originClient">Origin client.</param>
        /// <param name="packet">Packet.</param>
        /// <returns>Task.</returns>
        public async Task SendZonePacket(SandboxClient originClient, PacketOut packet)
        {
            var clients = maps[originClient.map].GetNearbyPlayers(originClient);

            foreach (var client in clients)
            {
                if (originClient != null && client.ID == originClient.ID)
                {
                    continue;
                }

                await SendPacket(client, packet);
            }
        }

        /// <summary>
        /// Get the game data.
        /// </summary>
        public GameData GameData
        {
            get { return gameData; }
        }

        /// <summary>
        /// Get the maps.
        /// </summary>
        public Dictionary<int, Map> Maps
        {
            get { return maps; }
        }
    }
}
