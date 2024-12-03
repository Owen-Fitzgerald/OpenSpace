using Geraldine._4XEngine.Units.Interfaces;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Geraldine._4XEngine.GraphTree.Interfaces
{
    public interface INode
    {
        /// <summary>
        /// Serialiable Id of the node
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// Text name of the node
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Parent Node Graph which contains this node
        /// </summary>
        public INodeGraph ParentGraph { get; set; }

        /// <summary>
        /// Local position of this node.
        /// </summary>
        public Vector3 Position { get; set; }

        /// <summary>
        /// I4XUnits which currently occupy this node
        /// </summary>
        public IList<IExistOnNodeGraph> Occupants { get; }

        /// <summary>
        /// Occupy Node with this unit
        /// </summary>
        /// <param name="OccupyingUnits"></param>
        public abstract void Occupy(IExistOnNodeGraph OccupyingUnit);

        /// <summary>
        /// Release Node with this unit
        /// </summary>
        /// <param name="ReleasingUnit"></param>
        public abstract void Release(IExistOnNodeGraph ReleasingUnit);

        /// <summary>
        /// Save the node data.
        /// </summary>
        /// <param name="writer"><see cref="BinaryWriter"/> to use.</param>
        public void Save(BinaryWriter writer);

        /// <summary>
        /// Load the node data.
        /// </summary>
        /// <param name="reader"><see cref="BinaryReader"/> to use.</param>
        /// <param name="header">Header version.</param>
        public void Load(BinaryReader reader, int header);

    }
}