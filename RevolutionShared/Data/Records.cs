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
    public record SerializableRecord<T> : IPacketCreatable<T>
    {

    }

    public record CharacterAppearance(GenderType Gender, byte Hair, byte Face, int Back, int Body, int Gloves, int Shoes, int Mask, int Hat, int Weapon, int SubWeapon) : SerializableRecord<CharacterAppearance>;
}

namespace System.Runtime.CompilerServices
{
    public class IsExternalInit { } // Hack to use some C# 9 features in .NET Framework 4.8 (which is keyword Record here)
}
