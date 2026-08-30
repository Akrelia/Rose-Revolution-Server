using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevolutionShared.Rose.Data.Items
{
    /// <summary>
    /// Material type.
    /// </summary>
    [Serializable]
    public class MaterialData : ItemData
    {
        public int materialType;
        public int bulletType;
    }
}
