using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geraldine._4XEngine.Initializers
{
    public class AsteroidBeltInitializer
    {
        /// The type of asteroid belt (e.g., rocky asteroid belt).
        public string Type { get; set; }

        /// Radius of the asteroid belt from the center of the system.
        public int Radius { get; set; }
    }
}
