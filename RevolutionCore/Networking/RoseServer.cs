using RevolutionCore.Configurations;
using RevolutionCore.SQL;
using RevolutionCore.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RevolutionCore.Networking
{
    /// <summary>
    /// Rose server.
    /// </summary>
    /// <typeparam name="T">Type from Rose client.</typeparam>
    /// <typeparam name="P">Type from Packet Handler.</typeparam>
    public abstract class RoseServer<T, P> where T : RoseClient, new() where P : PacketHandler<T>
    {
        /// <summary>
        /// IsRunning flag.
        /// </summary>
        protected bool isRunning;
        /// <summary>
        /// Database instance.
        /// </summary>
        protected Database database;
        /// <summary>
        /// Tcp listener for clients.
        /// </summary>
        protected TcpListener listener;
        /// <summary>
        /// Tcp listener for servers.
        /// </summary>
        protected TcpListener listenerIsc;
        /// <summary>
        /// Token for task cancelling.
        /// </summary>
        protected CancellationToken token;
        /// <summary>
        /// Token source for task cancelling.
        /// </summary>
        protected CancellationTokenSource tokenSource;
        /// <summary>
        /// Packet handler.
        /// </summary>
        protected P packetHandler;
        /// <summary>
        /// List of clients.
        /// </summary>
        protected List<T> clients;
        /// <summary>
        /// List of servers.
        /// </summary>
        protected List<IscServer> servers;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="address">Server address.</param>
        /// <param name="port">Main port.</param>
        /// <param name="addressIsc">Server isc address.</param>
        /// <param name="portIsc">Isc port.</param>
        public RoseServer(string address, short port, string addressIsc, short portIsc)
        {
            clients = new List<T>();
            servers = new List<IscServer>();
            database = new Database(Configuration.DatabaseName, Configuration.DatabaseUser, Configuration.DatabasePassword);
            listener = new TcpListener(IPAddress.Parse(address), port);
            listenerIsc = new TcpListener(IPAddress.Parse(addressIsc), portIsc);
        }

        /// <summary>
        /// Start the server.
        /// </summary>
        public virtual void Start()
        {
            isRunning = true;
            tokenSource = CancellationTokenSource.CreateLinkedTokenSource(new CancellationToken());
            token = tokenSource.Token;
            listener.Start();
            listenerIsc.Start();
            database.Open();
        }

        /// <summary>
        /// Listen to the client
        /// </summary>
        public async Task ListenAsync()
        {
            try
            {
                await Task.Run(async () =>
                {
                    while (true)
                    {
                        var tcpClient = await listener.AcceptTcpClientAsync().ConfigureAwait(false);

                        Logger.LogImportantMessage("CONNECTION", $"Client connected from {tcpClient.Client.RemoteEndPoint}");

                        T client = new T() { TcpClient = tcpClient }; // C# can't have generic constructor with parameters

                        clients.Add(client);

                        await Task.Delay(Configuration.ServerRefreshRate);
                    }
                });
            }

            catch (Exception ex)
            {
                Logger.LogFatalError($"Server crashed : {ex.Message}{Environment.NewLine}{ex.InnerException.StackTrace}");
            }

            finally
            {
                listener.Stop();
                isRunning = false;
            }
        }

        /// <summary>
        /// Update the clients.
        /// </summary>
        public async Task UpdateAsync()
        {
            try
            {
                await Task.Run(async () =>
                {
                    while (true)
                    {
                        for (int i = 0; i < clients.Count; i++)
                        {
                            var packet = await clients[i].UpdateAsync<T>();

                            if (packet != null)
                            {
                                await packetHandler.Handle(packet, clients[i]);
                            }

                            else
                            {
                                if (clients[i].TcpClient.Client.Poll(50, SelectMode.SelectRead))
                                {
                                    clients[i].ConnectAttempts++;

                                    if (clients[i].ConnectAttempts >= 10)
                                    {
                                        Disconnect(clients[i]);
                                    }
                                }

                                else
                                {
                                    if (clients[i].ConnectAttempts > 0)
                                    {
                                        clients[i].ConnectAttempts--;
                                    }
                                }
                            }
                        }

                        await Task.Delay(Configuration.ServerRefreshRate);
                    }
                });
            }

            catch (Exception ex)
            {
                Logger.LogFatalError($"Server crashed : {ex.Message}");
            }

            finally
            {
                listener.Stop();
                isRunning = false;
            }
        }

        /// <summary>
        /// Listen to other servers.
        /// </summary>
        public async Task ListenIscAsync()
        {
            try
            {
                await Task.Run(async () =>
                {
                    while (true)
                    {
                        var tcpClient = await listenerIsc.AcceptTcpClientAsync().ConfigureAwait(false);

                        Logger.LogImportantMessage("CONNECTION", $"Server connected from {tcpClient.Client.RemoteEndPoint}");

                        IscServer server = new IscServer(servers.Count() + 1, tcpClient);

                        servers.Add(server);

                        await Task.Delay(Configuration.ServerRefreshRate);
                    }
                });
            }

            catch (Exception ex)
            {
                Logger.LogFatalError($"Server crashed : {ex.Message}");
            }

            finally
            {
                listenerIsc.Stop();
                isRunning = false;
            }
        }

        /// <summary>
        /// Update the servers.
        /// </summary>
        public async Task UpdateIscAsync()
        {
            try
            {
                await Task.Run(async () =>
                {
                    while (true)
                    {
                        for (int i = 0; i < servers.Count; i++)
                        {
                            var packet = await servers[i].UpdateAsync();

                            if (packet != null)
                            {
                                await packetHandler.HandleIsc(packet, servers[i]);
                            }
                        }

                        await Task.Delay(Configuration.ServerRefreshRate);
                    }
                });
            }

            catch (Exception ex)
            {
                Logger.LogFatalError($"Server crashed : {ex.Message}");
            }

            finally
            {
                listenerIsc.Stop();
                isRunning = false;
            }
        }

        /// <summary>
        /// Disconnect a client.
        /// </summary>
        /// <param name="client">Client to disconnect.</param>
        public virtual void Disconnect(T client)
        {
            Logger.LogMessage("DISCONNECT", $"{client.IP} no longer connected, disconnecting");

            clients.Remove(client);

            client.TcpClient.Close();
            client.TcpClient.Dispose();
        }

        /// <summary>
        /// Stop the server.
        /// </summary>
        public void Stop()
        {
            tokenSource?.Cancel();
            listener.Stop();

            isRunning = false;
        }

        /// <summary>
        /// Get if the server is running.
        /// </summary>
        public bool IsRunning
        {
            get { return isRunning; }
        }

        /// <summary>
        /// Get the database instance.
        /// </summary>
        public Database Database
        {
            get { return database; }
        }

        /// <summary>
        /// Get the clients of the server.
        /// </summary>
        public List<T> Clients
        {
            get { return clients; }
        }

        /// <summary>
        /// Get the servers.
        /// </summary>
        public List<IscServer> Servers
        {
            get { return servers; }
        }
    }
}
