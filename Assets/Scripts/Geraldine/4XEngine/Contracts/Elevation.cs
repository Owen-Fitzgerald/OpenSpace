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
    public class Elevation : ContractBase<Elevation>
    {
        ///
        public string IconName { get; set; }

        ///
        public int MovementCost { get; set; }

        ///
        public Tuple<int, int> GenerationValues { get; set; }

        ///
        public int ZValue { get; set; }

        ///
        public bool IsFreshWaterSource { get; set; }

        ///
        public bool IsLand { get; set; }

        ///
        public bool IsUnderwater { get; set; }

        ///
        public bool NoVegetation { get; set; }

        ///
        public bool Impassable { get; set; }

        ///
        public bool CitySite { get; set; }

        public override Elevation ParseFromXml(XElement element)
        {
            UniqueId =          element.Attribute("Id")?.Value;
            Name =              LoaderHelper.ParseString(element.Element("Name"));
            IconName =          LoaderHelper.ParseString(element.Element("IconName"));
            MovementCost =      LoaderHelper.ParseInt32(element.Element("MovementCost"));
            GenerationValues = LoaderHelper.ParseTuple(element.Element("GenerationValues"));
            ZValue =            LoaderHelper.ParseInt32(element.Element("ZValue"));
            IsFreshWaterSource =LoaderHelper.ParseBool(element.Element("IsFreshWaterSource"));
            IsLand =            LoaderHelper.ParseBool(element.Element("IsLand"));
            IsUnderwater =      LoaderHelper.ParseBool(element.Element("IsUnderwater"));
            NoVegetation =      LoaderHelper.ParseBool(element.Element("NoVegetation"));
            Impassable =        LoaderHelper.ParseBool(element.Element("Impassable"));
            CitySite =          LoaderHelper.ParseBool(element.Element("CitySite"));

            return this;
        }
    }

}
