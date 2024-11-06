using Geraldine._4XEngine.Interfaces;
using Geraldine._4XEngine.GraphTree.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geraldine._4XEngine.GraphTree
{
    /// <summary>
    /// Priority queue to store hex nodes for the pathfinding algorithm.
    /// </summary>
    public class NodePriorityQueue<T, U> 
        where T : INodeGraph<U>
        where U : INode
    {
        readonly List<int> list = new();
        readonly T grid;

        public NodePriorityQueue(T grid) => this.grid = grid;

        int minimum = int.MaxValue;

        /// <summary>
        /// Add a node to the queue.
        /// </summary>
        /// <param name="node">Node to add.</param>
        public void Enqueue(int nodeIndex)
        {
            int priority = grid.SearchData[nodeIndex].SearchPriority;
            if (priority < minimum)
            {
                minimum = priority;
            }
            while (priority >= list.Count)
            {
                list.Add(-1);
            }
            grid.SearchData[nodeIndex].nextWithSamePriority = list[priority];
            list[priority] = nodeIndex;
        }

        /// <summary>
        /// Remove a node from the queue.
        /// </summary>
        /// <returns>The node with the highest priority.</returns>
        public bool TryDequeue(out int nodeIndex)
        {
            //count -= 1;
            for (; minimum < list.Count; minimum++)
            {
                nodeIndex = list[minimum];
                if (nodeIndex >= 0)
                {
                    list[minimum] = grid.SearchData[nodeIndex].nextWithSamePriority;
                    return true;
                }
            }
            nodeIndex = -1;
            return false;
        }

        /// <summary>
        /// Apply the current priority of a node that was previously enqueued.
        /// </summary>
        /// <param name="node">Node to update</param>
        /// <param name="oldPriority">Node priority before it was changed.</param>
        public void Change(int nodeIndex, int oldPriority)
        {
            int current = list[oldPriority];
            int next = grid.SearchData[current].nextWithSamePriority;
            if (current == nodeIndex)
            {
                list[oldPriority] = next;
            }
            else
            {
                while (next != nodeIndex)
                {
                    current = next;
                    next = grid.SearchData[current].nextWithSamePriority;
                }
                grid.SearchData[current].nextWithSamePriority =
                    grid.SearchData[nodeIndex].nextWithSamePriority;
            }
            Enqueue(nodeIndex);
        }

        /// <summary>
        /// Clear the queue.
        /// </summary>
        public void Clear()
        {
            list.Clear();
            minimum = int.MaxValue;
        }
    }
}
