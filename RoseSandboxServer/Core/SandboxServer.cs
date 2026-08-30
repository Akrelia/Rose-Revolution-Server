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
using System.Diagnostics;
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

                foreach (var npcSpawn in mapData.npcSpawns)
                {
                    var npcData = gameData.GetNPC(npcSpawn.npcID);

                    if (npcData != null)
                    {
                        map.SpawnNPCByID(npcData, npcSpawn.position);
                    }

                    else
                    {
                        Logger.LogWarning($"Missing enemy data for NPC spawn {npcSpawn.npcID} in map {mapData.mapName}");
                    }
                }
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
                                map.SpawnEnemyByID(enemyData, position);
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

            if (client.player != null && client.player.map != null)
            {
                var map = client.player.map;

                if (map.Players.ContainsKey(client.player.id))
                {
                    map.Players.Remove(client.player.id);
                }

                else
                {
                    Logger.LogImportantMessage($"Player {client.Account.username} does not exist in map {map.MapData.mapName}");
                }
            }

            else
            {
                Logger.LogWarning("Player disconnected was not on a map (should not happen)");
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
                var client = clients.FirstOrDefault(c => c.ID == player.idClient); // TODO : Turn clients into a Dictio ?

                await SendPacket(client, packet);
            }
        }

        /// <summary>
        /// Send a zone-wide packet to all players in the zone.
        /// </summary>
        /// <param name="originClient">Origin client.</param>
        /// <param name="packet">Packet.</param>
        /// <returns>Task.</returns>
        public async Task SendZonePacket(SandboxClient originClient, PacketOut packet, bool ignoreOriginClient)
        {
            var players = originClient.player.map.GetNearbyPlayers(originClient.player);

            foreach (var player in players)
            {
                var client = clients.FirstOrDefault(c => c.ID == player.idClient);

                if (ignoreOriginClient && originClient != null && client.ID == originClient.ID)
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
