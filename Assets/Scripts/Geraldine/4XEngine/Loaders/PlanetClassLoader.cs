using Geraldine._4XEngine.Contracts;
using Geraldine._4XEngine.Loaders.Interfaces;
using Geraldine._4XEngine.Util;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Xml.Linq;

namespace Geraldine._4XEngine.Loaders
{
    public class PlanetClassLoader : IDataLoader
    {
        string directoryPath = "Common/Planet Classes";
        string dbPath = LoaderHelper.GetDatabaseFilePath();

        // Static instance for singleton
        private static PlanetClassLoader _instance;

        // Public property to get the instance
        public static PlanetClassLoader Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new PlanetClassLoader();
                }
                return _instance;
            }
        }

        private static Dictionary<string, PlanetClass> planetClasses = new Dictionary<string, PlanetClass>();


        private PlanetClassLoader()
        {
            // Ensure the database file exists and create tables if necessary
            if (!File.Exists(dbPath))
            {
                SQLiteConnection.CreateFile(dbPath);
            }

            CreateTable();
            ReloadData();
        }

        public void CreateTable()
        {
            using (SQLiteConnection connection = new SQLiteConnection($"Data Source={dbPath};Version=3;"))
            {
                connection.Open();

                string createTableQuery = @"
                CREATE TABLE IF NOT EXISTS PlanetClasses (
                    ID INTEGER PRIMARY KEY AUTOINCREMENT,
                    Entity TEXT,
                    EntityScale REAL,
                    Icon TEXT,
                    AtmosphereColor TEXT,
                    AtmosphereIntensity REAL,
                    AtmosphereWidth REAL,
                    CityColorLut TEXT,
                    Climate TEXT,
                    Colonizable INTEGER,
                    Initial INTEGER,
                    MinPlanetSize INTEGER,
                    MaxPlanetSize INTEGER,
                    MinMoonSize INTEGER,
                    MaxMoonSize INTEGER,
                    SpawnOdds INTEGER,
                    MinDistanceFromSun INTEGER,
                    MaxDistanceFromSun INTEGER,
                    ExtraOrbitSize INTEGER,
                    ExtraPlanetCount INTEGER,
                    CanBeMoon INTEGER,
                    ChanceOfRing REAL,
                    UsesAlternativeSkiesForMoons INTEGER,
                    DistrictSet TEXT,
                    CarryCapPerFreeDistrict INTEGER
                )";

                using (SQLiteCommand command = new SQLiteCommand(createTableQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        public void LoadDataFromXml()
        {
            // Get all XML files from the directory
            var xmlFiles = Directory.GetFiles(directoryPath, "*.xml");

            // Parse each XML file
            foreach (var file in xmlFiles)
            {
                XDocument xmlDoc = XDocument.Load(file);
                var planetClassElements = xmlDoc.Descendants("planet_class");

                foreach (var element in planetClassElements)
                {
                    PlanetClass planetClass = ParsePlanetClassFromElement(element);

                    // Store the planet class in the dictionary for later reference
                    string planetClassKey = planetClass.Entity;  // Use entity name as a key
                    planetClasses[planetClassKey] = planetClass;
                }
            }
        }

        public void SaveDataToSql()
        {
            using (SQLiteConnection connection = new SQLiteConnection($"Data Source={dbPath};Version=3;"))
            {
                connection.Open();

                foreach (var planetClass in planetClasses.Values)
                {
                    string query = @"INSERT INTO PlanetClasses (Entity, EntityScale, Icon, AtmosphereColor, AtmosphereIntensity, AtmosphereWidth, 
                                CityColorLut, Climate, Colonizable, Initial, MinPlanetSize, MaxPlanetSize, MinMoonSize, MaxMoonSize, 
                                SpawnOdds, UsesAlternativeSkiesForMoons, DistrictSet, CarryCapPerFreeDistrict)
                                VALUES (@Entity, @EntityScale, @Icon, @AtmosphereColor, @AtmosphereIntensity, @AtmosphereWidth, 
                                @CityColorLut, @Climate, @Colonizable, @Initial, @MinPlanetSize, @MaxPlanetSize, @MinMoonSize, 
                                @MaxMoonSize, @SpawnOdds, @UsesAlternativeSkiesForMoons, @DistrictSet, @CarryCapPerFreeDistrict)";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@Entity", planetClass.Entity);
                        cmd.Parameters.AddWithValue("@EntityScale", planetClass.EntityScale);
                        cmd.Parameters.AddWithValue("@Icon", planetClass.Icon);
                        cmd.Parameters.AddWithValue("@AtmosphereColor", string.Join(",", planetClass.AtmosphereColor));
                        cmd.Parameters.AddWithValue("@AtmosphereIntensity", planetClass.AtmosphereIntensity);
                        cmd.Parameters.AddWithValue("@AtmosphereWidth", planetClass.AtmosphereWidth);
                        cmd.Parameters.AddWithValue("@CityColorLut", planetClass.CityColorLut);
                        cmd.Parameters.AddWithValue("@Climate", planetClass.Climate);
                        cmd.Parameters.AddWithValue("@Colonizable", planetClass.Colonizable ? 1 : 0);
                        cmd.Parameters.AddWithValue("@Initial", planetClass.Initial ? 1 : 0);
                        cmd.Parameters.AddWithValue("@MinPlanetSize", planetClass.PlanetSize.Item1);
                        cmd.Parameters.AddWithValue("@MaxPlanetSize", planetClass.PlanetSize.Item2);
                        cmd.Parameters.AddWithValue("@MinMoonSize", planetClass.MoonSize.Item1);
                        cmd.Parameters.AddWithValue("@MaxMoonSize", planetClass.MoonSize.Item2);
                        cmd.Parameters.AddWithValue("@SpawnOdds", planetClass.SpawnOdds);
                        cmd.Parameters.AddWithValue("@MinDistanceFromSun", planetClass.DistanceFromTheSun.Item1);
                        cmd.Parameters.AddWithValue("@MaxDistanceFromSun", planetClass.DistanceFromTheSun.Item2);
                        cmd.Parameters.AddWithValue("@ExtraOrbitSize", planetClass.ExtraOrbitSize);
                        cmd.Parameters.AddWithValue("@ExtraPlanetCount", planetClass.ExtraPlanetCount);
                        cmd.Parameters.AddWithValue("@CanBeMoon", planetClass.CanBeMoon ? 1 : 0);
                        cmd.Parameters.AddWithValue("@ChanceOfRing", planetClass.ChanceOfRing);
                        cmd.Parameters.AddWithValue("@UsesAlternativeSkiesForMoons", planetClass.UsesAlternativeSkiesForMoons ? 1 : 0);
                        cmd.Parameters.AddWithValue("@DistrictSet", planetClass.DistrictSet);
                        cmd.Parameters.AddWithValue("@CarryCapPerFreeDistrict", planetClass.CarryCapPerFreeDistrict);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        public static PlanetClass GetPlanetClassByReference(string reference)
        {
            if (planetClasses.ContainsKey(reference))
            {
                return planetClasses[reference];
            }
            throw new Exception($"Planet class {reference} not found.");
        }

        public static IEnumerable<String> GetAllPlanetClassReferences()
        {
            return planetClasses.Keys;
        }

        public void UnloadData()
        {
            // Optional: clear planetClasses or other resources
            planetClasses.Clear();
        }

        public void ReloadData()
        {
            UnloadData();
            LoadDataFromXml();
            SaveDataToSql();
        }

        //Private

        private PlanetClass ParsePlanetClassFromElement(XElement element)
        {
            PlanetClass planetClass = new PlanetClass
            {
                Entity = element.Element("entity")?.Value,
                EntityScale = float.Parse(element.Element("entity_scale")?.Value ?? "1.0"),
                Icon = element.Element("icon")?.Value,
                AtmosphereColor = LoaderHelper.ParseHsvColor(element.Element("atmosphere_color")),
                AtmosphereIntensity = float.Parse(element.Element("atmosphere_intensity")?.Value ?? "1.0"),
                AtmosphereWidth = float.Parse(element.Element("atmosphere_width")?.Value ?? "1.0"),
                CityColorLut = element.Element("city_color_lut")?.Value,
                Climate = element.Element("climate")?.Value,
                Colonizable = element.Element("colonizable")?.Value == "yes",
                Initial = element.Element("initial")?.Value == "yes",
                PlanetSize = LoaderHelper.ParseSizeTuple(element.Element("planet_size")?.Value),
                MoonSize = LoaderHelper.ParseSizeTuple(element.Element("moon_size")?.Value),
                SpawnOdds = int.Parse(element.Element("spawn_odds")?.Value ?? "0"),
                DistanceFromTheSun = LoaderHelper.ParseSizeTuple(element.Element("distance_from_sun")?.Value),
                ExtraOrbitSize = int.Parse(element.Element("extra_orbit_size")?.Value ?? "0"),
                ExtraPlanetCount = int.Parse(element.Element("extra_planet_count")?.Value ?? "0"),
                CanBeMoon = element.Element("can_be_moon")?.Value == "yes",
                ChanceOfRing = float.Parse(element.Element("chance_of_ring")?.Value ?? "0.0"),
                UsesAlternativeSkiesForMoons = element.Element("uses_alternative_skies_for_moons")?.Value == "yes",
                DistrictSet = element.Element("district_set")?.Value,
                CarryCapPerFreeDistrict = int.Parse(element.Element("carry_cap_per_free_district")?.Value ?? "0")
            };

            return planetClass;
        }
    }
}