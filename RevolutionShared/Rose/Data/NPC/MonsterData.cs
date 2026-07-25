using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevolutionShared.Rose.Data.NPC
{
    [Serializable]
    public class MonsterData : EntityData
    {
        public int level;
        public int healthPoints;
        public int attack;
        public int accuracy;
        public int defense;
        public int magicDefense;
        public int flee;
        public int attackSpeed;
        public AttackType attackType;
        public int AI;
        public int experience;
        public int dropTableID;
        public int moneyDrop; // I don't think this is used at all too
        public int drop; // Again, not sure this is used
        public int attackRange;
        public int attackEffectID;
        public int generalSoundEffectID;
        public int attackedSoundEffectID;
        public int dyingSoundID;
        public bool isPartyQuestMonster; // Rework this when Quest system is implemented
        public string eventTriggerDeath;
    }
}
