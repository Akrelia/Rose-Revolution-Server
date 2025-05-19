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
        /// Database instance.
        /// </summary>
        protected Database database;
        /// <summary>
        /// Tcp listener for clients.
        /// </summary>
        protected TcpListener listener;
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
        /// User tasks.
        /// </summary>
        protected Dictionary<Guid, Task> userTasks = new Dictionary<Guid, Task>();

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
           // database = new Database(Configuration.DatabaseDbIp, Configuration.DatabasePort, Configuration.DatabaseName, Configuration.DatabaseUser, Configuration.DatabasePassword);
            listener = new TcpListener(IPAddress.Parse(address), port);
            tokenSource = new CancellationTokenSource();
        }

        /// <summary>
        /// Start.
        /// </summary>
        public async Task StartAsync()
        {
            listener.Start();

            Logger.LogImportantMessage("Sandbox Server starting ...");

            _ = ListenAsync(tokenSource.Token);

            await Task.WhenAll(ListenAsync(tokenSource.Token));
        }

        /// <summary>
        /// Start the server.
        /// </summary>
        public virtual void Start()
        {
            tokenSource = CancellationTokenSource.CreateLinkedTokenSource(new CancellationToken());
            token = tokenSource.Token;
            listener.Start();
          //  database.Open();
        }

        /// <summary>
        /// Listen to incoming new tcp client.
        /// </summary>
        /// <returns>Task.</returns>
        public async Task ListenAsync(CancellationToken cancelToken)
        {
            try
            {
                while (!cancelToken.IsCancellationRequested)
                {
                    var tcpClient = await listener.AcceptTcpClientAsync().ConfigureAwait(false);

                    T client = new T() { TcpClient = tcpClient }; // C# can't have generic constructor with parameters

                    Logger.LogImportantMessage("CONNECTION", $"Client ({client.GUID}) connected from {tcpClient.Client.RemoteEndPoint}");

                    clients.Add(client);

                    userTasks[client.GUID] = UpdateUserAsync(client);

                    await Task.Delay(10, cancelToken);
                }
            }

            catch (Exception ex)
            {
                Logger.LogFatalError($"Server crashed : {ex.Message}{Environment.NewLine}{ex.InnerException.StackTrace}");
            }

            finally
            {
                listener.Stop();
            }
        }

        /// <summary>
        /// Update the user.
        /// </summary>
        /// <param name="client">Client to update.</param>
        /// <returns>Task.</returns>
        public async Task UpdateUserAsync(T client)
        {
            try
            {
                while (client.TcpClient.Connected)
                {
                    var packet = await packetHandler.GetPacketAsync(client.TcpClient, tokenSource.Token);

                    if (packet != null)
                    {
                        await packetHandler.HandlePacket(packet, client);

                        client.PacketCount++;
                        client.RefreshActivity();

                        if (client.PacketCount >= Configuration.PacketsPerSecond)
                        {
                            DisconnectSpammer(client);
                        }

                        else
                        {
                            if (client.LastSpamCheck + Configuration.CheckPacketRate <= DateTime.Now)
                            {
                                client.ResetPacketLimitation();
                            }
                        }
                    }

                    else
                    {
                        if (client.LastActivity + Configuration.PingDuration <= DateTime.Now)
                        {
                            if (!client.Pinged)
                            {
                                await packetHandler.PingClient(client);
                            }
                        }
                    }

                    await Task.Delay(20);
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Error while updating user {client.GUID}: {ex.Message}");
            }

            finally
            {
                Disconnect(client);

                await Task.CompletedTask;
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
        /// A spammer is detected.
        /// </summary>
        /// <param name="client">User.</param>
        public virtual void DisconnectSpammer(T client)
        {
            Logger.LogWarning($"User {client} reached the packet limit and will be disconnected");

            Disconnect(client);
        }

        /// <summary>
        /// Stop the server.
        /// </summary>
        public void Stop()
        {
            tokenSource?.Cancel();
            listener.Stop();
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
