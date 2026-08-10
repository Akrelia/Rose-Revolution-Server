using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevolutionShared.Rose.Data.Equipment
{
    [Serializable]
    public class WeaponData : EquipmentData
    {
        public byte motionID;
        public short range;
        public short attackPower;
        public short attackSpeed;
        public AttackType attackType;
        public WeaponType weaponType;
        public short attackEffectID;
        public short attackingSoundEffectID;
        public short firingSoundEffectID;
    }
}
