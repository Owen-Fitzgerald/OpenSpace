using Geraldine.Standards.InfoSystem;
using Geraldine.Standards.InfoSystem.Contracts;
using System;
using System.Xml.Linq;

namespace Geraldine._4XEngine.Contracts
{
    /// <summary>
    /// Class representing a age of Galaxy with various properties, 
    /// typically loaded from XML for defining characteristics.
    /// </summary>
    public class GalaxyAge : ContractBase<GalaxyAge>
    {
        /// The approximate number of years since the Galaxy was birthed from God's loins.
        public float Years { get; set; }

        public override GalaxyAge ParseFromXml(XElement element)
        {
            UniqueId =  element.Attribute("Id")?.Value;
            Name =      LoaderHelper.ParseString(element.Element("name"));
            Years =     LoaderHelper.ParseFloat(element.Element("years"));

            return this;
        }
    }

}
