using Geraldine._4XEngine.Util;
using Geraldine.Standards.InfoSystem;
using Geraldine.Standards.InfoSystem.Contracts;
using System;
using System.Xml.Linq;

namespace Geraldine._4XEngine.Contracts
{
    /// <summary>
    /// Class representing a planet type or class with various properties, 
    /// typically loaded from XML for defining planet characteristics.
    /// </summary>
    public class PlanetClass : ContractBase<PlanetClass>
    {
        /// Climate type of the planet (e.g., "dry", "temperate").
        public string Climate { get; set; }

        /// Tuple representing the minimum and maximum size range of the planet.
        public Tuple<int, int> PlanetSize { get; set; }

        /// Tuple representing the minimum and maximum size range of any moons the planet may have.
        public Tuple<int, int> MoonSize { get; set; }

        //Will make sure that modifiers for non artificial planets are not added, e.g. max districts
        public bool IsArtificialPlanet { get; set; }

        //default = yes; Can be used to block all other additions of max building and district modifiers except the planet class specific ones.
        public bool InheritCountryBuildingModifiers { get; set; } = true;

        #region VISUAL RENDERING

        /// The name or ID of the entity representing the visual model of the planet.
        public string Entity { get; set; }

        /// The scale factor to be applied to the planet's visual model.
        public float EntityScale { get; set; }

        /// The icon used to represent this planet class in the UI.
        public string Icon { get; set; }

        /// Color of the planet's atmosphere, represented as HSV values (hue, saturation, value).
        public float[] AtmosphereColor { get; set; }

        /// Intensity of the planet's atmosphere color.
        public float AtmosphereIntensity { get; set; }

        /// Width of the planet's atmosphere.
        public float AtmosphereWidth { get; set; }

        /// The LUT (Look-Up Table) to be used for the planet's city color correction.
        public string CityColorLut { get; set; }

        #endregion

        #region COLONIZATION
        /// Determines whether the planet can be colonized.
        public bool Colonizable { get; set; }

        /// The string Id of the tech required to colonize this planet class.
        public string ColonizationTech { get; set; }

        /// Default is no, only specified for planets available as starting classes for custom species.
        public bool Initial { get; set; }
        #endregion

        #region SPAWNING

        /// The chance of this planet spawning in the game (0 to 100 scale).
        public int SpawnOdds { get; set; }

        /// Tuple representing the minimum and maximum size range of distance from the sun the planet may have.
        public Tuple<int, int> DistanceFromTheSun { get; set; }

        /// The additional orbit size to add if multiple planets are in the same orbit.
        public int ExtraOrbitSize { get; set; }

        /// The number of extra planets to generate around this planet (if applicable).
        public int ExtraPlanetCount { get; set; }

        /// Determines if this planet class can serve as a moon.
        public bool CanBeMoon { get; set; }

        /// The probability of this planet having a ring system (0.0 to 1.0 scale).
        public float ChanceOfRing { get; set; }

        /// Determines if this planet should use alternative sky visuals when it is a moon.
        public bool UsesAlternativeSkiesForMoons { get; set; }

        #endregion

        #region CITIES

        /// The set of districts this planet supports (e.g., "standard", "industrial").
        public string DistrictSet { get; set; }

        /// Carrying capacity per available free district on the planet.
        public int CarryCapPerFreeDistrict { get; set; }

        #endregion

        public override PlanetClass ParseFromXml(XElement element)
        {
            UniqueId =                      element.Attribute("Id")?.Value;
            Name =                          LoaderHelper.ParseString(element.Element("name"));
            Entity =                        LoaderHelper.ParseString(element.Element("entity"));
            EntityScale =                   LoaderHelper.ParseFloat(element.Element("entity_scale"), 1f);
            Icon =                          LoaderHelper.ParseString(element.Element("icon"));
            AtmosphereColor =               LoaderHelper.ParseHsvColor(element.Element("atmosphere_color"));
            AtmosphereIntensity =           LoaderHelper.ParseFloat(element.Element("atmosphere_intensity"), 1f);
            AtmosphereWidth =               LoaderHelper.ParseFloat(element.Element("atmosphere_width"), 1);
            CityColorLut =                  LoaderHelper.ParseString(element.Element("city_color_lut"));
            Climate =                       LoaderHelper.ParseString(element.Element("climate"));
            Colonizable =                   LoaderHelper.ParseBool(element.Element("colonizable"));
            Initial =                       LoaderHelper.ParseBool(element.Element("initial"));
            PlanetSize =                    LoaderHelper.ParseTuple(element.Element("planet_size"));
            MoonSize =                      LoaderHelper.ParseTuple(element.Element("moon_size"));
            SpawnOdds =                     LoaderHelper.ParseInt32(element.Element("spawn_odds"));
            DistanceFromTheSun =            LoaderHelper.ParseTuple(element.Element("distance_from_sun"));
            ExtraOrbitSize =                LoaderHelper.ParseInt32(element.Element("extra_orbit_size"));
            ExtraPlanetCount =              LoaderHelper.ParseInt32(element.Element("extra_planet_count"));
            CanBeMoon =                     LoaderHelper.ParseBool(element.Element("can_be_moon"));
            ChanceOfRing =                  LoaderHelper.ParseFloat(element.Element("chance_of_ring"));
            UsesAlternativeSkiesForMoons =  LoaderHelper.ParseBool(element.Element("uses_alternative_skies_for_moons"));
            DistrictSet =                   LoaderHelper.ParseString(element.Element("district_set"));
            CarryCapPerFreeDistrict =       LoaderHelper.ParseInt32(element.Element("carry_cap_per_free_district"));

            return this;
        }
    }
}
