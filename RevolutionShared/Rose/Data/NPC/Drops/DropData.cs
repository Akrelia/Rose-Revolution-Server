using System;

namespace RevolutionShared.Rose.Data.NPC.Drops
{
    /// <summary>
    /// Drop entry.
    /// </summary>
    [Serializable]
    public class DropData
    {
        public int ID;
        public float dropChance;
        public ItemType Type;
    }
}