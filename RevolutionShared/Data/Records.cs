using MessagePack;
using RevolutionShared.Networking.Packets;
using RevolutionShared.Rose.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevolutionShared.Data
{
#nullable enable
    [Serializable]
    public record SerializableRecord<T> : IPacketCreatable<T> // TODO : remove this, as it is unusable in C#9
    {

    }

    [Serializable]
    public record SerializableRecord
    {

    }

    public record CharacterAppearance(
        GenderType Gender,
        byte Hair,
        byte Face,
        int Back,
        int Body,
        int Gloves,
        int Shoes,
        int Mask,
        int Hat,
        int Weapon,
        int SubWeapon
    ) : SerializableRecord;

    public record EntityInfos(int id, EntityType type, WorldPosition position) : SerializableRecord;

    [Union(0, typeof(ServerEntityInfos))]
    public abstract record EntitySubInfos() : SerializableRecord;

    public abstract record ServerEntityInfos(int dataID) : EntitySubInfos;

    [EntitySubInfos(EntityType.Enemy)]
    public record EnemyInfos(int dataID, int health) : ServerEntityInfos(dataID);

    [EntitySubInfos(EntityType.NPC)]
    public record NPCInfos(int dataID, int dialogID) : ServerEntityInfos(dataID);

    [EntitySubInfos(EntityType.Character)]
    public record CharacterInfos(string name, string clanName, byte[] clanIcon, byte clanGrade) : EntitySubInfos;
}

namespace System.Runtime.CompilerServices
{
    public class IsExternalInit { } // Hack to use some C# 9 features in .NET Framework 4.8 (which is keyword Record here)
}


[AttributeUsage(AttributeTargets.Class | AttributeTargets.Class)]
public class EntitySubInfosAttribute : Attribute
{
    public EntityType Type { get; }
    public EntitySubInfosAttribute(EntityType type) => Type = type;
}