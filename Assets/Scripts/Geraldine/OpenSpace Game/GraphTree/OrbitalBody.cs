using Geraldine._4XEngine.Contracts;
using Geraldine._4XEngine.Units.Interfaces;
using Geraldine.OpenSpaceGame;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace Geraldine._4XEngine.GraphTree
{
    public class OrbitalBody : NodeGraph<OrbitalBody>
    {
        public NodeGraph<HexNode> surfaceGraph;  // The hex-based surface graph.

        public override string Name { get; set; }
        public string PlanetClass { get; set; }
        public int Size { get; set; } // Radius
        public bool IsMoon { get; set; }

        public int OrbitDistance { get; set; } // Distance from the central star or planet (for moons).

        // Aggregate Data
        public Elevation Elevation { get; set; }
        public Temperature Temperature { get; set; }
        public Moisture Moisture { get; set; }
        public SoilQuality SoilQuality { get; set; }
        public Vegetation Vegetation { get; set; }

        public OrbitalBody()
        {
            surfaceGraph = new NodeGraph<HexNode>();
        }

        #region Hex Accessors
        public HexNode GetHexByIndex(int index)
        {
            // Ensure the index is within the valid range
            if (index < 0 || index >= surfaceGraph.Nodes.Count)
            {
                Debug.LogError($"Invalid hex index: {index}. Valid range is 0 to {surfaceGraph.Nodes.Count - 1}.");
                return null;
            }

            // Retrieve the hex at the specified index
            return surfaceGraph.Nodes[index];
        }

        public int GetClosestHexIndex(Vector3 position)
        {
            float minDistance = float.MaxValue;
            int closestIndex = -1;

            for (int i = 0; i < surfaceGraph.Nodes.Count; i++)
            {
                float distance = Vector3.Distance(position, surfaceGraph.Nodes[i].Position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestIndex = i;
                }
            }

            return closestIndex;
        }

        public List<HexNode> GetNeighbors(HexNode hex)
        {
            List<HexNode> neighbors = new List<HexNode>();

            // Directions: N, NE, SE, S, SW, NW (0 to 5)
            for (int dir = 0; dir < 6; dir++)
            {
                int neighborIndex = GetNeighborIndex(hex.Index, dir);
                if (neighborIndex >= 0 && neighborIndex < surfaceGraph.Nodes.Count)
                {
                    neighbors.Add(surfaceGraph.Nodes[neighborIndex]);
                }
            }

            return neighbors;
        }

        private int GetNeighborIndex(int currentIndex, int direction)
        {
            int totalHexes = surfaceGraph.Nodes.Count;
            int numRings = Mathf.FloorToInt(Mathf.Sqrt(totalHexes));  // Total vertical rings on the sphere

            int ring = currentIndex / numRings;  // Determine which ring the hex is in
            int positionInRing = currentIndex % numRings;

            // Define directions: North, NE, SE, South, SW, NW
            int[] dirOffsets = { -numRings, -numRings + 1, 1, numRings, numRings - 1, -1 };

            int neighborIndex = currentIndex + dirOffsets[direction];

            // Handle wrap-around at poles and edges
            if (ring == 0 || ring == numRings - 1)
            {
                // Poles: fewer neighbors, loop around to adjacent rings
                neighborIndex = (neighborIndex + totalHexes) % totalHexes;
            }
            else if (positionInRing == 0 && direction == 5)
            {
                // Wrap-around for leftmost neighbor (NW) on equator
                neighborIndex = currentIndex + numRings - 1;
            }
            else if (positionInRing == numRings - 1 && direction == 2)
            {
                // Wrap-around for rightmost neighbor (SE) on equator
                neighborIndex = currentIndex - numRings + 1;
            }

            // Validate neighbor index
            if (neighborIndex < 0 || neighborIndex >= totalHexes)
                return -1;  // Invalid neighbor

            return neighborIndex;
        }
        #endregion

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

        #region Serialization

        public override void Save(BinaryWriter writer)
        {
            //Save Properties specific to this graph
            base.Save(writer);
            writer.Write(Name);

            //Save children nodes
            writer.Write(Nodes.Count);
            for (int i = 0; i < Nodes.Count; i++)
            {
                Nodes[i].Save(writer);
            }

            //Save occupants
            writer.Write(Occupants.Count);
            for (int i = 0; i < Occupants.Count; i++)
            {
                Occupants[i].Save(writer);
            }
        }

        public override void Load(BinaryReader reader, int header)
        {
            //Load properties specific this this graph
            GameFileManager.CurrentLoadState = enLoadState.Day4;
            base.Load(reader, header);
            Name = reader.ReadString();

            //load children nodes
            int nodeCount = reader.ReadInt32();
            for (int i = 0; i < nodeCount; i++)
            {
                nodes[i] = new OrbitalBody()
                { ParentGraph = this };
                nodes[i].Load(reader, header);
            }

            int unitCount = reader.ReadInt32();
            for (int i = 0; i < unitCount; i++)
            {
                //HexOccupant.Load(reader, this);
            }
        }

        #endregion

        private void AggregateProperties()
        {
            // Example: Majority rule for elevation, or weighted averages
            Elevation = Nodes.GroupBy(h => h.Elevation)
                             .OrderByDescending(g => g.Count())
                             .First().Key;
            Temperature = Nodes.GroupBy(h => h.Temperature)
                             .OrderByDescending(g => g.Count())
                             .First().Key;
            Moisture = Nodes.GroupBy(h => h.Moisture)
                             .OrderByDescending(g => g.Count())
                             .First().Key;
            SoilQuality = Nodes.GroupBy(h => h.SoilQuality)
                             .OrderByDescending(g => g.Count())
                             .First().Key;
            Vegetation = Nodes.GroupBy(h => h.Vegetation)
                             .OrderByDescending(g => g.Count())
                             .First().Key;
            // Apply similar logic for other properties
        }
    }
}
