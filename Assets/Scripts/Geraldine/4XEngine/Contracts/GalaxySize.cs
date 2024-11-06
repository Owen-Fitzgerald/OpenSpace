using System;

namespace Geraldine._4XEngine.Contracts
{
    /// <summary>
    /// Class representing a size of Galaxy with various properties, 
    /// typically loaded from XML for defining characteristics.
    /// </summary>
    public class GalaxySize
    {

        /// The UniqueId of the GalaxySize.
        public string UniqueId { get; set; }

        /// The Text Name of the GalaxySize.
        public string Name { get; set; }

        /// The approximate number of stars in the GalaxySize. (Varies to to unique systems and story based systems)
        public int Stars { get; set; }

        /// The radius in world space of star systems
        public int Radius { get; set; }
    }

}
