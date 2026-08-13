using RevolutionCore.Networking;
using RevolutionCore.Services;
using RevolutionShared.Data;
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
    /// NPC entity.
    /// </summary>
    public class NPC : Entity
    {
        readonly NPCData data;

        public float moveChance = 0.01F;
        public double lastActionTime;
        public double actionCooldown = 3;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="id">ID.</param>
        /// <param name="data">Data.</param>
        /// <param name="map">Map.</param>
        public NPC(int id,NPCData data, Map map) : base(id, EntityType.NPC, data.ID, map)
        {
            this.data = data;
        }

        /// <summary>
        /// Update the enemy.
        /// </summary>
        /// <param name="context">Context.</param>
        /// <returns>Task.</returns>
        public override async Task Update(TickContext context)
        {
            await base.Update(context);

            if (lastActionTime + actionCooldown <= context.Time) 
            {
                ChangePrices(context);
            }
        }

        /// <summary>
        /// Change the prices of the NPC.
        /// </summary>
        /// <param name="context">Context.</param>
        public void ChangePrices(TickContext context)
        {
            if (RandomSystem.random.NextDouble() < moveChance)
            {
                var amount = new Random().Next(-3, 3);

                amount += Math.Sign(amount) * 2;

                position.x += amount;
                position.z += amount;

                context.AddMapPacket(Packets.UpdateEntity(this), map);

                lastActionTime = context.Time;
            }
        }

        /// <summary>
        /// NPC to sub infos.
        /// </summary>
        /// <returns>Sub infos.</returns>
        public override EntitySubInfos ToSubInfos()
        {
            return new NPCInfos(data.dialogID);
        }

        /// <summary>
        /// Get the NPC data.
        /// </summary>
        public NPCData Data
        {
            get { return data; }
        }
    }
}
