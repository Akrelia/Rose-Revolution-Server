using RevolutionShared.Data;
using RevolutionShared.Rose.Data;
using RoseSandboxServer.Core.Data.Entities;
using RoseSandboxServer.Core.World;
using RoseSandboxServer.Networking.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoseSandboxServer.Core.Data
{
    /// <summary>
    /// Player.
    /// </summary>
    public class Player : Entity
    {
        CharacterAppearance appearance;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type"></param>
        /// <param name="dataID"></param>
        public Player(int id, EntityType type, int dataID, Map map) : base(id, type, dataID, map)
        {
        }

        /// <summary>
        /// Update the player.
        /// </summary>
        /// <param name="context">Context.</param>
        public override async Task Update(TickContext context)
        {
            await base.Update(context);

            // Anything related to player only
        }

        public override EntitySubInfos ToSubInfos()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get or set the player appearance.
        /// </summary>
        public CharacterAppearance Appearance
        {
            get { return appearance; }
            set { appearance = value; }
        }
    }
}
