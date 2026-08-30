using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevolutionShared.Rose.Data.Items
{
    /// <summary>
    /// Consumable data.
    /// </summary>
    [Serializable]
    public class ConsumableData : ItemData
    {
        public int effectType;
        public int effectAmount;
        public int useEffectID;
    }
}
