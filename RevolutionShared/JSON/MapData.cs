using RevolutionShared.Rose.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevolutionShared.JSON
{
    public class MapData : IData
    {
        public int ID { get; set; }
        public string MapName { get; set; }
        public SpawnData spawnData;
        public List<MapSpawn> Spawns { get; set; }

        public MapData(int mapID, string mapName, List<MapSpawn> spawns)
        {
            ID = mapID;
            MapName = mapName;
            Spawns = spawns;
        }
    }

    public class MapSpawn
    {
        public string Name { get; set; }
        public float X { get; set; }
        public float Y { get; set; } // Turn to WorldPosition
        public float Z { get; set; }

        public MapSpawn(string name, float x, float y, float z)
        {
            Name = name;
            X = x;
            Y = y;
            Z = z;
        }
    }
}
