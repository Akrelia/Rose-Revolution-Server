using RevolutionShared.Networking.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevolutionShared.Utils
{
    /// <summary>
    /// Tools.
    /// </summary>
    public static partial class Tools
    {
        /// <summary>
        /// Our random.
        /// </summary>
        public static Random Random = new Random();

        /// <summary>
        /// Display a packet in a formated string.
        /// </summary>
        /// <param name="packet">Packet.</param>
        /// <returns>String format.</returns>
        public static string GetPacketString(IPacket packet)
        {
            string str = "";

            for (int i = 0; i < packet.Buffer.Length; i++)
            {
                str += packet.Buffer[i].ToString("X2") + " ";
            }

            return str;
        }

        /// <summary>
        /// Convert two bytes in to a short.
        /// </summary>
        /// <param name="A">First byte.</param>
        /// <param name="B">Second byte.</param>
        /// <returns></returns>
        public static short Convert(byte A, byte B)
        {
            return (short)((B << 8) + A);
        }

        /// <summary>
        /// Convert two bytes in to a short.
        /// </summary>
        /// <param name="A">First byte.</param>
        /// <param name="B">Second byte.</param>
        /// <param name="C">Third byte.</param>
        /// <param name="D">Fourth byte.</param>
        /// <returns></returns>
        public static int Convert(byte A, byte B, byte C, byte D)
        {
            return (D << 24) + (C << 16) + (B << 8) + A;
        }

        /// <summary>
        /// Check the possible duplicates of a list of props.
        /// </summary>
        /// <returns></returns>
        public static bool CheckClassDuplicates<T>()
        {
            var props = typeof(T).GetFields();

            var anyDuplicate = props.GroupBy(x => x.GetValue(null)).Any(g => g.Count() > 1);

            return anyDuplicate;
        }

        /// <summary>
        /// Map a class.
        /// </summary>
        /// <typeparam name="T">T.</typeparam>
        /// <typeparam name="U">U</typeparam>
        /// <returns>Dictionnary.</returns>
        public static Dictionary<T, string> MapClass<T, U>()
        {
            Dictionary<T, string> mapping = new Dictionary<T, string>();

            var props = typeof(U).GetFields();

            for (int i = 0; i < props.Length; i++)
            {
                var prop = props[i];

                var value = (T)prop.GetValue(null);
                var name = prop.Name;

                mapping.Add(value, name);
            }

            return mapping;
        }

        /// <summary>
        /// Get the Rose time.
        /// </summary>
        /// <returns>Time.</returns>
        public static int GetRoseTime()
        {
            var now = DateTime.Now;

            int time = 0;

            time += now.Second;
            time += now.Minute * 60;
            time += now.Hour * 60 * 60;
            time += now.DayOfYear * 60 * 60 * 24;
            time += (now.Year - 2000) * 60 * 60 * 24 * 366;

            return time;
        }
    }
}
