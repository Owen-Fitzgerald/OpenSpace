using Geraldine._4XEngine.Contracts;
using Geraldine._4XEngine.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geraldine._4XEngine.Initializers
{
    public class GalaxyInitializer
    {
        // Total number of star systems in the galaxy
        public GalaxySize Size { get; set; }

        // Density of hyperlanes, determines how many hyperlanes connect systems
        public float HyperlaneDensity { get; set; }

        // Density of habitable planets in the galaxy
        public float HabitablePlanetDensity { get; set; }

        // Number of starting empires/nations to be placed
        public int NumberOfNations { get; set; }

        // Minimum distance between starting systems of empires
        public float MinEmpireDistance { get; set; }

        // Reference to a precursor story or "random" for random precursor story
        public string PrecursorStory { get; set; }

        // Reference to a crisis story or "random" for random crisis story
        public string CrisisStory { get; set; }

        // Age of the galaxy, could affect star types, resource abundance, etc.
        public GalaxyAge Age { get; set; }

        public GalaxyInitializer()
        {
        }
    }
}
