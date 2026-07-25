using RevolutionCore.Utils;
using RevolutionShared.JSON;
using RoseSandboxServer.Core.Data;
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
        MapData mapData;
        Dictionary<int, Entity> entities;
        public Dictionary<long, SandboxClient> players;

        int currentEntityID;

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
        /// Spawn an entity in a specific map.
        /// </summary>
        /// <param name="mapId">Map id.</param>
        /// <param name="entity">Entity. to spawn</param>
        /// <param name="position">Initial position.</param>
        public void SpawnEntity(int mapId, Entity entity, Vector3 position)
        {
            entity.position = position;

            entity.id = GetNewEntityId();

            entities.Add(entity.id, entity);
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
                if (Vector3.Distance(client.position, entities[i].position) <= 10)
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
                Logger.LogImportantMessage($"Player {client.PlayerName} already exists in map {mapData.MapName}");
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
                Logger.LogImportantMessage($"Player {client.PlayerName} does not exist in map {mapData.MapName}");
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
            return $"[{mapData.MapID}] {mapData.MapName}";
        }
    }
}
