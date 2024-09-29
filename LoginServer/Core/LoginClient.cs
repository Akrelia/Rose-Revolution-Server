using RevolutionCore.Networking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace RoseLoginServer.Core
{
    /// <summary>
    /// Login client.
    /// </summary>
    public class LoginClient : RoseClient
    {
        State state;
        readonly DateTime lastConnection;

        /// <summary>
        /// Parameterless constructor.
        /// </summary>
        public LoginClient() : base()
        {

        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="tcpClient">Tcp client.</param>
        public LoginClient(TcpClient tcpClient) : base(tcpClient)
        {
            this.tcpClient = tcpClient;

            lastConnection = DateTime.Now;
        }

        /// <summary>
        /// Get or set the state of the client.
        /// </summary>
        public State State
        {
            get { return state; }
            set { state = value; }
        }

        /// <summary>
        /// Get ro set the last connection.
        /// </summary>
        public DateTime LastConnection
        {
            get { return lastConnection; }
        }
    }

    /// <summary>
    /// Every states.
    /// </summary>
    public enum State
    {
        /// <summary>
        /// Default.
        /// </summary>
        Default,
        /// <summary>
        /// Logged in state.
        /// </summary>
        LoggedIn,
        /// <summary>
        /// Transfering state.
        /// </summary>
        Transfering
    }
}
