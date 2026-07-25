using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevolutionShared.Rose.Data.Equipment
{
    [Serializable]
    public class EquipmentData : RoseData
    {
        public string description;
        public short iconID;
        public ItemType type;
        public int price;
        public short weight;
        public byte quality;
        public short icon;
        public short sfxID; // Sound when equipped ?
        public short craftID;
        public short fieldItemID; // This will be replaced, just spawn the item, except for armor, etc ...
        public short skillLevel; // Unused I guess
        public short productID;
        public Job1Type firstJobReq;
        public Job2Type secondJobReq;
        public short levelReq;
        public StatType statReq;
        public short statAmountReq;
        public short ability1Type; // This is the additional stat, like [MP Recovery 2] (Turn tris into a DB and bound the stat to it ?)
        public short ability1Value;
        public short ability2Type;
        public short ability2Value;
        public short durability;
        public byte prefixID; // Legend, Grand, Dark, etc ...  (Turn tris into a DB)
        public GenderType genderType;
    }
}
