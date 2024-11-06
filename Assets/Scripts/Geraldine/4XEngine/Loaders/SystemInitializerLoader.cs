using Geraldine._4XEngine.Initializers;
using Geraldine._4XEngine.Loaders.Interfaces;
using Geraldine._4XEngine.Util;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Xml.Linq;
using UnityEngine.Rendering.VirtualTexturing;

namespace Geraldine._4XEngine.Loaders
{
    public class SystemInitializerLoader : IDataLoader
    {
        string directoryPath = "Common/SystemInits";
        string dbPath = LoaderHelper.GetDatabaseFilePath();

        // Static instance for singleton
        private static SystemInitializerLoader _instance;

        // Public property to get the instance
        public static SystemInitializerLoader Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SystemInitializerLoader();
                }
                return _instance;
            }
        }

        private static Dictionary<string, SystemInitializer> systemInits = new Dictionary<string, SystemInitializer>();


        private SystemInitializerLoader()
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
                CREATE TABLE IF NOT EXISTS SystemInitializers (
                    ID INTEGER PRIMARY KEY AUTOINCREMENT,
                    Entity TEXT,
                    Name TEXT,
                    PlanetClass TEXT,
                    Size INTEGER,
                    IsMoon INTEGER
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
                var systemInitsElements = xmlDoc.Descendants("Predefined_Systems");

                foreach (var element in systemInitsElements)
                {
                    SystemInitializer systemInit = ParseSystem(element);

                    // Store the planet class in the dictionary for later reference
                    string systemInitKey = systemInit.UniqueId;  // Use entity name as a key
                    systemInits[systemInitKey] = systemInit;
                }
            }
        }

        public void SaveDataToSql()
        {
            // Connect to the SQLite database
            using (SQLiteConnection connection = new SQLiteConnection($"Data Source={dbPath};Version=3;"))
            {
                connection.Open();

                foreach (var system in systemInits.Values)
                {
                    // Insert the system into the database
                    string insertQuery = @"INSERT INTO systems (name, class, usage, max_instances, scaled_spawn_chance, spawn_chance) 
                                   VALUES (@name, @class, @usage, @max_instances, @scaled_spawn_chance, @spawn_chance)";

                    using (SQLiteCommand command = new SQLiteCommand(insertQuery, connection))
                    {
                        command.Parameters.AddWithValue("@name", system.Name);
                        command.Parameters.AddWithValue("@class", system.Class);
                        command.Parameters.AddWithValue("@usage", system.Usage);
                        command.Parameters.AddWithValue("@max_instances", system.MaxInstances);
                        command.Parameters.AddWithValue("@scaled_spawn_chance", system.ScaledSpawnChance);
                        command.Parameters.AddWithValue("@spawn_chance", system.SpawnChance);
                        command.ExecuteNonQuery();
                    }

                    // Get the inserted system ID
                    long systemId = connection.LastInsertRowId;

                    // Insert planets into the planets table
                    foreach (var planet in system.Planets)
                    {
                        string planetInsertQuery = @"INSERT INTO planets (system_id, class, orbit_distance, orbit_angle, size, has_ring)
                                             VALUES (@system_id, @class, @orbit_distance, @orbit_angle, @size, @has_ring)";

                        using (SQLiteCommand planetCommand = new SQLiteCommand(planetInsertQuery, connection))
                        {
                            planetCommand.Parameters.AddWithValue("@system_id", systemId);
                            planetCommand.Parameters.AddWithValue("@class", planet.PlanetClass);
                            planetCommand.Parameters.AddWithValue("@orbit_distance", planet.OrbitDistance);
                            planetCommand.Parameters.AddWithValue("@orbit_angle", planet.OrbitAngle);
                            planetCommand.Parameters.AddWithValue("@size", planet.Size);
                            planetCommand.Parameters.AddWithValue("@has_ring", planet.HasRing);
                            planetCommand.ExecuteNonQuery();
                        }
                    }
                }
            }
        }

        public static SystemInitializer GetSystemInitializerByReference(string reference)
        {
            if (systemInits.ContainsKey(reference))
            {
                return systemInits[reference];
            }
            throw new Exception($"Predefined System {reference} not found.");
        }

        public static IEnumerable<String> GetAllSystemInitializerReferences()
        {
            return systemInits.Keys;
        }

        public void UnloadData()
        {
            // Optional: clear planetClasses or other resources
            systemInits.Clear();
        }

        public void ReloadData()
        {
            UnloadData();
            LoadDataFromXml();
            SaveDataToSql();
        }

        //Private

        private SystemInitializer ParseSystem(XElement systemElement)
        {
            SystemInitializer system = new SystemInitializer
            {
                Name = systemElement.Element("Name")?.Value,
                Class = systemElement.Element("Class")?.Value,
                Usage = systemElement.Element("Usage")?.Value,
                MaxInstances = int.Parse(systemElement.Element("MaxInstances")?.Value),
                ScaledSpawnChance = int.Parse(systemElement.Element("ScaledSpawnChance")?.Value),
                SpawnChance = int.Parse(systemElement.Element("SpawnChance")?.Value),
                Planets = ParsePlanets(systemElement.Element("Planets"))
            };

            // Optional usage odds parsing
            var usageOddsElement = systemElement.Element("UsageOdds");
            if (usageOddsElement != null)
            {
                system.UsageOdds = new List<UsageOdds>();

                foreach (var modifier in usageOddsElement.Elements("Modifier"))
                {
                    var odds = new UsageOdds
                    {
                        Factor = int.Parse(modifier.Element("Factor")?.Value),
                        IsFeCluster = bool.Parse(modifier.Element("IsFeCluster")?.Value ?? "false"),
                        HasStarFlag = modifier.Element("HasStarFlag")?.Value,
                        IsBottleneckSystem = bool.Parse(modifier.Element("IsBottleneckSystem")?.Value ?? "false")
                    };

                    system.UsageOdds.Add(odds);
                }
            }

            return system;
        }

        private List<PlanetInitializer> ParsePlanets(XElement planetsElement)
        {
            List<PlanetInitializer> planets = new List<PlanetInitializer>();

            foreach (var planetElement in planetsElement.Elements("Planet"))
            {
                PlanetInitializer planet = new PlanetInitializer
                {
                    IsMoon = false,
                    PlanetClass = planetElement.Element("PlanetClass")?.Value,
                    OrbitDistance = int.Parse(planetElement.Element("OrbitDistance")?.Value),
                    OrbitAngle = LoaderHelper.ParseSizeTuple(planetElement.Element("OrbitAngle")?.Value),
                    Size = int.Parse(planetElement.Element("Size")?.Value),
                    HasRing = bool.Parse(planetElement.Element("HasRing")?.Value ?? "false")
                };

                // Parse moons
                var moonElement = planetElement.Element("Moon");
                if (moonElement != null)
                {
                    planet.Moons.Add(new PlanetInitializer
                    {
                        IsMoon = true,
                        PlanetClass = moonElement.Element("PlanetClass")?.Value,
                        Size = int.Parse(moonElement.Element("Size")?.Value),
                        OrbitDistance = int.Parse(moonElement.Element("OrbitDistance")?.Value),
                        OrbitAngle = moonElement.Element("OrbitAngle") != null
                            ? new Tuple<int, int>(int.Parse(moonElement.Element("OrbitAngle").Attribute("min").Value),
                                                  int.Parse(moonElement.Element("OrbitAngle").Attribute("max").Value))
                            : null
                    });
                }

                planets.Add(planet);
            }

            return planets;
        }
    }
}
