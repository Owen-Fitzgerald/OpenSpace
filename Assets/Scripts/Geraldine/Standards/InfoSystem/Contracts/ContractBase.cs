using System.Xml.Linq;

namespace Geraldine.Standards.InfoSystem.Contracts
{
    public abstract class ContractBase<T>
        where T : ContractBase<T>
    {
        public string UniqueId { get => m_UniqueID; set => m_UniqueID = value; }
        public string Name { get => m_Name; set => m_Name = value; }

        public ContractBase()
        {
        }

        public ContractBase(string UniqueID, string Name)
        {
            this.UniqueId = UniqueID;
            this.Name = Name;
        }

        public abstract T ParseFromXml(XElement element);

        private string m_UniqueID;
        private string m_Name;
    }
}
