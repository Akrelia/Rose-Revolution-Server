using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevolutionShared.Rose.Data.NPC
{
    [Serializable]
    public class EntityData
    {
        public int id;
        public string displayName;
        public int moveSpeed;
        public int runSpeed; // Does this is used in the OG ?
        public int size;
        public int rightWeaponID;
        public int leftWeaponID;
        public int faceIconID;
        public int characterType; // TODO : Turn this into a enum
        public int localizationID; // used to be a key string, but we will do our own localization system
        public int glowColor;
    }
}
