using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geraldine._4XEngine.Interfaces
{
    public interface ISeeThroughFogOfWar
    {
        /// <summary>
        /// Vision range of the object, in adjacent Nodes
        /// </summary>
        public int Vision { get; set; }
    }
}
