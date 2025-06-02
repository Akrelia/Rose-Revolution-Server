using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoseSandboxServer.Core.Data
{
    /// <summary>
    /// Entity.
    /// </summary>
    public class Entity
    {
        public int id;
        public int dataId;
        public Vector3 position;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="id">Id of the monster.</param>
        /// <param name="dataId">Data ID</param>
        /// <param name="position">Initial position.</param>
        public Entity(int id, int dataId, Vector3 position)
        {
            this.id = id;
            this.dataId = dataId;
            this.position = position;
        }
    }
}
