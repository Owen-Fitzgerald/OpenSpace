using System;

namespace Geraldine._4XEngine.Contracts
{
    /// <summary>
    /// Class representing a age of Galaxy with various properties, 
    /// typically loaded from XML for defining characteristics.
    /// </summary>
    public class GalaxyAge
    {

        /// The UniqueId of the GalaxyAge.
        public string UniqueId { get; set; }

        /// The Text Name of the GalaxyAge.
        public string Name { get; set; }

        /// The approximate number of years since the Galaxy was birthed from God's loins.
        public int Years { get; set; }
    }

}
