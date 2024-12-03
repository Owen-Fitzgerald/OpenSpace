using System.Collections.Generic;
using System.IO;
using Geraldine._4XEngine.GraphTree;
using Geraldine._4XEngine.GraphTree.Interfaces;

namespace Geraldine.OpenSpaceGame.Interfaces
{
    public interface IGame
    {
        public Galaxy Galaxy { get; }
        public int CurrentPlayerID { get; set; }
        public int CurrentTurn { get; }
        public IList<IPlayer> Players { get; }


        public void NextTurn();
        public void Save(BinaryWriter Writer);
        public void Load(BinaryReader reader, int header);
    }
}
