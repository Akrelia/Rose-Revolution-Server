using MessagePack;
using RevolutionShared.Networking.Packets;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace RevolutionCore.Utils
{
    /// <summary>
    /// Extension for the object class.
    /// </summary>
    public static class SerializerDeserializerExtensions
    {
        private static readonly Random random = new Random();

        /// <summary>
        /// Serialize an object.
        /// </summary>
        /// <param name="obj">Object.</param>
        /// <returns>Array of bytes</returns>
        public static byte[] Serialize(this object obj)
        {
            byte[] bytes;

            using (var stream = new MemoryStream())
            {
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, obj);
                bytes = stream.ToArray();
            }

            return bytes;
        }

        /// <summary>
        /// Deserialize an array of bytes.
        /// </summary>
        /// <typeparam name="T">Type of the object.</typeparam>
        /// <param name="bytes">Bytes.</param>
        /// <returns>Object of T type.</returns>
        public static T Deserialize<T>(this byte[] bytes)
        {
            T ReturnValue;

            using (var stream = new MemoryStream(bytes))
            {
                IFormatter foramtter = new BinaryFormatter();
                ReturnValue = (T)foramtter.Deserialize(stream);
            }

            return ReturnValue;
        }

        /// <summary>
        /// Get a random item in the list.
        /// </summary>
        /// <typeparam name="T">Type of the list.</typeparam>
        /// <param name="list">Current list.</param>
        /// <returns>Random elemeent.</returns>
        public static T PickUp<T>(this IList<T> list)
        {
            if (list == null || list.Count == 0)
            {
                throw new InvalidOperationException("Listis null or empty.");
            }

            int index = random.Next(list.Count);

            return list[index];
        }

        /// <summary>
        /// Serialize an object into a packet.
        /// </summary>
        /// <typeparam name="T">T.</typeparam>
        /// <param name="packet">Packet.</param>
        /// <param name="obj">Obj.</param>
        public static void SerializeRecord<T>(this PacketOut packet, T obj) where T : IPacketCreatable<T>
        {
            byte[] data = MessagePackSerializer.Serialize(obj, MessagePack.Resolvers.ContractlessStandardResolver.Options);

            packet.Add(data.Length);

            packet.Add(data);
        }

        /// <summary>
        /// Deserialize an object from a packet.
        /// </summary>
        /// <typeparam name="T">Type of object.</typeparam>
        /// <param name="packet">Packet.</param>
        /// <returns>Deserialized object.</returns>
        public static T DeserializeRecord<T>(this PacketIn packet) where T : IPacketCreatable<T>
        {
            var data = packet.GetBytes();

            return MessagePackSerializer.Deserialize<T>(data, MessagePack.Resolvers.ContractlessStandardResolver.Options);
        }

        /// <summary>
        /// Serialize a collection of objects into a packet.
        /// </summary>
        public static void SerializeRecords<T>(this PacketOut packet, IEnumerable<T> collection) where T : IPacketCreatable<T>
        {
            var list = collection is List<T> l ? l : new List<T>(collection);

            packet.Add(list.Count);

            foreach (var item in list)
            {
                packet.SerializeRecord(item);
            }
        }

        /// <summary>
        /// Deserialize a collection of objects from a packet.
        /// </summary>
        public static List<T> DeserializeRecords<T>(this PacketIn packet) where T : IPacketCreatable<T>
        {
            int count = packet.GetInt();

            var list = new List<T>(count);

            for (int i = 0; i < count; i++)
            {
                list.Add(packet.DeserializeRecord<T>());
            }

            return list;
        }

        /// <summary>
        /// Get a creatable from the buffer.
        /// </summary>
        /// <typeparam name="T">Type.</typeparam>
        /// <returns>Creatable.</returns>
        public static T GetCreatable<T>(this PacketIn packet) where T : IPacketCreatable<T>
        {
            return packet.DeserializeRecord<T>();
        }

        /// <summary>
        /// Get all creatables from the buffer.
        /// </summary>
        /// <typeparam name="T">T.</typeparam>
        /// <returns>Creatables.</returns>
        public static IEnumerable<T> GetCreatables<T>(this PacketIn packet) where T : IPacketCreatable<T>
        {
            int count = packet.GetInt();

            var list = new List<T>(count);

            for (int i = 0; i < count; i++)
            {
                list.Add(packet.DeserializeRecord<T>());
            }

            return list;
        }
    }

    /// <summary>
    /// Extension for the packet class.
    /// </summary>
    public static class PacketExtensions
    {
        /// <summary>
        /// Add a creatable to the packet.
        /// </summary>
        /// <typeparam name="T">T.</typeparam>
        /// <param name="packet">Packet.</param>
        /// <param name="creatable">Creatable to add.</param>
        public static void Add<T>(this PacketOut packet, T creatable) where T : IPacketCreatable<T>
        {
            packet.SerializeRecord(creatable);
        }

        /// <summary>
        /// Get a creatable from the packet.
        /// </summary>
        /// <typeparam name="T">Type.</typeparam>
        /// <param name="packet">Packet.</param>
        /// <param name="creatable">Creatable.</param>
        public static T Get<T>(this PacketIn packet) where T : IPacketCreatable<T>
        {
            return packet.DeserializeRecord<T>();
        }
    }
}
