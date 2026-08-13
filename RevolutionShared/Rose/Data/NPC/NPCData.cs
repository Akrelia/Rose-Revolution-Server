using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevolutionShared.Rose.Data.NPC
{
    [Serializable]
    public class NPCData : EntityData
    {
        public int dialogID;
        public int sellTableID1;
        public int sellTableID2;
        public int sellTableID3;
        public int sellTableID4;
    }
}
