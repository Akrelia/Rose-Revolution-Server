using Newtonsoft.Json;
using RevolutionCore.Configurations;
using RevolutionCore.Utils;
using RevolutionShared.Rose.Data;
using RevolutionShared.Rose.Data.NPC;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace RevolutionCore.Services
{
    /// <summary>
    /// Handles every game data.
    /// </summary>
    public class GameData
    {
        public Dictionary<int, MapData> maps;
        public Dictionary<int, SpawnData> spawners;
        public Dictionary<int, EnemyData> enemies;
        public Dictionary<int, NPCData> npcs;

        public const string MapFolder = "Maps";
        public const string SpawnFolder = "Spawns";
        public const string EnemyFolder = "Enemies";
        public const string NPCFolder = "NPCs";

        /// <summary>
        /// Constructor.
        /// </summary>
        public GameData()
        {
        }

        /// <summary>
        /// Load every game data.
        /// </summary>
        public void Load(string dataPath, ServerConfiguration configuration)
        {
            if (Directory.Exists(dataPath))
            {
                maps = LoadData<MapData>(Path.Combine(dataPath, MapFolder));
                spawners = LoadData<SpawnData>(Path.Combine(dataPath, SpawnFolder));
                enemies = LoadData<EnemyData>(Path.Combine(dataPath, EnemyFolder));
                npcs = LoadData<NPCData>(Path.Combine(dataPath, NPCFolder));

                foreach (var map in maps.Values)
                {
                    if (spawners.ContainsKey(map.ID))
                    {
                        map.spawnData = spawners[map.ID];
                    }

                    else
                    {
                        Logger.LogWarning("Missing spawns for map : " + map.mapName);
                    }
                }

                Logger.LogImportantMessage($"Maps loaded : {maps.Count}");
                Logger.LogImportantMessage($"Spawns loaded : {spawners.Count}");
                Logger.LogImportantMessage($"Enemies loaded : {enemies.Count}");
                Logger.LogImportantMessage($"NPCs loaded : {npcs.Count}");
            }

            else
            {
                Logger.LogWarning($"No data folder found (Excepted folder named {configuration.GameDataPath}! The server will not load any data it needs to work");
            }
        }

        /// <summary>
        /// Get enemy data.
        /// </summary>
        /// <param name="id">ID.</param>
        /// <returns>Enemy data.</returns>
        public EnemyData GetEnemy(int id)
        {
            return GetData(enemies, id);
        }

        /// <summary>
        /// Get NPC data.
        /// </summary>
        /// <param name="id">ID.</param>
        /// <returns>NPC Data.</returns>
        public NPCData GetNPC(int id)
        {
            return GetData(npcs, id);
        }

        /// <summary>
        /// Get map data.
        /// </summary>
        /// <param name="id">ID.</param>
        /// <returns>Map data.</returns>
        public MapData GetMap(int id)
        {
            return GetData(maps, id);
        }

        /// <summary>
        /// Get spawn data.
        /// </summary>
        /// <param name="id">ID.</param>
        /// <returns>Spawn data.</returns>
        public SpawnData GetSpawner(int id)
        {
            return GetData(spawners, id);
        }

        /// <summary>
        /// Get any data.
        /// </summary>
        /// <typeparam name="T">Type of data.</typeparam>
        /// <param name="dictionary">Dictionary.</param>
        /// <param name="id">ID.</param>
        /// <returns>Data.</returns>
        public T GetData<T>(Dictionary<int, T> dictionary, int id) where T : class, IData
        {
            if (dictionary.TryGetValue(id, out var data))
            {
                return data;
            }

            Logger.LogWarning($"Trying to get missing {typeof(T).Name} data for ID : {id}");

            return null;
        }

        /// <summary>
        /// Load data.
        /// </summary>
        /// <typeparam name="T">Idata.</typeparam>
        /// <param name="dataPath">Data path.</param>
        /// <returns>Loaded data.</returns>
        private Dictionary<int, T> LoadData<T>(string dataPath) where T : IData
        {
            var datas = new Dictionary<int, T>();

            var files = Directory.GetFiles(dataPath, "*.json");

            foreach (var file in files)
            {
                try
                {
                    string json = File.ReadAllText(file);

                    var data = JsonConvert.DeserializeObject<T>(json); // REPRISE : NPCs pas lues

                    if (data != null)
                    {
                        datas.Add(data.ID, data);
                    }
                }

                catch (Exception ex)
                {
                    Console.WriteLine($"The file {file} doesn't seems to be a {typeof(T).ToString()} data file : {ex.Message}");
                }
            }

            return datas;
        }
    }
}
