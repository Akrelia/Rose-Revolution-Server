using RevolutionShared.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevolutionShared.Rose.Data.Items.Equipment
{
    [BodyPart(BodyPartType.CAP)]
    [Serializable]
    public class HeadgearData : ArmorData
    {
        public byte hair;
    }
}
