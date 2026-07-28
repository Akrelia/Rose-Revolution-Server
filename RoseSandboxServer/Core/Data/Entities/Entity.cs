using RevolutionShared.Data;
using RevolutionShared.Networking.Packets;
using RevolutionShared.Rose.Data;
using RevolutionShared.Rose.Data.NPC;
using RevolutionCore.Utils;

namespace RoseSandboxServer.Core.Data.Entities
{
    /// <summary>
    /// Entity.
    /// </summary>
    public abstract class Entity : IPacketWritable
    {
        public int id;
        public int dataID;
        public EntityType type;
        public WorldPosition position;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="id">Id of the monster.</param>
        /// <param name="dataId">Data.</param>
        public Entity(int id, EntityType type, int dataID)
        {
            this.id = id;
            this.type = type;
            this.dataID = dataID;
        }

        /// <summary>
        /// Entity to infos.
        /// </summary>
        /// <returns>Serializable.</returns>
        public EntityInfos ToInfos()
        {
            return new EntityInfos(id, type, dataID, position);
        }

        public abstract EntitySubInfos ToSubInfos();

        /// <summary>
        /// Write to packet.
        /// </summary>
        /// <param name="packet">Packet.</param>
        public virtual void WriteToPacket(PacketOut packet)
        {
            packet.AddNew(ToInfos());
            packet.AddNew(ToSubInfos());
        }
    }
}
