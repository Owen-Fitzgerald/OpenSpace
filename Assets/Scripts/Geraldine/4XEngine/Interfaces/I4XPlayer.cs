using Geraldine._4XEngine.GraphTree.Interfaces;
using Geraldine._4XEngine.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Geraldine._4XEngine.Interfaces
{
    public interface I4XPlayer
    {
        /// <summary>
        /// Player ID
        /// </summary>
        public int ID { get; }

        /// <summary>
        /// enum for if this player is controlled by local, AI, or remote
        /// </summary>
        public enPlayerController PlayerController { get; }

        /// <summary>
        /// List of owned control points, ie cities, planets
        /// </summary>
        public IList<I4XControlPoint> OwnedControlPoints { get; }

        /// <summary>
        /// List of owned units
        /// </summary>
        public IList<I4XUnit> OwnedUnits { get; }

        /// <summary>
        /// List of discovered nodes
        /// </summary>
        public IList<INode> DiscoveredNodes { get; set; }

        /// <summary>
        /// List of actively visible nodes
        /// </summary>
        public IList<INode> VisibleNodes { get; set; }

    }
}