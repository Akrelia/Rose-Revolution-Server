using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevolutionShared.Rose.Data.Clan
{
    /// <summary>
    /// Clan Grade Data..
    /// </summary>
    [Serializable]
    public class ClanGradeData
    {
        public int cost;
        public int clanPointsRequired;
        public int maxClanMemberCount;
        public NameColor nameColor;
    }
}
