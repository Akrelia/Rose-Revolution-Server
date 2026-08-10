using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoseSandboxServer.Networking.Contexts
{
    /// <summary>
    /// Tick context.
    /// </summary>
    public class TickContext : Context
    {
        double time;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="time">Time.</param>
        public TickContext(double time) : base()
        {
            this.time = time;
        }

        /// <summary>
        /// Get the time elapsed since last tick.
        /// </summary>
        public double Time
        {
            get { return time; }
        }
    }
}
