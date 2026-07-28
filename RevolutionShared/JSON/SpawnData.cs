using RevolutionShared.Rose.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevolutionShared.JSON
{
    /// <summary>
    /// Monster Spawn.
    /// </summary>
    public class EnemySpawn
    {
        public int ID { get; set; }
        public int Count { get; set; }
        public string Description { get; set; }
    }

    /// <summary>
    /// Spawn Data.
    /// </summary>
    public class SpawnData : IData
    {
        public int ID { get; set; }
        public string MapName { get; set; }
        public List<EnemySpawner> Spawners { get; set; }
    }

    /// <summary>
    /// Spawn Settings.
    /// </summary>
    public class SpawnSettings
    {
        public string Name { get; set; }
        public float MapX { get; set; }
        public float MapY { get; set; }
        public int ID { get; set; }
        public float WorldX { get; set; }
        public float WorldY { get; set; }
        public float WorldZ { get; set; }
        public int Interval { get; set; }
        public int LimitCount { get; set; }
        public float Range { get; set; }
        public int TacticPoints { get; set; }
    }

    /// <summary>
    /// Spawn.
    /// </summary>
    public class EnemySpawner
    {
        public SpawnSettings Settings { get; set; }
        public List<EnemySpawn> Basic { get; set; }
        public List<EnemySpawn> Tactic { get; set; }
    }
}
