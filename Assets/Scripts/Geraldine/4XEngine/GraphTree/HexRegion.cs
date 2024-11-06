using Geraldine._4XEngine.Units.Interfaces;
using System;
using System.IO;
using UnityEngine;

namespace Geraldine._4XEngine.GraphTree
{
    public class HexRegion : NodeGraph<HexNode>
    {

        #region Base Node Interactions

        public override void Occupy(IExistOnNodeGraph OccupyingUnit)
        {
            throw new NotImplementedException();
        }

        public override void Release(IExistOnNodeGraph ReleasingUnit)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Serialization

        public override void Save(BinaryWriter writer)
        {
            //Save Properties specific to this graph

            //Save children nodes
            for (int i = 0; i < nodes.Length; i++)
            {
                nodes[i].Save(writer);
            }
        }

        public override void Load(BinaryReader reader, int header)
        {
            //Load properties specific this this graph

            //load children nodes
        }

        #endregion
    }
}
