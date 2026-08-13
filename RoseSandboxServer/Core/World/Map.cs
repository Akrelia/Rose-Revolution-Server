using RevolutionCore.Utils;
using RevolutionShared.Data;
using RevolutionShared.Rose.Data;
using RevolutionShared.Rose.Data.NPC;
using RoseSandboxServer.Core.Data.Entities;
using RoseSandboxServer.Networking.Contexts;
using System.Collections.Generic;
using System.Linq;
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
        Dictionary<int, NPC> npcs;
        Dictionary<long, SandboxClient> players;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="mapData">Map data.</param>
        public Map(MapData mapData)
        {
            this.mapData = mapData;

            players = new Dictionary<long, SandboxClient>();
            entities = new Dictionary<int, Entity>();
            npcs = new Dictionary<int, NPC>();
        }

        /// <summary>
        /// Update.
        /// </summary>
        /// <param name="context">Context.</param>
        /// <returns></returns>
        public async Task Update(TickContext context)
        {
            foreach (var player in players.Values)
            {
                await player.Update(context);
            }

            foreach (var entity in entities.Values)
            {
                await entity.Update(context);
            }

            foreach (var npc in npcs.Values)
            {
                await npc.Update(context);
            }
        }

        /// <summary>
        /// Get the default spawn of the map.
        /// </summary>
        /// <returns></returns>
        public MapSpawn GetDefaultSpawn()
        {
            var spawn = mapData.spawns.FirstOrDefault(e => e.name == "start") ?? mapData.spawns.FirstOrDefault(e => e.name == "restore"); // TODO : Avoid those legacy name to something better, like an enum

            return spawn;
        }

        /// <summary>
        /// Spawn entity by ID.
        /// </summary>
        /// <param name="dataID">Data ID.</param>
        /// <param name="position">Position.</param>
        public Entity SpawnNPCByID(NPCData data, WorldPosition position)
        {
            var entityID = GetNewEntityId();

            NPC entity = new NPC(entityID, data, this);

            entity.position = position;

            npcs.Add(entityID, entity);

            return entity;
        }

        /// <summary>
        /// Spawn entity by ID.
        /// </summary>
        /// <param name="dataID">Data ID.</param>
        /// <param name="position">Position.</param>
        public Entity SpawnEntityByID(EnemyData data, WorldPosition position)
        {
            var entityID = GetNewEntityId();

            Enemy entity = new Enemy(entityID, data, this);

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
        public List<SandboxClient> GetNearbyPlayers(SandboxClient client)
        {
            var nearbyPlayers = new List<SandboxClient>();

            for (int i = 0; i < players.Count; i++)
            {
                if (WorldPosition.Distance(client.player.position, players[i].player.position) <= 10000)
                {
                    nearbyPlayers.Add(players[i]);
                }
            }

            return nearbyPlayers;
        }

        /// <summary>
        /// Get entities nearby the client.
        /// </summary>
        /// <param name="client">Client.</param>
        /// <returns>List of nearby entities.</returns>
        public List<Entity> GetNearbyEntities(SandboxClient client)
        {
            var nearbyEntities = new List<Entity>();

            foreach (Entity entity in entities.Values)
            {
                if (WorldPosition.Distance(client.player.position, entity.position) <= 10000)
                {
                    nearbyEntities.Add(entity);
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
                Logger.LogImportantMessage($"Player {client} already exists in map {mapData.mapName}");
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
                Logger.LogImportantMessage($"Player {client} does not exist in map {mapData.mapName}");
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
        /// Get the NPCs.
        /// </summary>
        public Dictionary<int, NPC> NPCs
        {
            get { return npcs; }
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
            return $"[{mapData.ID}] {mapData.mapName}";
        }
    }
}
