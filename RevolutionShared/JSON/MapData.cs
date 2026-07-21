using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevolutionShared.JSON
{
    public class MapData
    {
        public int MapID { get; set; }
        public string MapName { get; set; }
        public List<MapSpawn> Spawns { get; set; }
        public Dictionary<int, SpawnData> spawnsData;

        public MapData(int mapID, string mapName, List<MapSpawn> spawns)
        {
            MapID = mapID;
            MapName = mapName;
            Spawns = spawns;
        }
    }

    public class MapSpawn
    {
        public string Name { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
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
