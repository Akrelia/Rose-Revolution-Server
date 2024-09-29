using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace RevolutionCore.Networking
{
    /// <summary>
    /// Another rose server.
    /// </summary>
    public class IscServer
    {
        bool isActive;
        int index;
        int seed;
        short port;
        string address;
        string name;
        readonly TcpClient tcpClient;
        readonly List<Channel> channels;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="index">Index of the server.</param>
        /// <param name="tcpClient">Tcp client.</param>
        public IscServer(int index, TcpClient tcpClient)
        {
            this.isActive = true;
            this.index = index;
            this.tcpClient = tcpClient;
            this.channels = new List<Channel>();
        }

        /// <summary>
        /// Update the server.
        /// </summary>
        /// <returns>Task.</returns>
        public async Task<Packet> UpdateAsync()
        {
            var stream = tcpClient.GetStream();

            if (stream.DataAvailable)
            {
                byte[] buffer = new byte[Packet.BufferSize];

                await stream.ReadAsync(buffer, 0, buffer.Length);

                Packet packet = new Packet(buffer);

                return packet;
            }

            return null;
        }

        /// <summary>
        /// Get or set if the server is active.
        /// </summary>
        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value; }
        }

        /// <summary>
        /// Get or set the index of the server.
        /// </summary>
        public int Index
        {
            get { return index; }
            set { index = value; }
        }

        /// <summary>
        /// Get or set the seed of the server.
        /// </summary>
        public int Seed
        {
            get { return seed; }
            set { seed = value; }
        }

        /// <summary>
        /// Get or set the port of the server.
        /// </summary>
        public short Port
        {
            get { return port; }
            set { port = value; }
        }

        /// <summary>
        /// Get or set the name of the server.
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// Get or set the address of the server.
        /// </summary>
        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        /// <summary>
        /// Get the tcp client.
        /// </summary>
        public TcpClient TcpClient
        {
            get { return tcpClient; }
        }

        /// <summary>
        /// Get the channels of the server.
        /// </summary>
        public List<Channel> Channels
        {
            get { return channels; }
        }
    }
}
