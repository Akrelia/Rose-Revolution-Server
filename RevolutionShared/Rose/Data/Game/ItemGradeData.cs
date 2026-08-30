using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevolutionShared.Rose.Data.Game
{
    /// <summary>
    /// Item grade data.
    /// </summary>
    [Serializable]
    public class ItemGradeData
    {
        public int grade;
        public int attackBonus;
        public int accuracyBonus;
        public int defenseBonus;
        public int magicDefenseBonus;
        public int dodgeRateBonus;
        public int glowColor;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="attackBonus"></param>
        /// <param name="accuracyBonus"></param>
        /// <param name="defenseBonus"></param>
        /// <param name="magicDefenseBonus"></param>
        /// <param name="dodgeRateBonus"></param>
        /// <param name="glowColor"></param>
        public ItemGradeData(int grade, int attackBonus, int accuracyBonus, int defenseBonus, int magicDefenseBonus, int dodgeRateBonus, int glowColor)
        {
            this.grade = grade;
            this.attackBonus = attackBonus;
            this.accuracyBonus = accuracyBonus;
            this.defenseBonus = defenseBonus;
            this.magicDefenseBonus = magicDefenseBonus;
            this.dodgeRateBonus = dodgeRateBonus;
            this.glowColor = glowColor;
        }
    }
}
