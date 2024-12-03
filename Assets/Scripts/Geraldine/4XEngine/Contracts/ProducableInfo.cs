using Geraldine.Standards.InfoSystem.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Geraldine._4XEngine.Contracts
{
    public class ProducableInfo : ContractBase<ProducableInfo>
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

        public override ProducableInfo ParseFromXml(XElement element)
        {
            throw new NotImplementedException();
        }
    }
}
