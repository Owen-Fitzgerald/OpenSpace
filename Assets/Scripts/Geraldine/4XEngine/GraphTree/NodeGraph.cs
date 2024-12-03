using Geraldine._4XEngine.GraphTree.Interfaces;
using Geraldine._4XEngine.Units.Interfaces;
using Geraldine.OpenSpaceGame;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;

namespace Geraldine._4XEngine.GraphTree
{
    public class NodeGraph<T> : Node, INodeGraph<T>
        where T : INode
    {
        public IList<T> Nodes => nodes;

        public NodeSearchData[] SearchData => throw new NotImplementedException();

        public virtual void Define(IList<T> nodes)
        {
            Nodes.Clear();
            foreach (var node in nodes) { AddChildNode(node); }
        }

        public virtual void AddChildNode(T child)
        {
            child.ParentGraph = this;
            Nodes.Add(child);
        }

        public virtual void AddChildNodes(IList<T> children)
        {
            foreach (var child in children)
            {
                child.ParentGraph = this;
                Nodes.Add(child);
            }
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

        #region Get Node Accesses
        public virtual bool TryGetNode(int nodeIndex, out T node)
        {
            if (nodeIndex < 0 || nodeIndex > nodes.Count - 1)
            {
                node = default(T);
                return false;
            }
            node = nodes[nodeIndex];
            return true;
        }


        public virtual T GetNode(int nodeIndex) => Nodes[nodeIndex];

        public virtual T GetNode(Ray ray)
        {
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                return GetNode(hit.point);
            }
            return default(T);
        }

        public virtual T GetNode(Vector3 position)
        {
            //position = transform.InverseTransformPoint(position);
            //HexCoordinates coordinates = HexCoordinates.FromPosition(position);
            //return GetNode(coordinates);
            throw new NotImplementedException();
        }
        #endregion

        #region Generic Inheritance

        INode INodeGraph.GetNode(int index) => GetNode(index);

        INode INodeGraph.GetNode(Ray ray) => GetNode(ray);

        INode INodeGraph.GetNode(Vector3 position) => GetNode(position);

        public bool TryGetNode(int nodeIndex, out INode node) => TryGetNode(nodeIndex, out node);
        #endregion

        #region Serialization

        public override void Save(BinaryWriter writer)
        {
            //Save Properties specific to this graph

            //Save children nodes
            for (int i = 0; i < nodes.Count; i++)
            {
                nodes[i].Save(writer);
            }
        }

        public override void Load(BinaryReader reader, int header)
        {
            //Load properties specific this this graph
            GameFileManager.CurrentLoadState = enLoadState.Day5;

            //load children nodes
        }

        #endregion

        public bool Search(INode fromNode, INode toNode)
        {
            throw new NotImplementedException();
        }


        /// Protected

        protected IList<T> nodes = new List<T>();
    }
}
