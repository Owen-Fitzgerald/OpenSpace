using Geraldine._4XEngine.Units.Interfaces;
using System;
using System.IO;

namespace Geraldine._4XEngine.GraphTree
{
    public class HexNode : Node
    {

        public override void Occupy(IExistOnNodeGraph OccupyingUnit)
        {
            throw new NotImplementedException();
        }

        public override void Release(IExistOnNodeGraph ReleasingUnit)
        {
            throw new NotImplementedException();
        }

        public override void Save(BinaryWriter writer)
        {
            throw new NotImplementedException();
        }
        public override void Load(BinaryReader reader, int header)
        {
            throw new NotImplementedException();
        }
    }
}
