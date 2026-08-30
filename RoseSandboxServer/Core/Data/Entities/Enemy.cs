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
    public class Enemy : Entity, IDataEntity<EnemyData>
    {
        int currentHealth;
        readonly EnemyData data;

        // Dummies for now since we don't use the AIP file atm.
        public float moveChance = 0.07F;
        public float checkRate = 2;
        public double lastCheckTime;
        public double lastActionTime;
        public float distanceRecoverPosition = 4;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="id">ID.</param>
        /// <param name="dataId">Data ID.</param>
        public Enemy(int id, WorldPosition position, EnemyData data, Map map) : base(id, position, EntityType.Enemy, map)
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

            if (lastCheckTime + checkRate <= context.Time) // Behavior should be updated after the base update, so that the entity can react to changes in the world (taking damage, death, etc ...)
            {
                if (Data.moveSpeed != 0) // Obviously, if the enemy can't move, don't move it.
                {
                    if (RandomSystem.random.NextDouble() < moveChance)
                    {
                        Move(context);

                        lastActionTime = context.Time;
                    }
                }

                lastCheckTime = context.Time;
            }
        }

        /// <summary>
        /// Move the enemy.
        /// </summary>
        /// <param name="context">Context.</param>
        public void Move(TickContext context)
        {
            if (WorldPosition.Distance(position, initialPosition) < distanceRecoverPosition)
            {
                var random = new Random();

                var amountX = random.Next(-3, 4);
                var amountZ = random.Next(-3, 4);

                amountX += Math.Sign(amountX) * 2;
                amountZ += Math.Sign(amountZ) * 2;

                position.x += amountX;
                position.z += amountZ;
            }
            else
            {
                position = initialPosition;
            }

            context.AddMapPacket(Packets.UpdateEntity(this), map);
        }

        public override void WriteToPacket(PacketOut packet)
        {
            base.WriteToPacket(packet);

            packet.AddNew(zToSubInfos());
        }

        /// <summary>
        /// Do damage.
        /// </summary>
        /// <param name="amount">Amount.</param>
        public void DoDamage(int amount)
        {
            currentHealth -= amount;

            if (currentHealth < 0)
            {
                // TODO : UpdateContext ? Die ?
            }
        }

        /// <summary>
        /// To sub infos.
        /// </summary>
        /// <returns>DTO.</returns>
        public override ServerEntityInfos ToSubInfos()
        {
            return new EnemyInfos(data.id, currentHealth);
        }

        public EnemyInfos zToSubInfos()
        {
            return new EnemyInfos(data.id, currentHealth);
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
