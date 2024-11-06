using Geraldine._4XEngine.GraphTree.Interfaces;
using Geraldine._4XEngine.Units.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Geraldine._4XEngine.Units
{
    public class Unit : Occupant, IMoveOnNodeGraph
    {
        public int MovementSpeed { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int MovementUsed { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public int GetMoveCost(INode fromNode, INode toNode)
        {
            throw new NotImplementedException();
        }

        public bool IsValidDestination(INode node)
        {
            throw new NotImplementedException();
        }

        public IEnumerator LookAt(Vector3 point)
        {
            throw new NotImplementedException();
        }

        public void Travel(List<int> path)
        {
            throw new NotImplementedException();
        }

        public IEnumerator TravelPath()
        {
            throw new NotImplementedException();
        }
    }
}
