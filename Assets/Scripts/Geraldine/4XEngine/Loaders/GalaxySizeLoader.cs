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
    public class GalaxySizeLoader : IDataLoader
    {
        string directoryPath = "Common/Galaxy Sizes";
        string dbPath = LoaderHelper.GetDatabaseFilePath();

        // Static instance for singleton
        private static GalaxySizeLoader _instance;

        // Public property to get the instance
        public static GalaxySizeLoader Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GalaxySizeLoader();
                }
                return _instance;
            }
        }

        private static Dictionary<string, GalaxySize> galaxySizes = new Dictionary<string, GalaxySize>();


        private GalaxySizeLoader()
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
                CREATE TABLE IF NOT EXISTS GalaxySizes (
                    ID INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT,
                    Stars INTEGER,
                    Radius INTEGER
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
                var systemInitsElements = xmlDoc.Descendants("Galaxy_Sizes");

                foreach (var element in systemInitsElements)
                {
                    GalaxySize systemInit = ParseSystem(element);

                    // Store the planet class in the dictionary for later reference
                    string systemInitKey = systemInit.UniqueId;  // Use entity name as a key
                    galaxySizes[systemInitKey] = systemInit;
                }
            }
        }

        public void SaveDataToSql()
        {
            // Connect to the SQLite database
            using (SQLiteConnection connection = new SQLiteConnection($"Data Source={dbPath};Version=3;"))
            {
                connection.Open();

                foreach (var galaxySize in galaxySizes.Values)
                {
                    // Insert the system into the database
                    string insertQuery = @"INSERT INTO systems (name, stars, radius) 
                                   VALUES (@name, @stars, @radius)";

                    using (SQLiteCommand command = new SQLiteCommand(insertQuery, connection))
                    {
                        command.Parameters.AddWithValue("@name", galaxySize.Name);
                        command.Parameters.AddWithValue("@stars", galaxySize.Stars);
                        command.Parameters.AddWithValue("@radius", galaxySize.Radius);
                        command.ExecuteNonQuery();
                    }
                }
            }
        }

        public static GalaxySize GetSystemInitializerByReference(string reference)
        {
            if (galaxySizes.ContainsKey(reference))
            {
                return galaxySizes[reference];
            }
            throw new Exception($"Galaxy Size {reference} not found.");
        }

        public static IEnumerable<String> GetAllSystemInitializerReferences()
        {
            return galaxySizes.Keys;
        }

        public void UnloadData()
        {
            // Optional: clear planetClasses or other resources
            galaxySizes.Clear();
        }

        public void ReloadData()
        {
            UnloadData();
            LoadDataFromXml();
            SaveDataToSql();
        }

        //Private
        private GalaxySize ParseSystem(XElement systemElement)
        {
            GalaxySize system = new GalaxySize
            {
                Name = systemElement.Element("Name")?.Value,
                Stars = int.Parse(systemElement.Element("Stars")?.Value),
                Radius = int.Parse(systemElement.Element("Radius")?.Value)
            };

            return system;
        }
    }
}
