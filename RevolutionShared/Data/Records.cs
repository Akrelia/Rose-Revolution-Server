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

    public record CharacterAppearance(GenderType Gender, byte Hair, byte Face, int Back, int Body, int Gloves, int Shoes, int Mask, int Hat, int Weapon, int SubWeapon) : SerializableRecord<CharacterAppearance>;

    //  public record EntityInfos(int id, EntityType type, int dataID, WorldPosition position) : SerializableRecord<EntityInfos>;
    //  public record EnemyInfos(int id, EntityType type, int dataID, WorldPosition position, int health) : EntityInfos(id, type, dataID, position);
    // public record EnemyInfos(int id, EntityType type, int dataID, WorldPosition position, int health) : SerializableRecord<EnemyInfos>;
    public record EntityInfos(int id, EntityType type, int dataID, WorldPosition position) : SerializableRecord;
    [Union(0, typeof(EnemyInfos))]
    [Union(1, typeof(NPCInfos))]
    public abstract record EntitySubInfos() : SerializableRecord;
    [EntitySubInfos(EntityType.Enemy)]
    public record EnemyInfos(int health) : EntitySubInfos;
    [EntitySubInfos(EntityType.NPC)]
    public record NPCInfos(int dialogID) : EntitySubInfos;
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