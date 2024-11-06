using Geraldine._4XEngine.Contracts;

namespace Geraldine._4XEngine.Interfaces
{
    public interface I4XBuildOrder
    {

        /// <summary>
        /// Info loaded from xml that this order is producing
        /// </summary>
        public ProducableInfo Blueprint { get; set; }

        /// <summary>
        /// IMaintainBuildQueue instance which owns this build order
        /// </summary>
        public IMaintainBuildQueue QueueOwner { get; set; }

        /// <summary>
        /// Total production value, regardless of what resource that value is counting
        /// </summary>
        public float TotalProductionRequired { get; set; }

        /// <summary>
        /// proiduction finished thus far
        /// </summary>
        public float ProductionDone { get; set; }

        /// <summary>
        /// Name string of the build order, generally grabbed from the blueprint being produced
        /// </summary>
        public string BuildingJobName { get; }

        /// <summary>
        /// Icon path string of the build order, generally grabbed from the blueprint being produced
        /// </summary>
        public string BuildingJobIcon { get; }

        /// <summary>
        /// Callback delegate upon production completion
        /// </summary>
        public delegate void ProductionCompleteDelegate();
        public event ProductionCompleteDelegate OnProductionComplete;
    }
}