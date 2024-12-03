using Geraldine._4XEngine.GraphTree.Interfaces;
using Geraldine._4XEngine.Units.Interfaces;
using System.IO;
using UnityEngine;

namespace Geraldine._4XEngine.Units
{
    public class Occupant : MonoBehaviour, IExistOnNodeGraph
    {
        public INodeGraph ParentGraph { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        public INode Location { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        public float Orientation { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        public void Kill()
        {
            throw new System.NotImplementedException();
        }

        public void Save(BinaryWriter writer)
        {
            throw new System.NotImplementedException();
        }

        public void ValidateLocation()
        {
            throw new System.NotImplementedException();
        }
    }
}
