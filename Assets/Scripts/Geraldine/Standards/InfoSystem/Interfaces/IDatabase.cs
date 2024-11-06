using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geraldine.Standards.InfoSystem.Interfaces
{
    public interface IDatabase<T>
    {
        public List<T> LoadedDatabase { get; }

        public void LoadDatabase(string FileName) { }
        public void SaveDatabase(string FileName) { }
    }
}
