using RevolutionCore.Networking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevolutionCore.Utils
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
        public static string DisplayPacket(Packet packet)
        {
            string str = "";

            for (int i = 0; i < packet.Size; i++)
            {
                str += packet.Buffer[i].ToString("X2") + " ";
            }

            return str;
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
