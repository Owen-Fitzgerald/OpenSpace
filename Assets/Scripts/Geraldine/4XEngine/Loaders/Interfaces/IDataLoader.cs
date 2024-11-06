using Geraldine._4XEngine.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geraldine._4XEngine.Loaders.Interfaces
{
    public interface IDataLoader
    {
        //Create Sql Table
        void CreateTable();

        // Method to load data from XML
        void LoadDataFromXml();

        // Method to save data into SQL
        void SaveDataToSql();

        // Optional methods to unload or reload data
        void UnloadData();
        void ReloadData();
    }

}
