using RevolutionShared.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevolutionShared.Rose.Data.Items.Equipment
{
    [BodyPart(BodyPartType.FOOT)]
    [Serializable]
    public class FootwearData : ArmorData
    {
        public int moveSpeed;
    }
}
