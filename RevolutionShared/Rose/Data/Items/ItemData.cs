using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevolutionShared.Rose.Data.Items
{
    /// <summary>
    /// Genera item data.
    /// </summary>
    [Serializable]
    public class ItemData : RoseData
    {
        public string description;
        public short iconID;
        public ItemType type;
        public int price;
        public short weight;
        public byte quality;
        public short icon;
        public short sfxID; // Sound when equipped ?
        public short craftID; // In STB, almost everything is craftable but in the future we should an interface for that
        public short fieldItemID; // This will be replaced, just spawn the item, except for armor, etc ...
        public short skillLevel; // Unused I guess
        public short productID;
    }
}
