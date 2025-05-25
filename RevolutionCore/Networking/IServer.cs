using RevolutionShared.Networking.Packets;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevolutionCore.Networking
{
    public interface IServer<T> where T : RoseClient
    {
        List<T> Clients { get; }
        Task SendPacket(T client, PacketOut packet);
        void Disconnect(T client);
    }
}
