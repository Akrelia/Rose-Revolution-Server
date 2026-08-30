using RevolutionShared.Networking.Packets;
using System;

namespace RevolutionShared.Data
{
    [Serializable]
    public struct WorldRotation : IPacketWritable, IPacketReadable
    {
        public float W;
        public float X;
        public float Y;
        public float Z;

        public WorldRotation(float w, float x, float y, float z)
        {
            W = w;
            X = x;
            Y = y;
            Z = z;
        }

        public void ReadFromPacket(PacketIn packet)
        {
            W = packet.GetFloat();
            X = packet.GetFloat();
            Y = packet.GetFloat();
            Z = packet.GetFloat();
        }

        public void WriteToPacket(PacketOut packet)
        {
            packet.Add(W);
            packet.Add(X);
            packet.Add(Y);
            packet.Add(Z);
        }
    }
}