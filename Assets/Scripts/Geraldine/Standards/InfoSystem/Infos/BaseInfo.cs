namespace Geraldine.Standards.InfoSystem.Infos
{
    public class BaseInfo
    {
        public string UniqueID { get => m_UniqueID; set => m_UniqueID = value; }
        public string Name { get => m_Name; set => m_Name = value; }

        public BaseInfo()
        {
        }

        public BaseInfo(string UniqueID, string Name)
        {
            this.UniqueID = UniqueID;
            this.Name = Name;
        }

        private string m_UniqueID;
        private string m_Name;
    }
}
