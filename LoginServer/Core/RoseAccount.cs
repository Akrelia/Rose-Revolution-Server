using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoseLoginServer.Core
{
    /// <summary>
    /// Rose account.
    /// </summary>
    public class RoseAccount
    {
        int id;
        string name;
        string password;
        int right;
        int serverIndex;
        byte channelIndex;
        int loginTime;
        int transferTime;
        AccountState accountState;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="id">Id of the account.</param>
        /// <param name="name">Name of the account.</param>
        /// <param name="password">Password of the account.</param>
        /// <param name="right">Right of the account.</param>
        public RoseAccount(int id, string name, string password, int right)
        {
            this.id = id;
            this.name = name;
            this.password = password;
            this.right = right;
        }

        /// <summary>
        /// Get or set the id of the account.
        /// </summary>
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        /// <summary>
        /// Get or set the right.
        /// </summary>
        public int Right
        {
            get { return right; }
            set { right = value; }
        }

        /// <summary>
        /// Get or set the server index.
        /// </summary>
        public int ServerIndex
        {
            get { return serverIndex; }
            set { serverIndex = value; }
        }

        /// <summary>
        /// Get or set the channel index.
        /// </summary>
        public byte ChannelIndex
        {
            get { return channelIndex; }
            set { channelIndex = value; }
        }

        /// <summary>
        /// Get or set the name.
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// Get or set the password.
        /// </summary>
        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        /// <summary>
        /// Get or set the login time.
        /// </summary>
        public int LoginTime
        {
            get { return loginTime; }
            set { loginTime = value; }
        }

        /// <summary>
        /// Get or set the transfert time.
        /// </summary>
        public int TransferTime
        {
            get { return transferTime; }
            set { transferTime = value; }
        }

        /// <summary>
        /// Get or set the account state.
        /// </summary>
        public AccountState AccountState
        {
            get { return accountState; }
            set { accountState = value; }
        }
    }

    /// <summary>
    /// Every state of a rose account.
    /// </summary>
    public enum AccountState
    {
        /// <summary>
        /// Default.
        /// </summary>
        Default,
        /// <summary>
        /// Transfering state.
        /// </summary>
        Transfering,
        /// <summary>
        /// Confirmed state.
        /// </summary>
        Confirmed,
        /// <summary>
        /// Reset added.
        /// </summary>
        ResetAdded
    }
}
