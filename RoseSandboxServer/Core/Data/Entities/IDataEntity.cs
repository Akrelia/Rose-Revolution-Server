using RevolutionShared.Rose.Data.NPC;

namespace RoseSandboxServer.Core.Data.Entities
{
    public interface IDataEntity<T> where T : EntityData
    {
        T Data { get;}
    }
}
