using System.Xml.Linq;

namespace Geraldine._4XEngine.Loaders.Interfaces
{
    public interface IDataLoader
    {
        // Method to load data from XML
        void LoadDataFromXml();

        // Optional methods to unload or reload data
        void UnloadData();
        void ReloadData();
    }

}
