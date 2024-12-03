using System;

namespace Geraldine._4XEngine.Contracts
{
    /// <summary>
    /// Class representing a size of Galaxy with various properties, 
    /// typically loaded from XML for defining characteristics.
    /// </summary>
    public class StellarClass
    {

        /// The UniqueId of the StellarClass.
        public string UniqueId { get; set; }

        /// The Text Name of the StellarClass.
        public string Name { get; set; }

        /// In solar mass units
        public Tuple<int, int> Mass { get; set; }

        /// In solar radius units
        public Tuple<int, int> Radius{ get; set; }

        /// In solar luminosity units
        public Tuple<int, int> Luminosity{ get; set; }

        /// In Kelvin
        public Tuple<int, int> Temperature{ get; set; }
    }

}
