using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevolutionShared.Packets
{
    /// <summary>
    /// Every packet commands from the client
    /// </summary>
    public enum ClientCommands : short
    {
        Ping = 0x998,
        Pong = 0x999,
        ConnectSandbox = 0x1000,
        SpawnMonster = 0x1001,
        SendNormalChat = 0x1002,
        SendShout = 0x1002,
        DisconnectSandbox = 0x1003,
        GetWorld = 0x1004
    }

    /// <summary>
    /// Every packet commands from the server.
    /// </summary>
    public enum ServerCommands : short
    {
        Ping = 0x998,
        Pong = 0x999,
        SandboxConnectionResponse = 0x1000,
        AddEntities = 0x1001,
        MessageReceived = 0x1002,
        PlayerConnected = 0x1003,
        PlayerDisconnected = 0x1004,
        SendWorld = 0x1005
    }
}
