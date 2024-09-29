using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevolutionCore.Networking
{
    /// <summary>
    /// Packet handler.
    /// </summary>
    /// <typeparam name="T">Type of client.</typeparam>
    public abstract partial class PacketHandler<T> where T : RoseClient
    {
        /// <summary>
        /// Handlings.
        /// </summary>
        static public Dictionary<ushort, Func<T, Packet, Task>> Handlings = new Dictionary<ushort, Func<T, Packet, Task>>();
        /// <summary>
        /// Isc Handlings.
        /// </summary>
        static public Dictionary<ushort, Func<IscServer, Packet, Task>> IscHandlings = new Dictionary<ushort, Func<IscServer, Packet, Task>>();

        /// <summary>
        /// Initialize the handlings here;
        /// </summary>
        public abstract void Initialize();
    }
}
