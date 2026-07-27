using RevolutionShared.Rose.Data;
using RevolutionShared.Rose.Data.NPC.Drops;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevolutionCore.Services
{
    /// <summary>
    /// System handling everything related to randomness.
    /// </summary>
    public class RandomSystem
    {
        private static readonly Random random = new Random();

        /// <summary>
        /// Constructor.
        /// </summary>
        public RandomSystem()
        {

        }

        /// <summary>
        /// Pick up a drop from a table.
        /// </summary>
        /// <param name="table">Table.</param>
        /// <returns>Drop picked.</returns>
        public DropData PickUp(DropTableData table)
        {
            if (table.drops == null || table.drops.Count == 0)
            {
                return null;
            }

            if (random.NextDouble() * 100 >= table.dropSuccess)
            {
                return null;
            }

            double roll = random.NextDouble() * table.TotalChance;
            double current = 0;

            foreach (var drop in table.drops)
            {
                current += drop.dropChance;

                if (roll < current)
                {
                    return drop;
                }
            }

            return null;
        }
    }
}
