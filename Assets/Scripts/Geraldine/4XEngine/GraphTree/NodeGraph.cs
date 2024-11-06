using Geraldine._4XEngine.GraphTree.Interfaces;
using Geraldine._4XEngine.Units.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Geraldine._4XEngine.GraphTree
{
    public abstract class NodeGraph<T> : Node, INodeGraph<T>
        where T : class, INode
    {
        public List<T> childNodes = new List<T>();

        public T[] Nodes => nodes;

        public NodeSearchData[] SearchData => throw new NotImplementedException();

        public virtual void AddChildNode(T child)
        {
            child.ParentGraph = (INodeGraph<INode>)this;
            childNodes.Add(child);
        }

        public virtual void RemoveChildNode(T child)
        {
            if (childNodes.Contains(child))
            {
                child.ParentGraph = null;
                childNodes.Remove(child);
            }
        }

        #region Get Node Accesses
        public bool TryGetNode(int nodeIndex, out T node)
        {
            if (nodeIndex < 0 || nodeIndex > nodes.Length - 1)
            {
                node = null;
                return false;
            }
            node = nodes[nodeIndex];
            return true;
        }


        public T GetNode(int nodeIndex) => Nodes[nodeIndex];

        public T GetNode(Ray ray)
        {
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                return GetNode(hit.point);
            }
            return null;
        }

        public T GetNode(Vector3 position)
        {
            //position = transform.InverseTransformPoint(position);
            //HexCoordinates coordinates = HexCoordinates.FromPosition(position);
            //return GetNode(coordinates);
            throw new NotImplementedException();
        }
        #endregion

        public bool Search(T fromNode, T toNode)
        {
            throw new NotImplementedException();
        }

        /// Protected

        protected T[] nodes;
    }
}
