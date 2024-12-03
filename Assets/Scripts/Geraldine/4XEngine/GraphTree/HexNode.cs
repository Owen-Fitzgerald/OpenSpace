using Geraldine._4XEngine.Units.Interfaces;
using Geraldine._4XEngine.Util;
using Geraldine.OpenSpaceGame;
using System;
using System.IO;
using Geraldine._4XEngine.Contracts;
using Geraldine._4XEngine.Loaders;
using System.Linq;

namespace Geraldine._4XEngine.GraphTree
{
    public class HexNode : Node
    {
        /// <summary>
        /// Unique global index of the cell.
        /// </summary>
        public int Index
        { get; set; }

        public override int Id => throw new NotImplementedException();

        public override string Name => Index.ToString();

        public Elevation Elevation => ElevationLoader.GetContractByIndex(values.Elevation);

        public Temperature Temperature => TemperatureLoader.GetContractByIndex(values.Temperature);

        public Moisture Moisture => MoistureLoader.GetContractByIndex(values.Moisture);

        public SoilQuality SoilQuality => SoilQualityLoader.GetContractByIndex(values.SoilQuality);

        public Vegetation Vegetation => VegetationLoader.GetContractByIndex(values.Vegetation);


        #region Base Node Interaction
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
            throw new NotImplementedException();
        }
        public override void Load(BinaryReader reader, int header)
        {
            //Load properties specific this this graph
            GameFileManager.CurrentLoadState = enLoadState.Day6;

            //load base node
            base.Load(reader, header);

        }
        #endregion


        //Protected
        protected HexFlags flags;

        protected HexValues values;
    }
}
