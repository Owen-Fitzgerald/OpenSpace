using Geraldine._4XEngine.GraphTree.Interfaces;
using Geraldine._4XEngine.Units.Interfaces;
using Geraldine.OpenSpaceGame;
using System;
using System.IO;
using UnityEngine;

namespace Geraldine._4XEngine.GraphTree
{
    public class StarSystem : NodeGraph<OrbitalBody>
    {
        public Vector3 Location;
        public StarSystem(Vector3 location)
        {
            Location = location;
        }
        public StarSystem()
        {

        }

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
            writer.Write(Nodes.Count);
            for (int i = 0; i < Nodes.Count; i++)
            {
                Nodes[i].Save(writer);
            }
        }

        public override void Load(BinaryReader reader, int header)
        {
            //Load properties specific this this graph
            GameFileManager.CurrentLoadState = enLoadState.Day3;

            //load children nodes
            int nodeCount = reader.ReadInt32();
            for (int i = 0; i < nodeCount; i++)
            {
                nodes[i] = new OrbitalBody()
                { ParentGraph = this };
                nodes[i].Load(reader, header);
            }
        }

        #endregion
    }
}
