using Geraldine._4XEngine.GraphTree.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Geraldine._4XEngine.Units.Interfaces
{
    public interface IMoveOnNodeGraph : IExistOnNodeGraph
    {
        /// <summary>
        /// Speed of the unit, in nodes per turn or feet per second depending on implementation
        /// </summary>
        public int MovementSpeed { get; set; }

        /// <summary>
        /// Speed of the unit, in nodes per turn or feet per second depending on implementation
        /// </summary>
        public int MovementUsed { get; set; }

        public bool IsValidDestination(INode node);
        public void Travel(List<int> path);
        public IEnumerator TravelPath();
        public IEnumerator LookAt(Vector3 point);
        public int GetMoveCost(INode fromNode, INode toNode);
    }
}
