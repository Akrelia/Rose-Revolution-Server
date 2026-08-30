using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevolutionShared.Rose.Data
{
    public enum Job1Type
    {
        VISITOR = 0,
        SOLDIER = 111,
        MUSE = 211,
        HAWKER = 311,
        DEALER = 411
    }

    public enum Job2Type
    {
        NONE,
        KNIGHT = 121,
        CHAMPION = 122,
        MAGE = 221,
        CLERIC = 222,
        SCOUT = 322,
        RAIDER = 321,
        BOURGEOIS = 421,
        ARTISAN = 422
    }

    public enum EntityType
    {
        Character = 1,
        Enemy = 2,
        Summon = 3,
        NPC = 4
    }

    public enum StatType // Value are the same as used as required stats in the STB
    {
        STR = 10,
        DEX = 11,
        INT = 12,
        CON = 13,
        CHA = 14,
        SEN = 15,
    }

    public enum WeaponType
    {
        EMPTY = 1,
        OHSWORD = 211,
        THAXE = 223,
        OHMACE = 212,
        OHTOOL,
        THSWORD = 221,
        THSPEAR = 222,
        DSW = 252,
        THBLUNT,
        CANNON = 233,
        BOW = 231,
        XBOX = 271,
        GUN = 232,
        STAFF = 241,
        WAND = 242,
        BOOK = 269,
        KATAR = 251,
        SHIELD = 261,
    };

    public enum ActionType
    {
        STANDING = 0,
        TIRED,
        WALK,
        RUN,
        SIT,
        SITTING,
        STANDUP,
        WARNING,
        ATTACK1,
        ATTACK2,
        ATTACK3,
        HIT,
        FALL,
        DIE,
        RAISE,
        JUMP1,
        JUMP2,
        PICKUP,
    };

    public enum RigType
    {
        FOOT = 0,
        CART,
        CASTLEGEAR,
        FLIGHT,
        CHARSELECT,
    }

    public enum GenderType : byte
    {
        NONE = 0, // This is only used for genderless items / stuff
        MALE = 1,
        FEMALE = 2,
    };

    public enum BodyPartType
    {
        ARMS = 1,
        FOOT = 2,
        BODY = 3,
        CAP = 4,
        FACE = 5,
        FACEITEM = 6,
        BACK = 7,
        HAIR = 8,
        WEAPON = 9,
        SUBWEAPON = 10,
    }

    public enum ItemType : byte
    {
        FACEITEM = 1,
        HAT = 2,
        BODY = 3,
        GLOVES = 4,
        BOOTS = 5,
        BACK = 6,
        JEWEL = 7,
        WEAPON = 8,
        SUBWEAPON = 9,
        USEITEM = 10,
        GEMS = 11,
        MATERIAL = 12,
        PAT = 14
    }

    public enum AttackType
    {
        Normal = 1,
        Magic = 2
    }

    public enum NameColor
    {
        Gray = 0,
        LightBlue = 1,
        Blue = 2,
        Green = 3,
        Yellow = 4,
        Orange = 5,
        Red = 6,
        Pink = 7,
        Violet = 8,
    }
}
