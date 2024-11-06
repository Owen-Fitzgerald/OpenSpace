using Geraldine.HexEngine.Localization;
using Geraldine.Standards.InfoSystem.Infos;
using Geraldine.Standards.InfoSystem.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using UnityEngine;

namespace Geraldine.Standards.InfoSystem.Databases
{
    public class InfoDatabase<T> : IDatabase<T>
       where T : BaseInfo
    {
        public List<T> LoadedDatabase
        {
            get => m_LoadedDatabase;
        }

        private static InfoDatabase<T> S_Instance;
        public static InfoDatabase<T> Instance
        {
            get
            {
                if (S_Instance == null) { S_Instance = new InfoDatabase<T>(); }
                return S_Instance;
            }
            private set { S_Instance = value; }
        }

        public InfoDatabase() : base()
        {
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
                    Instance = FileSystem.DeserializeFile(file, typeof(InfoDatabase<T>)) as InfoDatabase<T>;
                    successullyRead = Instance.LoadedDatabase.Count() > 0;
                }
                catch
                {
                    successullyRead = false;
                }
            }

            //SaveDatabase(FileName);
        }

        public static void SaveDatabase(string FileName)
        {
            string fullPath = String.Format("{0}/{1}.xml", Application.dataPath + "/Resources/" + GeraldineStandardsSettings.InfoResourcesFilePath, FileName);
            var serializer = new XmlSerializer(typeof(InfoDatabase<T>));
            using (var writer = new StreamWriter(fullPath))
            {
                serializer.Serialize(writer, Instance);
            }
        }

        protected List<T> m_LoadedDatabase;

        protected virtual void LoadDefaults()
        {

        }
    }
}
