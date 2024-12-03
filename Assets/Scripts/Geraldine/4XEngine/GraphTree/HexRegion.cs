using Geraldine._4XEngine.Contracts;
using Geraldine._4XEngine.Units.Interfaces;
using Geraldine.OpenSpaceGame;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using UnityEngine;

namespace Geraldine._4XEngine.GraphTree
{
    public class HexRegion : NodeGraph<HexNode>
    {

        public int RegionId { get; set; }  // Unique identifier
        public Vector3 Center { get; set; }  // Geographic center for calculations

        // Aggregate Data
        public Elevation Elevation { get; set; }
        public Temperature Temperature { get; set; }
        public Moisture Moisture { get; set; }
        public SoilQuality SoilQuality { get; set; }
        public Vegetation Vegetation { get; set; }

        public HexRegion(int regionId, List<HexNode> hexes)
        {
            RegionId = regionId;
            Define(hexes);
            Center = CalculateCenter(hexes);
            AggregateProperties();  // Calculate aggregate data from hexes
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

        private Vector3 CalculateCenter(List<HexNode> hexes)
        {
            Vector3 sum = Vector3.zero;
            foreach (var hex in hexes)
            {
                sum += hex.Position;
            }
            return sum / hexes.Count;
        }

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
