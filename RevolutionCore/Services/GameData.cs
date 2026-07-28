using Newtonsoft.Json;
using RevolutionCore.Configurations;
using RevolutionCore.Utils;
using RevolutionShared.JSON;
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
                maps = LoadData<MapData>(Path.Combine(dataPath, "Maps"));
                spawners = LoadData<SpawnData>(Path.Combine(dataPath, "Spawns"));
                enemies = LoadData<EnemyData>(Path.Combine(dataPath, "Enemies"));

                foreach (var map in maps.Values)
                {
                    if (spawners.ContainsKey(map.ID))
                    {
                        map.spawnData = spawners[map.ID];
                    }

                    else
                    {
                        Logger.LogWarning("Missing spawns for map : " + map.MapName);
                    }
                }

                Logger.LogImportantMessage($"Maps loaded : {maps.Count}");
                Logger.LogImportantMessage($"Spawns loaded : {spawners.Count}");
                Logger.LogImportantMessage($"Enemies loaded : {enemies.Count}");
            }

            else
            {
                Logger.LogWarning($"No data folder found (Excepted folder named {configuration.GameDataPath}! The server will not load any data it needs to work");
            }
        }

        public EnemyData GetEnemy(int id)
        {
            return GetData(enemies, id);
        }

        public MapData GetMap(int id)
        {
            return GetData(maps, id);
        }

        public SpawnData GetSpawner(int id)
        {
            return GetData(spawners, id);
        }

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

                    var data = JsonConvert.DeserializeObject<T>(json);

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
