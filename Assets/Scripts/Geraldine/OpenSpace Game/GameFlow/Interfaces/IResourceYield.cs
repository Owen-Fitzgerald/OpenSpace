using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geraldine.OpenSpaceGame.Interfaces
{
    public interface IResourceYield
    {
        public string Name { get; set; }

        public float BaseYield { get; set; }
    }
}
