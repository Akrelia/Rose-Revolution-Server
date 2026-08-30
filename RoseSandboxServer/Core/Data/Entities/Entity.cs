using RevolutionShared.Data;
using RevolutionShared.Networking.Packets;
using RevolutionShared.Rose.Data;
using RevolutionShared.Rose.Data.NPC;
using RevolutionCore.Utils;
using RoseSandboxServer.Networking.Contexts;
using System.Threading.Tasks;
using RoseSandboxServer.Core.World;

namespace RoseSandboxServer.Core.Data.Entities
{
    /// <summary>
    /// Entity.
    /// </summary>
    public abstract class Entity : IPacketWritable
    {
        public int id;
        public EntityType type;
        public Map map;
        public WorldPosition position;
        public WorldPosition initialPosition;
        public double startingTime;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="id">Id of the monster.</param>
        /// <param name="dataId">Data.</param>
        public Entity(int id, WorldPosition position, EntityType type, Map map)
        {
            this.id = id;
            this.position = initialPosition = position;
            this.type = type;
            this.map = map;
            this.startingTime = SandboxServer.GameTime; // TODO : make a better way to access gametime that doesn't require a static reference to the server or passing it though a lot of objets
        }

        /// <summary>
        /// Update the entity.
        /// </summary>
        /// <param name="context">Context.</param>
        public virtual async Task Update(TickContext context)
        {
            // Every passive packets happens here (buff finished, death, etc ...)
        }

        /// <summary>
        /// Entity to infos.
        /// </summary>
        /// <returns>Serializable.</returns>
        public EntityInfos ToInfos()
        {
            return new EntityInfos(id, type, position);
        }

        /// <summary>
        /// Entity to sub infos.
        /// </summary>
        /// <returns>Sub infos.</returns>
        public abstract ServerEntityInfos ToSubInfos();

        /// <summary>
        /// Write to packet.
        /// </summary>
        /// <param name="packet">Packet.</param>
        public virtual void WriteToPacket(PacketOut packet)
        {
            packet.AddNew(ToInfos());
         //   packet.AddNew(ToSubInfos());
        }
    }
}
