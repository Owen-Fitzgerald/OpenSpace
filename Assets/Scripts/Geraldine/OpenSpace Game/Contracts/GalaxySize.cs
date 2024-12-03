using Geraldine.Standards.InfoSystem;
using Geraldine.Standards.InfoSystem.Contracts;
using System.Xml.Linq;

namespace Geraldine._4XEngine.Contracts
{
    /// <summary>
    /// Class representing a size of Galaxy with various properties, 
    /// typically loaded from XML for defining characteristics.
    /// </summary>
    public class GalaxySize : ContractBase<GalaxySize>
    {
        /// The approximate number of stars in the GalaxySize. (Varies to to unique systems and story based systems)
        public int Stars { get; set; }

        /// The radius in world space of star systems
        public int Radius { get; set; }

        //Protected
        public override GalaxySize ParseFromXml(XElement element)
        {
            UniqueId =  element.Attribute("Id")?.Value;
            Name =      LoaderHelper.ParseString(element.Element("name"));
            Stars =     LoaderHelper.ParseInt32(element.Element("stars"));
            Radius =    LoaderHelper.ParseInt32(element.Element("radius"));

            return this;
        }
    }

}
