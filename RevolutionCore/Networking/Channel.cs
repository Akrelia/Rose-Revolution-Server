using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevolutionCore.Networking
{
    /// <summary>
    /// A channel.
    /// </summary>
    public class Channel
    {
        byte index;
        byte lowAge;
        byte highAge;
        short status;
        string name;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="index">Index of the channel.</param>
        /// <param name="lowAge">Low age of the channel.</param>
        /// <param name="highAge">High age of the channel.</param>
        /// <param name="status">Status of the channel.</param>
        /// <param name="name">Name of the channel.</param>
        public Channel(byte index, byte lowAge, byte highAge, short status, string name)
        {
            this.index = index;
            this.lowAge = lowAge;
            this.highAge = highAge;
            this.status = status;
            this.name = name;
        }

        /// <summary>
        /// Get or set the index.
        /// </summary>
        public byte Index
        {
            get { return index; }
            set { index = value; }
        }

        /// <summary>
        /// Get or set the low age.
        /// </summary>
        public byte LowAge
        {
            get { return lowAge; }
            set { lowAge = value; }
        }

        /// <summary>
        /// Get or set the high age.
        /// </summary>
        public byte HighAge
        {
            get { return highAge; }
            set { highAge = value; }
        }

        /// <summary>
        /// Get or set the status.
        /// </summary>
        public short Status
        {
            get { return status; }
            set { status = value; }
        }

        /// <summary>
        /// Get or set the name.
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
    }
}
