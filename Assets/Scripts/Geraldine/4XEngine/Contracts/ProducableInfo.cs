using Geraldine.Standards.InfoSystem.Infos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geraldine._4XEngine.Contracts
{
    public class ProducableInfo : BaseInfo
    {
        public ProducableInfo() :
            base()
        {

        }

        public ProducableInfo(string InfoID, string Name) :
            base(InfoID, Name)
        {

        }

        public int ProductionRequired = -1;
    }
}
