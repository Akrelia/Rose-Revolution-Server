using RevolutionShared.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevolutionShared.Rose.Data
{
    [Serializable]
    public class MapData : IData
    {
        public int id;
        public int planetID;
        public int skyID;
        public string mapName;
        public SpawnData spawnData;
        public MapTime time;
        public List<MapSpawn> spawns;
        public List<NPCSpawn> npcSpawns;
        public int ID { get { return id; } set { id = value; } }

        public MapData(int mapID, string mapName, List<MapSpawn> spawns, List<NPCSpawn> npcSpawns)
        {
            this.id = mapID;
            this.mapName = mapName;
            this.spawns = spawns;
            this.npcSpawns = npcSpawns;
        }
    }

    [Serializable]
    public class MapSpawn
    {
        public string name;
        public WorldPosition position;

        public MapSpawn()
        {

        }

        public MapSpawn(string name, WorldPosition position)
        {
            this.name = name;
            this.position = position;
        }

        public MapSpawn(string name, float x, float y, float z)
        {
            this.name = name;
            this.position = new WorldPosition(x, y, z);
        }
    }

    [Serializable]
    public class NPCSpawn
    {
        public int npcID;
        public string name;
        public WorldPosition position;
        public WorldRotation rotation;

        public NPCSpawn()
        {

        }

        public NPCSpawn(int npcID, string name, WorldPosition position)
        {
            this.npcID = npcID;
            this.name = name;
            this.position = position;
        }

        public NPCSpawn(int npcID, string name, float x, float y, float z)
        {
            this.npcID = npcID;
            this.name = name;
            this.position = new WorldPosition(x, y, z);
        }
    }

    [Serializable]
    public class MapTime
    {
        public int dayPeriod;
        public int morningTime;
        public int dayTime;
        public int eveningTime;
        public int nightTime;

        public MapTime(int dayPeriod, int morningTime, int dayTime, int eveningTime, int nightTime)
        {
            this.dayPeriod = dayPeriod;
            this.morningTime = morningTime;
            this.dayTime = dayTime;
            this.eveningTime = eveningTime;
            this.nightTime = nightTime;
        }
    }
}
