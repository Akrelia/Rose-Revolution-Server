using RevolutionCore.Networking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace RoseGameServer.Core
{
    /// <summary>
    /// Game client.
    /// </summary>
    public class GameClient : RoseClient
    {
        int clanId;
        int clanRank;
        int channel;
        int charId;

        /// <summary>
        /// Parameterless constructor.
        /// </summary>
        public GameClient() : base()
        {

        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="tcpClient">Tcp client.</param>
        public GameClient(TcpClient tcpClient) : base(tcpClient)
        {
            this.tcpClient = tcpClient;
        }

        /// <summary>
        /// Get or set the clan id.
        /// </summary>
        public int ClanId
        {
            get { return clanId; }
            set { clanId = value; }
        }

        /// <summary>
        /// Get or set the clan rank.
        /// </summary>
        public int ClanRank
        {
            get { return clanRank; }
            set { clanRank = value; }
        }

        /// <summary>
        /// Get or set the channel.
        /// </summary>
        public int Channel
        {
            get { return channel; }
            set { channel = value; }
        }

        /// <summary>
        /// Get or set the char id.
        /// </summary>
        public int CharId
        {
            get { return charId; }
            set { charId = value; }
        }

    }
}
