using RevolutionCore.Services;
using RevolutionCore.Utils;
using RevolutionShared.Data;
using RevolutionShared.Networking.Packets;
using RevolutionShared.Rose.Data;
using RevolutionShared.Rose.Data.NPC;
using RoseSandboxServer.Core.World;
using RoseSandboxServer.Networking.Contexts;
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
        /// Dummies for now since we don't use the AIP file atm.
        /// </summary>
        public float moveChance = 0.1F;
        public double lastActionTime;
        public double actionCooldown = 2;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="id">ID.</param>
        /// <param name="dataId">Data ID.</param>
        public Enemy(int id, EnemyData data, Map map) : base(id, EntityType.Enemy, data.ID, map)
        {
            this.data = data;
            this.currentHealth = data.healthPoints;
        }

        /// <summary>
        /// Update the enemy.
        /// </summary>
        /// <param name="context">Context.</param>
        /// <returns>Task.</returns>
        public override async Task Update(TickContext context)
        {
            await base.Update(context);

            if (lastActionTime + actionCooldown <= context.Time) // Behavior should be updated after the base update, so that the entity can react to changes in the world (taking damage, death, etc ...)
            {
                Move(context);
            }
        }

        /// <summary>
        /// Move the enemy.
        /// </summary>
        /// <param name="context">Context.</param>
        public void Move(TickContext context)
        {
            if (RandomSystem.random.NextDouble() < moveChance)
            {
                var amount = new Random().Next(-5, 5);

                amount += Math.Sign(amount) * 2;

                position.x += amount;
                position.z += amount;

                context.AddMapPacket(Packets.UpdateEntity(this), map);

                lastActionTime = context.Time;
            }
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
