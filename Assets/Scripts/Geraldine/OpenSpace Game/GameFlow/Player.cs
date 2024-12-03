using System;
using System.Collections.Generic;
using System.Linq;
using Geraldine.OpenSpaceGame.Interfaces;
using Geraldine._4XEngine.Util;
using Geraldine._4XEngine.GraphTree.Interfaces;

namespace Geraldine.OpenSpaceGame.GameFlow
{
    public class Player : IPlayer
    {
        public int ID { get => m_ID; private set => m_ID = value; }
        public enPlayerController PlayerController { get => m_PlayerController; private set => m_PlayerController = value; }
       // public IEmpire Empire { get => m_Empire; private set => m_Empire = value; }
        public IList<object> OwnedUnits { get => m_OwnedUnits; } //DEFINE
        public IList<object> OwnedCities { get => m_OwnedCities; } //DEFINE
        public IList<INode> DiscoveredTiles { get => m_DiscoveredTiles; set => m_DiscoveredTiles = value; }
        public IList<INode> VisibleTiles { get => m_VisibleTiles; set => m_VisibleTiles = value; }
        public IList<object> Modifiers { get => m_Modifiers; } //DEFINE
        public IDictionary<string, float> StoredResources { get => m_StoredResources; }
        public IDictionary<string, float> LastProducedResources { get => m_LastProducedResources; }


        public Player(int ID, enPlayerController Controller)
        {
            this.ID = ID;
            PlayerController = Controller;

#if UNITY_EDITOR
            LoadSampleEmpire();
#endif
        }

        public void MakeAIMovements()
        {

        }

        private int m_ID;
        private enPlayerController m_PlayerController;
        //private IEmpire m_Empire;
        private IList<object> m_OwnedUnits = new List<object>(); //DEFINE
        private IList<object> m_OwnedCities = new List<object>(); //DEFINE
        private IList<INode> m_DiscoveredTiles = new List<INode>();
        private IList<INode> m_VisibleTiles = new List<INode>();
        private IList<object> m_Modifiers = new List<object>(); //DEFINE
        private IDictionary<string, float> m_StoredResources = new Dictionary<string, float>();
        private IDictionary<string, float> m_LastProducedResources = new Dictionary<string, float>();

        private void LoadSampleEmpire()
        {
            //INamesList sampleNames = new NamesList();
            //sampleNames.CityNames.Add("Sample City");
            //sampleNames.LeadersNames.Add("Sample Leader");

            //m_Empire = new Empire()
            //{
            //    Name = "Sample Empire",
            //    Adjective = "Samplonian",
            //    Color = "#0f1b54",
            //    NamesList = sampleNames,
            //    BiomePreference = "Temperate"
            //};
        }

        public void NextTurn()
        {
            throw new NotImplementedException();
        }
    }
}
