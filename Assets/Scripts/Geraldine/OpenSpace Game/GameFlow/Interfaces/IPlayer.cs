using Geraldine._4XEngine.GraphTree.Interfaces;
using Geraldine._4XEngine.Util;
using System.Collections.Generic;

namespace Geraldine.OpenSpaceGame.Interfaces
{
    public interface IPlayer
    {
        public int ID { get; }
        public enPlayerController PlayerController { get; }
        //public IEmpire Empire { get; }
        public IList<object> OwnedUnits { get; } //DEFINE
        public IList<object> OwnedCities { get; } //DEFINE
        public IList<INode> DiscoveredTiles { get; set; }
        public IList<INode> VisibleTiles { get; set; }
        public IList<object> Modifiers { get; } //DEFINE

        public IDictionary<string, float> StoredResources { get; }
        public IDictionary<string, float> LastProducedResources { get; }

        public void MakeAIMovements();
        public void NextTurn();
    }
}
