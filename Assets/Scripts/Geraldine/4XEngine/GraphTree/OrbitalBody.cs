using Geraldine._4XEngine.GraphTree.Interfaces;
using Geraldine._4XEngine.Units;
using Geraldine._4XEngine.Units.Interfaces;
using System;
using System.IO;
using UnityEngine;

namespace Geraldine._4XEngine.GraphTree
{
    public class OrbitalBody : NodeGraph<OrbitalBody>
    {
        public string Name { get; set; }
        public string PlanetClass { get; set; }
        public int Size { get; set; }
        public bool IsMoon { get; set; }

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
            base.Save(writer);
            writer.Write(Name);

            //Save children nodes
            writer.Write(Nodes.Length);
            for (int i = 0; i < Nodes.Length; i++)
            {
                Nodes[i].Save(writer);
            }

            //Save occupants
            writer.Write(Occupants.Count);
            for (int i = 0; i < Occupants.Count; i++)
            {
                Occupants[i].Save(writer);
            }
        }

        public override void Load(BinaryReader reader, int header)
        {
            //Load properties specific this this graph
            base.Load(reader, header);
            Name = reader.ReadString();

            //load children nodes
            int nodeCount = reader.ReadInt32();
            for (int i = 0; i < nodeCount; i++)
            {
                nodes[i] = new OrbitalBody()
                { ParentGraph = (INodeGraph<INode>)this };
                nodes[i].Load(reader, header);
            }

            int unitCount = reader.ReadInt32();
            for (int i = 0; i < unitCount; i++)
            {
                //HexOccupant.Load(reader, this);
            }
        }

        #endregion
    }
}
