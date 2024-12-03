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
    public class Temperature : ContractBase<Temperature>
    {
        ///
        public string IconName { get; set; }

        ///
        public Tuple<int,int> GenerationValues { get; set; }

        public override Temperature ParseFromXml(XElement element)
        {
            UniqueId =          element.Attribute("Id")?.Value;
            Name =              LoaderHelper.ParseString(element.Element("Name"));
            IconName =          LoaderHelper.ParseString(element.Element("IconName"));
            GenerationValues =  LoaderHelper.ParseTuple(element.Element("GenerationValues"));

            return this;
        }
    }

}
