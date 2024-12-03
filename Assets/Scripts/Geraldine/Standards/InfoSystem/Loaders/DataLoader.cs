using Geraldine._4XEngine.Loaders.Interfaces;
using Geraldine.Standards.InfoSystem;
using Geraldine.Standards.InfoSystem.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Geraldine._4XEngine.Loaders
{
    public abstract class DataLoader<T, U> : IDataLoader
        where T : ContractBase<T>, new()
        where U : DataLoader<T, U>, new()
    {
        protected const string directoryPath = "Assets/Common/";
        protected string dbPath = LoaderHelper.GetDatabaseFilePath();

        // abstract method for overrides
        protected abstract string directoryName { get; }
        protected abstract string descendantName { get; }

        private static readonly object _lock = new object();

        // Static instance for singleton
        private static U _instance;

        // Public property to get the instance
        public static U Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new U();
                        }
                    }
                }
                return _instance;
            }
        }

        //private store for loaded contracts
        private Dictionary<string, T> database = new Dictionary<string, T>();


        protected DataLoader()
        {
            ReloadData();
        }

        public virtual void LoadDataFromXml()
        {
            try
            {
                // Get all XML files from the directory
                var xmlFiles = Directory.GetFiles(Path.Combine(directoryPath, directoryName), "*.xml");

                // Parse each XML file
                foreach (var file in xmlFiles)
                {
                    XDocument xmlDoc = XDocument.Load(file);
                    var systemInitsElements = xmlDoc.Descendants(descendantName);

                    foreach (var element in systemInitsElements)
                    {
                        T parsedObject = ParseXmlElement(element);

                        // Store the planet class in the dictionary for later reference
                        string systemInitKey = parsedObject.UniqueId;  // Use entity name as a key
                        database[systemInitKey] = parsedObject;
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the error or provide meaningful context
                Console.WriteLine($"Error loading XML files: {ex.Message}");
                throw;
            }
        }

        public static T GetContractByReference(string reference)
        {
            if (Instance.database.ContainsKey(reference))
            {
                return Instance.database[reference];
            }
            throw new Exception($"Contract {reference} not found.");
        }

        public static IEnumerable<String> GetAllContractReferences()
        {
            return Instance.database.Keys;
        }

        public static T GetContractByIndex(int index)
        {
            if (Instance.database.Count > index)
            {
                return Instance.database.Values.ToList()[index];
            }
            throw new Exception($"Contract of index {index} not found.");
        }

        public virtual void UnloadData()
        {
            // Optional: clear planetClasses or other resources
            database.Clear();
        }

        public virtual void ReloadData()
        {
            UnloadData();
            LoadDataFromXml();
        }

        //Protected
        protected virtual T ParseXmlElement(XElement element)
        {
            return new T().ParseFromXml(element);
        }
    }
}
