using Geraldine._4XEngine.GraphTree.Interfaces;
using Geraldine._4XEngine.Units.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Geraldine._4XEngine.GraphTree
{
    public abstract class Node : MonoBehaviour, INode
    {

        //Serializable Properties
        public virtual int Id { get; private set; }
        public virtual string Name { get; set; }

        public Vector3 Position { get; set; }

        //Gameplay Properties

        public INodeGraph ParentGraph { get; set; }

        public IList<IExistOnNodeGraph> Occupants { get; private set; }

        //Methods for Interaction

        public abstract void Occupy(IExistOnNodeGraph OccupyingUnit);

        public abstract void Release(IExistOnNodeGraph ReleasingUnit);

        //Methods for serialization

        public virtual void Save(BinaryWriter writer)
        {
            writer.Write(Id);
            writer.Write(Position.x);
            writer.Write(Position.y);
            writer.Write(Position.z);
        }

        public virtual void Load(BinaryReader reader, int header)
        {
            Id = reader.ReadInt32();
            float x = reader.ReadSingle();
            float y = reader.ReadSingle();
            float z = reader.ReadSingle();
            Position = new Vector3(x, y, z);
        }
    }
}
