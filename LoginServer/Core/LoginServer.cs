using RevolutionCore.Configurations;
using RevolutionCore.Networking;
using RevolutionCore.Utils;
using RoseLoginServer.Core.Handling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoseLoginServer.Core
{
    /// <summary>
    /// Login server.
    /// </summary>
    public class LoginServer : RoseServer<LoginClient, LoginPacketHandler>
    {
        List<RoseAccount> accounts;

        /// <summary>
        /// Constructor.
        /// </summary>
        public LoginServer() : base(Configuration.LoginServerAddress, Configuration.LoginServerPort, Configuration.LoginServerAddress, Configuration.LoginServerPortIsc)
        {
            accounts = new List<RoseAccount>();
            packetHandler = new LoginPacketHandler(this, database);
        }

        /// <summary>
        /// Get a client by its id.
        /// </summary>
        /// <param name="id">Id.</param>
        /// <returns>Client.</returns>
        public LoginClient GetClient(int id)
        {
            return clients.FirstOrDefault(c => c.Id == id);
        }

        /// <summary>
        /// Disconnect a client.
        /// </summary>
        /// <param name="client">Client.</param>
        public override void Disconnect(LoginClient client)
        {
            if (client.State != State.Transfering)
            {
                //Kick(client.AccountName);
            }

            //base.Disconnect(client);
        }

        /// <summary>
        /// Kick an account.
        /// </summary>
        /// <param name="accountName">Account name.</param>
        public void Kick(string accountName)
        {
            var account = GetAccount(accountName);

            if (account != null)
            {
                accounts.Remove(account);
            }

            else
            {
                Logger.LogError($"Trying to kick an account that does not exist : {accountName}");
            }

        }

        /// <summary>
        /// Get an account by its name.
        /// </summary>
        /// <param name="accountName">Account name.</param>
        /// <returns>Account.</returns>
        public RoseAccount GetAccount(string accountName)
        {
            return accounts.FirstOrDefault(a => a.Name == accountName);
        }

        /// <summary>
        /// Get an account by its id.
        /// </summary>
        /// <param name="id">Id.</param>
        /// <returns>Account.</returns>
        public RoseAccount GetAccount(int id)
        {
            return accounts.FirstOrDefault(a => a.Id == id);
        }

        /// <summary>
        /// Get or set the logged accounts.
        /// </summary>
        public List<RoseAccount> Accounts
        {
            get { return accounts; }
            set { accounts = value; }
        }
    }
}
