using RevolutionShared.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevolutionShared.Attributes
{
    /// <summary>
    /// Packet Command.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public class PacketCommand : Attribute
    {
        public int Value { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="value">Value.</param>
        public PacketCommand(int value)
        {
            Value = value;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="value">Value.</param>
        public PacketCommand(ClientCommands value)
        {
            Value = Convert.ToInt32(value);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="value">Value.</param>
        public PacketCommand(ServerCommands value)
        {
            Value = Convert.ToInt32(value);
        }
    }
}