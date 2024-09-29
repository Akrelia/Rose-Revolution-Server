using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace RevolutionCore.Networking
{
    /// <summary>
    /// A rose client.
    /// </summary>
    public abstract class RoseClient
    {
        /// <summary>
        /// Id.
        /// </summary>
        protected int id;
        /// <summary>
        /// Right.
        /// </summary>
        protected short right;
        /// <summary>
        /// Connect attempts;
        /// </summary>
        protected byte connectAttempts;
        /// <summary>
        /// Account name.
        /// </summary>
        protected string accountName;
        /// <summary>
        /// Tcp client.
        /// </summary>
        protected TcpClient tcpClient;

        /// <summary>
        /// Parameterless constructor.
        /// </summary>
        public RoseClient()
        {

        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="tcpClient">Tcp client.</param>
        public RoseClient(TcpClient tcpClient)
        {
            this.tcpClient = tcpClient;
        }

        /// <summary>
        /// Update the client.
        /// </summary>
        /// <returns>Task.</returns>
        public async Task<Packet> UpdateAsync<T>() where T : RoseClient
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
        /// Get or set the id of the client.
        /// </summary>
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        /// <summary>
        /// Get or set the connect attempts.
        /// </summary>
        public byte ConnectAttempts
        {
            get { return connectAttempts; }
            set { connectAttempts = value; }
        }

        /// <summary>
        /// Get or set the right.
        /// </summary>
        public short Right
        {
            get { return right; }
            set { right = value; }
        }

        /// <summary>
        /// Get or set the account name.
        /// </summary>
        public string AccountName
        {
            get { return accountName; }
            set { accountName = value; }
        }

        /// <summary>
        /// Get the IP of the client.
        /// </summary>
        public string IP
        {
            get { return tcpClient.Client.RemoteEndPoint.ToString(); }
        }

        /// <summary>
        /// Get or set the tcp client of the client.
        /// </summary>
        public TcpClient TcpClient
        {
            get { return tcpClient; }
            set { tcpClient = value; }
        }
    }
}