using RevolutionCore.Configurations;
using RevolutionCore.SQL;
using RevolutionCore.Utils;
using RevolutionShared.Attributes;
using RevolutionShared.Networking.Packets;
using RevolutionShared.Packets;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RevolutionCore.Networking
{
    /// <summary>
    /// A packet handler.
    /// </summary>
    public abstract partial class PacketHandler<T> where T : RoseClient 
    {
        protected ServerConfiguration configuration;

        protected Dictionary<int, Func<T, PacketIn, Task>> actions;

        /// <summary>
        /// Database instance.
        /// </summary>
        protected Database database;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="database">Database instance.</param>
        public PacketHandler(ServerConfiguration configuration, Database database)
        {
            this.configuration = configuration;
            this.database = database;
            this.actions = new Dictionary<int, Func<T, PacketIn, Task>>();

            LoadAsyncActions();

            Logger.LogDebug("Actions : " + actions.Count);
        }

        /// <summary>
        /// Get packet from a context.
        /// </summary>
        /// <param name="context">Context.</param>
        /// <returns>Packet.</returns>
        public async Task<PacketIn> GetPacketAsync(T client, CancellationToken token)
        {
            var header = new byte[6];
            var stream = client.TcpClient.GetStream();

            if (stream.DataAvailable)
            {
                int bytesRead = await stream.ReadAsync(header, 0, PacketIn.HeaderLength, token);

                if (bytesRead != 0)
                {
                    var size = BitConverter.ToInt32(header, 0);

                    if (size <= configuration.MaximumPacketSize)
                    {
                        byte[] buffer = new byte[size + PacketIn.HeaderLength];

                        buffer[0] = header[0];
                        buffer[1] = header[1];
                        buffer[2] = header[2];
                        buffer[3] = header[3];
                        buffer[4] = header[4];
                        buffer[5] = header[5];

                        if (size != 0)
                        {
                            await stream.ReadAsync(buffer, PacketIn.HeaderLength, size, token);
                        }

                        PacketIn packet = new PacketIn(buffer);

                        Logger.LogImportantMessage("IN" ,$"{client.ToString()}> [{((ClientCommands)packet.Command).ToString()}]");

                        return packet;
                    }

                    else
                    {
                        Logger.LogWarning($"{client.ToString()} ({client.IP}) is trying to send a large packet !");

                        return null;
                    }
                }

                else
                {
                    Logger.LogWarning($"{client.ToString()} ({client.IP}) is not connected anymore");
                }
            }

            return null;
        }

        /// <summary>
        /// Load async actions.
        /// </summary>
        public void LoadAsyncActions()
        {
            var type = this.GetType();

            foreach (MethodInfo method in type.GetMethods(BindingFlags.Public | BindingFlags.Instance))
            {
                PacketCommand attribute = method.GetCustomAttribute<PacketCommand>();

                if (attribute != null)
                {
                    if (method.ReturnType == typeof(Task))
                    {
                        ParameterInfo[] parameters = method.GetParameters();

                        if (parameters.Length == 2 && parameters[1].ParameterType == typeof(PacketIn) && parameters[0].ParameterType == typeof(T))
                        {
                            var instance = this;

                            Func<T, PacketIn, Task> action = async (T user, PacketIn packet) => await (Task)method.Invoke(instance, new object[] { user, packet });

                            if (!actions.ContainsKey(attribute.Value))
                            {
                                actions[attribute.Value] = action;
                            }

                            else
                            {
                                Console.WriteLine($"Warning: Duplicate key {attribute.Value}. Method {method.Name} was skipped.");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Method {method.Name} skipped: Incorrect parameter type or count.");
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Handle the packet.
        /// </summary>
        /// <param name="packet">Packet.</param>
        /// <param name="client">Client.</param>
        /// <returns>Task.</returns>
        public async Task HandlePacket(PacketIn packet, T client)
        {
            if (actions.ContainsKey(packet.Command))
            {
                await actions[packet.Command](client, packet);
            }

            else
            {
                Logger.LogWarning($"There is no packet action for the following command : {packet.Command} ({packet.CommandString})");
            }
        }
    }
}
