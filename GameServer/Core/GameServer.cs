using RevolutionCore.Configurations;
using RevolutionCore.Networking;
using RevolutionCore.Utils;
using RoseGameServer.Core.Handling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RoseGameServer.Core
{
    /// <summary>
    /// Game server.
    /// </summary>
    public class GameServer : RoseServer<GameClient, GamePacketHandler>
    {
        IscServer loginServer;
        List<Channel> channels;

        /// <summary>
        /// Constructor.
        /// </summary>
        public GameServer() : base(Configuration.GameServerAddress, Configuration.GameServerPort, Configuration.GameServerAddress, Configuration.GameServerPortIsc)
        {
            channels = new List<Channel>();
            packetHandler = new GamePacketHandler(this, database);
            channels.Add(new Channel(1, 0, 0, 0, Configuration.ChannelName));
        }

        /// <summary>
        /// Start the server.
        /// </summary>
        public override void Start()
        {
            base.Start();

            try
            {
                _ = ConnectToLoginServer();
            }

            catch (Exception ex)
            {
                Logger.LogError($"Please start the Login Server before this one, thanks. Error : {ex.Message}");

                return;
            }

            Logger.LogImportantMessage("CONNECTED", "Connected to the Login Server");

        }

        /// <summary>
        /// Connect to the login server and send some packets.
        /// </summary>
        public async Task ConnectToLoginServer()
        {
            var tcpClient = new TcpClient(Configuration.LoginServerAddress, Configuration.LoginServerPortIsc);

            loginServer = new IscServer(0, tcpClient);

            // await packetHandler.SendIscPacket(packetHandler.SendServerPacket(), loginServer);

            Thread.Sleep(100);

            // await packetHandler.SendIscPacket(packetHandler.SendChannelsPacket(), loginServer);

            servers.Add(loginServer);
        }

        /// <summary>
        /// Get client by its id.
        /// </summary>
        /// <param name="id">Id.</param>
        /// <returns>Client if found.</returns>
        public GameClient GetClient(int id)
        {
            for (int i = 0; i < clients.Count; i++)
            {
                if (clients[i].Id == id)
                {
                    return clients[i];
                }
            }

            return null;
        }

        /// <summary>
        /// Get client by its account name.
        /// </summary>
        /// <param name="accountName">Account name.</param>
        /// <returns>Client if found.</returns>
        public GameClient GetClient(string accountName)
        {
            for (int i = 0; i < clients.Count; i++)
            {
                if (clients[i].AccountName == accountName)
                {
                    return clients[i];
                }
            }

            return null;
        }

        /// <summary>
        /// Disconnect a client.
        /// </summary>
        /// <param name="client">Client.</param>
        public override void Disconnect(GameClient client)
        {
            if ((client.CharId != 0))
            {
              //  _ = packetHandler.SendIscPacket(packetHandler.KickAccountPacket(client.AccountName), loginServer);
            }

            base.Disconnect(client);
        }

        /// <summary>
        /// Kick a client.
        /// </summary>
        /// <param name="client">Client to kick.</param>
        public void KickClient(GameClient client)
        {
            client.TcpClient.Close();
            client.TcpClient.Dispose();

            clients.Remove(client);
        }

        /// <summary>
        /// Get the login server.
        /// </summary>
        public IscServer LoginServer
        {
            get { return loginServer; }
        }

        /// <summary>
        /// Get the list of channels of the server.
        /// </summary>
        public List<Channel> Channels
        {
            get { return channels; }
        }
    }
}
