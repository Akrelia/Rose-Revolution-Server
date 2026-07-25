using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevolutionShared.Rose.Data.Equipment
{
    [Serializable]
    public class ArmorData : EquipmentData
    {
        public short defense;
        public short magicDefense;
    }
}
