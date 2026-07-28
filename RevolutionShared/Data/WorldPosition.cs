using RevolutionShared.Networking.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevolutionShared.Data
{
    [Serializable]
    public struct WorldPosition : IPacketWritable, IPacketReadable
    {
        public float x;
        public float y;
        public float z;

        public static readonly WorldPosition Zero = new WorldPosition(0f, 0f, 0f);
        public static readonly WorldPosition One = new WorldPosition(1f, 1f, 1f);
        public static readonly WorldPosition Up = new WorldPosition(0f, 1f, 0f);
        public static readonly WorldPosition Down = new WorldPosition(0f, -1f, 0f);
        public static readonly WorldPosition Left = new WorldPosition(-1f, 0f, 0f);
        public static readonly WorldPosition Right = new WorldPosition(1f, 0f, 0f);
        public static readonly WorldPosition Forward = new WorldPosition(0f, 0f, 1f);
        public static readonly WorldPosition Back = new WorldPosition(0f, 0f, -1f);

        public WorldPosition(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public static WorldPosition operator +(WorldPosition a, WorldPosition b) => new WorldPosition(a.x + b.x, a.y + b.y, a.z + b.z);

        public static WorldPosition operator -(WorldPosition a, WorldPosition b) => new WorldPosition(a.x - b.x, a.y - b.y, a.z - b.z);

        public static WorldPosition operator -(WorldPosition a) => new WorldPosition(-a.x, -a.y, -a.z);

        public static WorldPosition operator *(WorldPosition a, float d) =>new WorldPosition(a.x * d, a.y * d, a.z * d);

        public static WorldPosition operator *(float d, WorldPosition a) => a * d;

        public static WorldPosition operator /(WorldPosition a, float d) => new WorldPosition(a.x / d, a.y / d, a.z / d);

        public float Magnitude => (float)Math.Sqrt(x * x + y * y + z * z);

        public float SqrMagnitude => x * x + y * y + z * z;

        public WorldPosition Normalized =>this / Magnitude;

        public static float Dot(WorldPosition a, WorldPosition b) => a.x * b.x + a.y * b.y + a.z * b.z;

        public static WorldPosition Cross(WorldPosition a, WorldPosition b) =>
            new WorldPosition(
                a.y * b.z - a.z * b.y,
                a.z * b.x - a.x * b.z,
                a.x * b.y - a.y * b.x
            );

        public static float Distance(WorldPosition a, WorldPosition b) =>(a - b).Magnitude;

        public override string ToString() => $"({x:0.###}, {y:0.###}, {z:0.###})";

        public override bool Equals(object obj) => obj is WorldPosition other && x == other.x && y == other.y && z == other.z;

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;

                hash = hash * 23 + x.GetHashCode();
                hash = hash * 23 + y.GetHashCode();
                hash = hash * 23 + z.GetHashCode();

                return hash;
            }
        }

        public static bool operator ==(WorldPosition lhs, WorldPosition rhs) => lhs.Equals(rhs);

        public static bool operator !=(WorldPosition lhs, WorldPosition rhs) =>!lhs.Equals(rhs);

        public void ReadFromPacket(PacketIn packet)
        {
            x = packet.GetFloat();
            y = packet.GetFloat();
            z = packet.GetFloat();
        }

        public void WriteToPacket(PacketOut packet)
        {
            packet.Add(x);
            packet.Add(y);
            packet.Add(z);
        }
    }
}