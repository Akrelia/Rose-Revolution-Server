using LinqToDB;
using Newtonsoft.Json;
using RevolutionCore.Configurations;
using RevolutionCore.Networking;
using RevolutionCore.Utils;
using RevolutionShared.JSON;
using RevolutionShared.Networking.Packets;
using RoseSandboxServer.Core.Data;
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
        SandboxDatabase db = new SandboxDatabase();
        Dictionary<int, Map> maps;

        /// <summary>
        /// Constructor.
        /// </summary>
        public SandboxServer()
        {
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
        }

        /// <summary>
        /// Load and build.
        /// </summary>
        public void InitializeMaps()
        {
            Logger.BeginSection("LOADING DATA");

            string dataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, configuration.GameDataPath);

            if (Directory.Exists(dataPath))
            {
                var mapsData = LoadMaps(Path.Combine(dataPath, "Maps"));
                var spawnsData = LoadSpawns(Path.Combine(dataPath, "Spawns"));

                Logger.LogImportantMessage($"Maps loaded : {mapsData.Count} ");
                Logger.LogImportantMessage($"Spawns loaded : {spawnsData.Count} ");

                foreach (var mapData in mapsData.Values)
                {
                    var map = new Map(mapData);

                    maps.Add(mapData.MapID, map);
                }

                foreach (var spawn in spawnsData.Values)
                {
                    var map = maps[spawn.MapID];

                    foreach (var entity in spawn.Spawners)
                    {
                        var monsterSpawn = entity.Basic.PickUp();

                        for (int i = 0; i < monsterSpawn.Count; i++)
                        {
                            var worldPosition = new Vector3(entity.Settings.WorldX, entity.Settings.WorldZ, entity.Settings.WorldY);

                            var position = RandomPosition(worldPosition, entity.Settings.Range);

                            var monster = new Entity(monsterSpawn.ID, monsterSpawn.ID);

                            map.SpawnEntity(spawn.MapID, monster, worldPosition);
                        }
                    }
                }

                Logger.LogImportantMessage($"Maps created : {maps.Count} ");
                Logger.LogImportantMessage($"Monsters spawned : {maps.Values.Sum(map => map.Entities.Count)}");
            }

            else
            {
                Logger.LogWarning($"No data folder found (Excepted folder named {configuration.GameDataPath}! The server will not load any data it needs to work");
            }

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
                    Logger.LogImportantMessage($"Player {client.PlayerName} does not exist in map {map.MapData.MapName}");
                }
            }

            else
            {
                Logger.LogImportantMessage($"Map {client.map} does not exist (this shouldn't happen");
            }
        }

        /// <summary>
        /// Load all maps from the data folder.
        /// </summary>
        /// <param name="dataPath">Data path.</param>
        /// <returns>Maps data.</returns>
        private Dictionary<int, MapData> LoadMaps(string dataPath)
        {
            var maps = new Dictionary<int, MapData>();

            var files = Directory.GetFiles(dataPath, "*.json");

            foreach (var file in files)
            {
                try
                {
                    string json = File.ReadAllText(file);

                    var mapData = JsonConvert.DeserializeObject<MapData>(json);

                    if (mapData != null)
                    {
                        maps.Add(mapData.MapID, mapData);
                    }
                }

                catch (Exception ex)
                {
                    Console.WriteLine($"The file {file} doesn't seems to be a map data file : {ex.Message}");
                }
            }
            return maps;
        }

        /// <summary>
        /// Load all spawns from the maps.
        /// </summary>
        /// <param name="dataPath">Data path.</param>
        /// <returns>Spawns.</returns>
        private Dictionary<int, SpawnData> LoadSpawns(string dataPath)
        {
            var spawns = new Dictionary<int, SpawnData>();

            var files = Directory.GetFiles(dataPath, "*.json");

            foreach (var file in files)
            {
                try
                {
                    string json = File.ReadAllText(file);

                    var spawnData = JsonConvert.DeserializeObject<SpawnData>(json);

                    if (spawnData != null)
                    {
                        spawns.Add(spawnData.MapID, spawnData);
                    }
                }

                catch (Exception ex)
                {
                    Console.WriteLine($"The file {file} doesn't seems to be a spawn data file : {ex.Message}");
                }
            }

            return spawns;
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
