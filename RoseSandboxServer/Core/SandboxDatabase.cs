using LinqToDB;
using LinqToDB.Common;
using LinqToDB.Data;
using LinqToDB.Tools;
using Npgsql;
using RevolutionCore.Configurations;
using RoseSandboxServer.Data.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoseSandboxServer.Core
{
    /// <summary>
    /// Sandbox database.
    /// </summary>
    public class SandboxDatabase : DataConnection
    {
        private static string AdminConnectionString = $"Host={DatabaseConfiguration.DatabaseAddress};Username={DatabaseConfiguration.DatabaseUser};Password={DatabaseConfiguration.DatabasePassword};Database=postgres;Timeout={DatabaseConfiguration.DatabaseTimeOut}";
        private static string RoseOnlineConnectionString = $"Host={DatabaseConfiguration.DatabaseAddress};Username={DatabaseConfiguration.DatabaseUser};Password={DatabaseConfiguration.DatabasePassword};Database={DatabaseConfiguration.DatabaseName};Timeout={DatabaseConfiguration.DatabaseTimeOut}";

        /// <summary>
        /// Constructor.
        /// </summary>
        public SandboxDatabase() : base("PostgreSQL", RoseOnlineConnectionString)
        {
        } 

        /// <summary>
        /// Initialize.
        /// </summary>
        public void Initialize()
        {
            EnsureDatabaseExists();

            this.CreateTable<Account>(tableOptions : TableOptions.CheckExistence);
        }

        /// <summary>
        /// Ensure that the database exists.
        /// </summary>
        public void EnsureDatabaseExists()
        {
            var connectionStringBuilder = new NpgsqlConnectionStringBuilder(AdminConnectionString);
            var databaseName = DatabaseConfiguration.DatabaseName;

            connectionStringBuilder.Database = "postgres";

            NpgsqlConnection connection = null;
            NpgsqlCommand command = null;

            try
            {
                connection = new NpgsqlConnection(connectionStringBuilder.ConnectionString);
                connection.Open();

                command = connection.CreateCommand();
                command.CommandText = $"SELECT 1 FROM pg_database WHERE datname = '{databaseName}'";

                var exists = command.ExecuteScalar();

                if (exists == null)
                {
                    command.CommandText = $"CREATE DATABASE \"{databaseName}\"";
                    command.ExecuteNonQuery();
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Error while checking the database: {ex.Message}");
            }

            finally
            {
                if (command != null)
                    command.Dispose();

                if (connection != null)
                    connection.Dispose();
            }
        }
    }
}
