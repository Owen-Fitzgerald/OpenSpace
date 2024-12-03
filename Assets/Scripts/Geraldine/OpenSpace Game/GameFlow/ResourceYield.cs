using Geraldine.OpenSpaceGame.Interfaces;

namespace Geraldine.OpenSpaceGame.GameFlow
{
    public class ResourceYield : IResourceYield
    {
        public string Name { get => m_Name; set => m_Name = value; }
        public float BaseYield { get => m_BaseYield; set => m_BaseYield = value; }

        public ResourceYield(string Name, float BaseYield)
        {
            this.Name = Name;
            this.BaseYield = BaseYield;
        }

        private string m_Name;
        private float m_BaseYield;
    }
}
