using RevolutionCore.Utils;
using System;
using System.Net.Sockets;

namespace RevolutionCore.Networking
{
    /// <summary>
    /// A rose client.
    /// </summary>
    public abstract class RoseClient
    {
        /// <summary>
        /// Guid.
        /// </summary>
        protected long id;
        /// <summary>
        /// Pinged.
        /// </summary>
        protected bool pinged;
        /// <summary>
        /// Connect attempts;
        /// </summary>
        protected byte connectAttempts;
        /// <summary>
        /// Packet count.
        /// </summary>
        protected int packetCount;
        /// <summary>
        /// Tcp client.
        /// </summary>
        protected TcpClient tcpClient;
        /// <summary>
        /// Last activity.
        /// </summary>
        protected DateTime lastActivity;
        /// <summary>
        /// Last spam check.
        /// </summary>
        protected DateTime lastSpamCheck;
        /// <summary>
        /// Account.
        /// </summary>
        protected Account account;

        /// <summary>
        /// Parameterless constructor.
        /// </summary>
        public RoseClient()
        {
            RefreshActivity();
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="tcpClient">Tcp client.</param>
        public RoseClient(TcpClient tcpClient) : base()
        {
            this.tcpClient = tcpClient;
        }

        /// <summary>
        /// Refresh the last activity.
        /// </summary>
        public void RefreshActivity()
        {
            pinged = false;

            lastActivity = DateTime.Now;
        }

        /// <summary>
        /// Reset the packet limitation.
        /// </summary>
        public void ResetPacketLimitation()
        {
            packetCount = 0;

            lastSpamCheck = DateTime.Now;
        }

        /// <summary>
        /// Get or set the GUID of the client.
        /// </summary>
        public long ID
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
        /// Get or set if the user is under a ping.
        /// </summary>
        public bool Pinged
        {
            get { return pinged; }
            set { pinged = value; }
        }

        /// <summary>
        /// Get the IP of the client.
        /// </summary>
        public string IP
        {
            get { return tcpClient.Client.RemoteEndPoint.ToString(); }
        }

        /// <summary>
        /// Get the last spam check.
        /// </summary>
        public DateTime LastSpamCheck
        {
            get { return lastSpamCheck; }
        }

        /// <summary>
        /// Get the last activity.
        /// </summary>
        public DateTime LastActivity
        {
            get { return lastActivity; }
        }

        /// <summary>
        /// Get or set the packet count.
        /// </summary>
        public int PacketCount
        {
            get { return packetCount; }
            set { packetCount = value; }
        }

        /// <summary>
        /// Get or set the tcp client of the client.
        /// </summary>
        public TcpClient TcpClient
        {
            get { return tcpClient; }
            set { tcpClient = value; }
        }

        /// <summary>
        /// Get or set the account of the client.
        /// </summary>
        public Account Account
        {
            get { return account; }
            set { account = value; }
        }

        /// <summary>
        /// String format.
        /// </summary>
        /// <returns>Object in string format.</returns>
        public override string ToString()
        {
            return $"({ID}:{IP})";
        }
    }
}