using System.Collections.Generic;
using UnityEngine;

namespace Geraldine._4XEngine.GraphTree.Interfaces
{
    public interface INodeGraph<T> : INode, INodeGraph
        where T : INode
    {
        public IList<T> Nodes { get; }

        /// <summary>
        /// Define and entirely new set of nodes in the tree
        /// </summary>
        /// <param name="nodes"></param>
        public abstract void Define(IList<T> nodes);

        /// <inheritdoc/>
		public new abstract T GetNode(Ray ray);

        /// <inheritdoc/>
        public new abstract T GetNode(Vector3 position);

        /// <inheritdoc/>
        public new abstract T GetNode(int nodeIndex);
    }

    public interface INodeGraph
    {
        public NodeSearchData[] SearchData { get; }

        /// <summary>
		/// Get a node given a <see cref="Ray"/>.
		/// </summary>
		/// <param name="ray"><see cref="Ray"/> used to perform a raycast.</param>
		/// <returns>The hit node, if any.</returns>
		public abstract INode GetNode(Ray ray);

        /// <summary>
        /// Get the node that contains a position.
        /// </summary>
        /// <param name="position">Position to check.</param>
        /// <returns>The node containing the position, if it exists.</returns>
        public abstract INode GetNode(Vector3 position);

        /// <summary>
        /// Get the node with a specific index.
        /// </summary>
        /// <param name="nodeIndex">Node index, which should be valid.</param>
        /// <returns>The indicated node.</returns>
        public abstract INode GetNode(int nodeIndex);


        /// <summary>
        /// Try to get the node with specific <see cref="HexCoordinates"/>.
        /// </summary>
        /// <param name="coordinates"><see cref="HexCoordinates"/>
        /// of the node.</param>
        /// <param name="node">The node, if it exists.</param>
        /// <returns>Whether the node exists.</returns>
        public abstract bool TryGetNode(int nodeIndex, out INode node);

        //A* Search Pattern
        public abstract bool Search(INode fromNode, INode toNode);
    }
}
