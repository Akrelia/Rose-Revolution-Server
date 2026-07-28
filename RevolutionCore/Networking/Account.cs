using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevolutionCore.Networking
{
    /// <summary>
    /// Account.
    /// </summary>
    public class Account
    {
        /// <summary>
        /// Account name.
        /// </summary>
        public string username;
        /// <summary>
        /// Right.
        /// </summary>
        protected AccountRight right;
    
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="username">Username.</param>
        /// <param name="right">Right.</param>
        public Account(string username, AccountRight right)
        {
            this.username = username;
            this.right = right;
        }
    }

    /// <summary>
    /// Rights.
    /// </summary>
    public enum AccountRight : byte
    {
        Banned = 0,
        User = 1,
        Moderator = 2,
        GameMaster = 3,
        Developer = 4,
        Administrator = 5
    }
}
