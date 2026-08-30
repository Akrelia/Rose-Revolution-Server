using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevolutionShared.Rose.Data.Items.Equipment
{
    /// <summary>
    /// Equipment data.
    /// </summary>
    [Serializable]
    public class EquipmentData : ItemData
    {
        public short levelReq;
        public Job1Type firstJobReq;
        public Job2Type secondJobReq;
        public StatType statReq;
        public GenderType genderType;
        public short statAmountReq;
        public short ability1Type; // This is the additional stat, like [MP Recovery 2] (Turn tris into a DB and bound the stat to it ?)
        public short ability1Value;
        public short ability2Type;
        public short ability2Value;
        public short durability;
        public byte prefixID; // Legend, Grand, Dark, etc ...  (Turn tris into a DB)
    }
}
