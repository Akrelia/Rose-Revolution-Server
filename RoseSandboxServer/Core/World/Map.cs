using RevolutionCore.Services;
using RevolutionCore.Utils;
using RevolutionShared.Data;
using RevolutionShared.JSON;
using RevolutionShared.Rose.Data.NPC;
using RoseSandboxServer.Core.Data;
using RoseSandboxServer.Core.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoseSandboxServer.Core.World
{
    /// <summary>
    /// A ROSE Map.
    /// </summary>
    public class Map
    {
        int currentEntityID;
        MapData mapData;
        Dictionary<int, Entity> entities;
        public Dictionary<long, SandboxClient> players;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="mapData">Map data.</param>
        public Map(MapData mapData)
        {
            this.mapData = mapData;

            players = new Dictionary<long, SandboxClient>();
            entities = new Dictionary<int, Entity>();
        }

        /// <summary>
        /// Get the default spawn of the map.
        /// </summary>
        /// <returns></returns>
        public MapSpawn GetDefaultSpawn()
        {
            var spawn = mapData.Spawns.FirstOrDefault(e => e.Name == "start") ?? mapData.Spawns.FirstOrDefault(e => e.Name == "restore"); // TODO : Avoid those legacy name to something better, like an enum

            return spawn;
        }

        /// <summary>
        /// Spawn entity by ID.
        /// </summary>
        /// <param name="dataID">Data ID.</param>
        /// <param name="position">Position.</param>
        public Entity SpawnEntityByID(EnemyData data, WorldPosition position)
        {
            var entityID = GetNewEntityId();

            Enemy entity = new Enemy(entityID, data);

            entity.position = position;

            entities.Add(entityID, entity);

            return entity;
        }

        /// <summary>
        /// Get random point around.
        /// </summary>
        /// <param name="range">Range.</param>
        /// <param name="position">Position.</param>
        /// <returns>Position.</returns>
        public WorldPosition GetRandomPointAround(float range, WorldPosition position)
        {
            return new WorldPosition(position.x + Tools.Random.NextFloat(-range, range), position.y, position.z + Tools.Random.NextFloat(-range, range)); // Square, for efficiency
        }

        /// <summary>
        /// Get a new entity id.
        /// </summary>
        /// <returns>ID.</returns>
        public int GetNewEntityId()
        {
            while (entities.ContainsKey(currentEntityID))
            {
                Logger.LogImportantMessage($"COLLISION WOOHOO ! ID : {currentEntityID}");

                currentEntityID++;
            }

            return currentEntityID++;
        }

        /// <summary>
        /// Get entities nearby the client.
        /// </summary>
        /// <param name="client">Client.</param>
        /// <returns>List of nearby entities.</returns>
        public List<Entity> GetNearbyEntities(SandboxClient client)
        {
            var nearbyEntities = new List<Entity>();

            for (int i = 0; i < entities.Count; i++)
            {
                if (WorldPosition.Distance(client.position, entities[i].position) <= 10)
                {
                    nearbyEntities.Add(entities[i]);
                }
            }

            return nearbyEntities;
        }

        /// <summary>
        /// Add a player to the map.
        /// </summary>
        /// <param name="client">Client.</param>
        public void AddPlayer(SandboxClient client)
        {
            if (!players.ContainsKey(client.ID))
            {
                players.Add(client.ID, client);
            }

            else
            {
                Logger.LogImportantMessage($"Player {client} already exists in map {mapData.MapName}");
            }
        }

        /// <summary>
        /// Remove a player from the map.
        /// </summary>
        /// <param name="client">Client.</param>
        public void RemovePlayer(SandboxClient client)
        {
            if (players.ContainsKey(client.ID))
            {
                players.Remove(client.ID);
            }

            else
            {
                Logger.LogImportantMessage($"Player {client} does not exist in map {mapData.MapName}");
            }
        }

        /// <summary>
        /// Gets the map data associated with this map.
        /// </summary>
        public MapData MapData
        {
            get { return mapData; }
        }

        /// <summary>
        /// Gets the players currently in the map.
        /// </summary>
        public Dictionary<long, SandboxClient> Players
        {
            get { return players; }
        }

        /// <summary>
        /// Gets the entities in the map.
        /// </summary>
        public Dictionary<int, Entity> Entities
        {
            get { return entities; }
        }

        /// <summary>
        /// Returns a string representation of the map, including its ID and name.
        /// </summary>
        /// <returns>String format.</returns>
        public override string ToString()
        {
            return $"[{mapData.ID}] {mapData.MapName}";
        }
    }
}
