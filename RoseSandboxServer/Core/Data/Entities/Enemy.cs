using RevolutionCore.Utils;
using RevolutionShared.Data;
using RevolutionShared.Networking.Packets;
using RevolutionShared.Rose.Data;
using RevolutionShared.Rose.Data.NPC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoseSandboxServer.Core.Data.Entities
{
    /// <summary>
    /// Enemy entity.
    /// </summary>
    public class Enemy : Entity
    {
        int currentHealth;
        readonly EnemyData data;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="id">ID.</param>
        /// <param name="dataId">Data ID.</param>
        public Enemy(int id, EnemyData data) : base(id, EntityType.Enemy, data.ID)
        {
            this.data = data;
            this.currentHealth = data.healthPoints;
        }

        /// <summary>
        /// To sub infos.
        /// </summary>
        /// <returns>DTO.</returns>
        public override EntitySubInfos ToSubInfos()
        {
            return new EnemyInfos(currentHealth);
        }

        /// <summary>
        /// Get the current health.
        /// </summary>
        public int CurrentHealth
        {
            get { return currentHealth; }
        }

        /// <summary>
        /// Get the enemy data.
        /// </summary>
        public EnemyData Data
        {
            get { return data; }
        }
    }
}
