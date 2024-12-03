using Geraldine._4XEngine.Localization;
using Geraldine.Standards.InfoSystem;
using Geraldine.Standards.Localization.Infos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using UnityEngine;

namespace Geraldine.Standards.Localization.Databases
{
    public class LocalizableTextInfoDatabase
    {
        public IDictionary<string, List<LocalizableTextInfo>> LoadedDatabase
        {
            get => m_LoadedDatabase;
        }

        private static LocalizableTextInfoDatabase S_Instance;
        public static LocalizableTextInfoDatabase Instance
        {
            get
            {
                if (S_Instance == null) { S_Instance = new LocalizableTextInfoDatabase(); }
                return S_Instance;
            }
            private set { S_Instance = value; }
        }

        public LocalizableTextInfoDatabase()
        {
            m_LoadedDatabase = new Dictionary<string, List<LocalizableTextInfo>>();
        }

        public static void LoadDatabases()
        {
            Instance.LoadedDatabase.Clear();
            foreach (string file in GeraldineStandardsSettings.LocalizationFiles)
            {
                LoadDatabase(file);
            }
        }

        public static void LoadDatabase(string FileName)
        {
            string fullPath = String.Format("{0}/{1}", GeraldineStandardsSettings.InfoResourcesFilePath, FileName);
            bool successullyRead = false;

            //If already cached for this event
            TextAsset file = Resources.Load<TextAsset>(fullPath);
            if (file != null)
            {
                try
                {
                    //Instance.LoadedDatabase.Add(FileName, FileSystem.DeserializeFile(file, typeof(List<LocalizableTextInfo>)) as List<LocalizableTextInfo>);
                    successullyRead = Instance.LoadedDatabase.Count() > 0;
                }
                catch
                {
                    successullyRead = false;
                }
            }

             //SaveDatabase(FileName);
        }

        public static void SaveDatabases()
        {
            foreach(KeyValuePair<string, List <LocalizableTextInfo>> kvp in Instance.LoadedDatabase)
            {
                SaveDatabase(kvp.Key);
            }
        }

        public static void SaveDatabase(string FileName)
        {
            string fullPath = String.Format("{0}/{1}.xml", Application.dataPath + "/Resources/" + GeraldineStandardsSettings.InfoResourcesFilePath, FileName);
            var serializer = new XmlSerializer(typeof(List<LocalizableTextInfo>));
            using (var writer = new StreamWriter(fullPath))
            {
                serializer.Serialize(writer, Instance.LoadedDatabase[FileName]);
            }
        }

        private IDictionary<string, List<LocalizableTextInfo>> m_LoadedDatabase;
    }
}
