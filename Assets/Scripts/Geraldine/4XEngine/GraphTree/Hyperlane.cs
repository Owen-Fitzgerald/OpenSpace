using System;
using UnityEngine;

namespace Geraldine._4XEngine.GraphTree
{
    public class Hyperlane
    {
        // Unique ID for each hyperlane
        public string Id { get; private set; }

        // Starting system of the hyperlane
        public StarSystem StartSystem { get; private set; }

        // Ending system of the hyperlane
        public StarSystem EndSystem { get; private set; }

        // Distance between the two systems
        public float Distance { get; private set; }

        // Determines if the hyperlane can be traveled in both directions
        public bool IsBidirectional { get; set; }

        public Hyperlane(StarSystem startSystem, StarSystem endSystem, bool isBidirectional = true)
        {
            Id = Guid.NewGuid().ToString();
            StartSystem = startSystem;
            EndSystem = endSystem;
            Distance = CalculateDistance(startSystem, endSystem);
            IsBidirectional = isBidirectional;
        }

        private float CalculateDistance(StarSystem start, StarSystem end)
        {
            // Assume StartSystem and EndSystem have positions as Vector3 or similar
            return Vector3.Distance(start.Position, end.Position);
        }
    }
}
