using RevolutionShared.Networking.Packets;
using RoseSandboxServer.Core;
using RoseSandboxServer.Core.World;
using System.Threading.Tasks;

namespace RoseSandboxServer.Networking.Contexts
{
    /// <summary>
    /// Context packet.
    /// </summary>
    public abstract class ContextPacket
    {
        protected PacketOut packet;
        protected PacketScopeType scope;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="packet">Packet.</param>
        /// <param name="scope">Scope.</param>
        public ContextPacket(PacketOut packet, PacketScopeType scope)
        {
            this.packet = packet;
            this.scope = scope;
        }

        /// <summary>
        /// Send the packet.
        /// </summary>
        /// <param name="server">Server.</param>
        /// <returns>Task.</returns>
        public virtual async Task Send(SandboxServer server)
        {
            await Task.CompletedTask;
        }
    }

    /// <summary>
    /// Nexus context packet.
    /// </summary>
    public class MapContextPacket : ContextPacket
    {
        Map map;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="packet">Packet.</param>
        /// <param name="nexus">Nexus.</param>
        public MapContextPacket(PacketOut packet, Map map) : base(packet, PacketScopeType.Map)
        {
            this.map = map;
        }

        /// <summary>
        /// Send the packet to the nexus.
        /// </summary>
        /// <param name="server">Server.</param>
        /// <returns>Task.</returns>
        public override async Task Send(SandboxServer server)
        {
            await server.SendMapPacket(map.MapData.ID, packet);
        }
    }

    /// <summary>
    /// Zone context packet.
    /// </summary>
    public class ZoneContextPacket : ContextPacket
    {
        SandboxClient sourceClient;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="packet">Packet.</param>
        /// <param name="zone">Zone.</param>
        public ZoneContextPacket(PacketOut packet, SandboxClient sourceClient) : base(packet, PacketScopeType.Zone)
        {
            this.sourceClient = sourceClient;
        }

        /// <summary>
        /// Send the packet to the zone.
        /// </summary>
        /// <param name="server">Server.</param>
        /// <returns>Task.</returns>
        public override async Task Send(SandboxServer server)
        {
            await server.SendZonePacket(sourceClient, packet, false);
        }
    }

    /// <summary>
    /// Player context packet.
    /// </summary>
    public class PlayerContextPacket : ContextPacket
    {
        SandboxClient player;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="packet">Packet.</param>
        /// <param name="player">Player/</param>
        public PlayerContextPacket(PacketOut packet, SandboxClient player) : base(packet, PacketScopeType.Player)
        {
            this.player = player;
        }

        /// <summary>
        /// Send the packet to the player.
        /// </summary>
        /// <param name="server">Server.</param>
        /// <returns>Task.</returns>
        public override async Task Send(SandboxServer server)
        {
            await server.SendPacket(player, packet);
        }
    }

    /// <summary>
    /// Packet scope type.
    /// </summary>
    public enum PacketScopeType
    {
        Map,
        Zone,
        Entity,
        Player
    }
}
