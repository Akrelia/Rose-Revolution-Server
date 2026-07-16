using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevolutionCore.Configurations
{
    /// <summary>
    /// Configuration for the database.
    /// </summary>
    static public partial class Configuration
    {
        /// <summary>
        /// Database's ip.
        /// </summary>
        static public string DatabaseIP = "192.168.1.57";

        /// <summary>
        /// Database's port.
        /// </summary>
        static public string DatabasePort = "5432";

        /// <summary>
        /// Database's name.
        /// </summary>
        static public string DatabaseName = "roserevolution";
        /// <summary>
        /// Database's SQL user.
        /// </summary>
        static public string DatabaseUser = "postgres";
        /// <summary>
        /// Database's password.
        /// </summary>
        static public string DatabasePassword = "password";
        /// <summary>
        /// Database's time out.
        /// </summary>
        static public int DatabaseTimeOut = 5;
    }
}