using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
    }

    public static class RandomExtensions
    {

        /// <summary>
        /// Get a random bool.
        /// </summary>
        /// <param name="random">Random.</param>
        /// <returns></returns>
        public static bool GetBool(this Random random)
        {
            return random.Next(0, 2) == 0;
        }
    }
}
