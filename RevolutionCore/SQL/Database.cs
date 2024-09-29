using RevolutionCore.Utils;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevolutionCore.SQL
{
    /// <summary>
    /// Everything related to the database.
    /// </summary>
    public partial class Database
    {
        readonly string connectionString;
        readonly SqlConnection connection;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="database">Database name.</param>
        /// <param name="user">User.</param>
        /// <param name="password">Password.</param>
        public Database(string database, string user, string password)
        {
            connectionString = $@"Data Source =.; Initial Catalog = {database}; User ID = {user}; Password = {password};";

            connection = new SqlConnection(connectionString);
        }

        /// <summary>
        /// Open the database connection.
        /// </summary>
        /// <returns>True if succesful.</returns>
        public bool Open()
        {
            try
            {
                connection.Open();

                Logger.LogImportantMessage("STARTING", $"Database connection started : [{connection.Database}]");

                return true;
            }

            catch (Exception ex)
            {
                Logger.LogFatalError($"Can't open database on SQL server : {ex.Message}");

                return false;
            }
        }
    }
}
