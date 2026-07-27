using System;
using System.Collections.Generic;

namespace RevolutionShared.Rose.Data.NPC.Drops
{
    /// <summary>
    /// Drop table.
    /// </summary>
    [Serializable]
    public class DropTableData
    {
        public int dropSuccess;
        public float totalChance;
        public List<DropData> drops;

        /// <summary>
        /// Constructor.
        /// </summary>
        public DropTableData()
        {
            drops = new List<DropData>();
        }

        /// <summary>
        /// Total chance of all drops.
        /// </summary>
        public float TotalChance
        {
            get
            {
                totalChance = 0;

                foreach (var drop in drops)
                {
                    totalChance += drop.dropChance;
                }


                return totalChance;
            }
        }
    }
}