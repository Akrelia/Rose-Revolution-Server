using LinqToDB;
using Newtonsoft.Json;
using RevolutionCore.Configurations;
using RevolutionCore.Networking;
using RevolutionCore.Utils;
using RevolutionShared.JSON;
using RevolutionShared.Networking.Packets;
using RoseSandboxServer.Core.Data;
using RoseSandboxServer.Core.Handling;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Sockets;
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
        Dictionary<int, SpawnData> spawnData;
        Dictionary<int, List<Entity>> entities;

        /// <summary>
        /// Constructor.
        /// </summary>
        public SandboxServer()
        {
            packetHandler = new SandboxPacketHandler(this, database);

            spawnData = new Dictionary<int, SpawnData>();
            entities = new Dictionary<int, List<Entity>>();

            LoadData();

         //   db.Initialize();

            PopulateMaps();
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
        /// Load all the data the sandbox server need.
        /// </summary>
        public void LoadData()
        {
            string dataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");

            if (Directory.Exists(dataPath))
            {
                spawnData = LoadSpawns(Path.Combine(dataPath, "Spawns"));

                Logger.LogImportantMessage($"{spawnData.Count} spawns file(s) loaded");
            }

            else
            {
                Logger.LogWarning($"No data folder found ! The server will not load anything");
            }
        }

        /// <summary>
        /// Populate every maps with the data read from the files.
        /// </summary>
        public void PopulateMaps()
        {
            foreach (var spawn in spawnData.Values)
            {
                if (!entities.ContainsKey(spawn.MapID))
                {
                    entities[spawn.MapID] = new List<Entity>();
                }

                foreach (var entity in spawn.Spawners)
                {
                    var monsterSpawn = entity.Basic.PickUp();

                    for (int j = 0; j < monsterSpawn.Count; j++)
                    {
                        var worldPosition = new Vector3(entity.Settings.WorldX, entity.Settings.WorldZ, entity.Settings.WorldY);

                        var position = RandomPosition(worldPosition, entity.Settings.Range);

                        var monster = new Entity(monsterSpawn.ID, monsterSpawn.ID);

                        SpawnEntity(spawn.MapID, monster, worldPosition);
                    }
                }
            }

            Logger.LogImportantMessage($"{entities.Count} map(s) populated with {entities.Sum(e => e.Value.Count)} entities created");
        }

        /// <summary>
        /// Spawn an entity in a specific map.
        /// </summary>
        /// <param name="mapId">Map id.</param>
        /// <param name="entity">Entity. to spawn</param>
        /// <param name="position">Initial position.</param>
        public void SpawnEntity(int mapId, Entity entity, Vector3 position)
        {
            entity.position = position;

            entity.id = RandomInt();

            if (entities.ContainsKey(mapId))
            {
                entities[mapId].Add(entity);
            }

            else
            {
                entities.Add(mapId, new List<Entity> { entity });
            }
        }

        /// <summary>
        /// Get entities nearby the client.
        /// </summary>
        /// <param name="client">Client.</param>
        /// <returns>List of nearby entities.</returns>
        public List<Entity> GetNearbyEntities(SandboxClient client)
        {
            var entitiesMap = entities[client.map];

            var nearbyEntities = new List<Entity>();

            for (int i = 0; i < entitiesMap.Count; i++)
            {
                if (Vector3.Distance(client.position, entitiesMap[i].position) <= 10)
                {
                    nearbyEntities.Add(entitiesMap[i]);
                }
            }

            return nearbyEntities;
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
    }
}
