using Geraldine._4XEngine.Contracts;
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
    public class GalaxyAgeLoader : IDataLoader
    {
        string directoryPath = "Common/Galaxy Ages";
        string dbPath = LoaderHelper.GetDatabaseFilePath();

        // Static instance for singleton
        private static GalaxyAgeLoader _instance;

        // Public property to get the instance
        public static GalaxyAgeLoader Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GalaxyAgeLoader();
                }
                return _instance;
            }
        }

        private static Dictionary<string, GalaxyAge> galaxyAges = new Dictionary<string, GalaxyAge>();


        private GalaxyAgeLoader()
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
                CREATE TABLE IF NOT EXISTS GalaxyAges (
                    ID INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT,
                    Years INTEGER
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
                var systemInitsElements = xmlDoc.Descendants("Galaxy_Ages");

                foreach (var element in systemInitsElements)
                {
                    GalaxyAge systemInit = ParseSystem(element);

                    // Store the planet class in the dictionary for later reference
                    string systemInitKey = systemInit.UniqueId;  // Use entity name as a key
                    galaxyAges[systemInitKey] = systemInit;
                }
            }
        }

        public void SaveDataToSql()
        {
            // Connect to the SQLite database
            using (SQLiteConnection connection = new SQLiteConnection($"Data Source={dbPath};Version=3;"))
            {
                connection.Open();

                foreach (var galaxySize in galaxyAges.Values)
                {
                    // Insert the system into the database
                    string insertQuery = @"INSERT INTO systems (name, years) 
                                   VALUES (@name, @years)";

                    using (SQLiteCommand command = new SQLiteCommand(insertQuery, connection))
                    {
                        command.Parameters.AddWithValue("@name", galaxySize.Name);
                        command.Parameters.AddWithValue("@years", galaxySize.Years);
                        command.ExecuteNonQuery();
                    }
                }
            }
        }

        public static GalaxyAge GetSystemInitializerByReference(string reference)
        {
            if (galaxyAges.ContainsKey(reference))
            {
                return galaxyAges[reference];
            }
            throw new Exception($"Galaxy Size {reference} not found.");
        }

        public static IEnumerable<String> GetAllSystemInitializerReferences()
        {
            return galaxyAges.Keys;
        }

        public void UnloadData()
        {
            // Optional: clear planetClasses or other resources
            galaxyAges.Clear();
        }

        public void ReloadData()
        {
            UnloadData();
            LoadDataFromXml();
            SaveDataToSql();
        }

        //Private
        private GalaxyAge ParseSystem(XElement systemElement)
        {
            GalaxyAge system = new GalaxyAge
            {
                Name = systemElement.Element("Name")?.Value,
                Years = int.Parse(systemElement.Element("Years")?.Value)
            };

            return system;
        }
    }
}
