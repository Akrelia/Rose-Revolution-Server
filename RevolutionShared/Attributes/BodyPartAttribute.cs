using RevolutionShared.Rose.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevolutionShared.Attributes
{
    /// <summary>
    /// Attribute to link Data and BodyPartType.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class BodyPartAttribute : Attribute
    {
        public BodyPartType Type { get; }

        public BodyPartAttribute(BodyPartType type)
        {
            Type = type;
        }
    }
}
