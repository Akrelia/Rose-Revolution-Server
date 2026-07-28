using LinqToDB;
using Newtonsoft.Json;
using RevolutionCore.Configurations;
using RevolutionCore.Networking;
using RevolutionCore.Services;
using RevolutionCore.Utils;
using RevolutionShared.Data;
using RevolutionShared.JSON;
using RevolutionShared.Networking.Packets;
using RoseSandboxServer.Core.Data;
using RoseSandboxServer.Core.Data.Entities;
using RoseSandboxServer.Core.Handling;
using RoseSandboxServer.Core.World;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Reflection.Emit;
using System.Text;
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
        /// Initialize the client.
        /// </summary>
        /// <param name="client">Client.</param>
        public override void InitializeClient(SandboxClient client)
        {
            base.InitializeClient(client);

            client.map = configuration.StartingMapID;

            var spawn = maps[client.map].GetDefaultSpawn();
            client.position = new WorldPosition(spawn.X, spawn.Y, spawn.Z);
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

                        for (int i = 0; i < spawn.Count; i++)
                        {
                            var worldPosition = new WorldPosition(spawner.Settings.WorldX, spawner.Settings.WorldZ, spawner.Settings.WorldY);

                            var position = map.GetRandomPointAround(spawner.Settings.Range, worldPosition);

                            var enemyData = gameData.GetEnemy(spawn.ID);

                            if (enemyData != null)
                            {
                                map.SpawnEntityByID(enemyData, worldPosition);
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
                    Logger.LogImportantMessage($"Player {client.Account.username} does not exist in map {map.MapData.MapName}");
                }
            }

            else
            {
                Logger.LogImportantMessage($"Map {client.map} does not exist (this shouldn't happen");
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
