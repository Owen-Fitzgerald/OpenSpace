using UnityEngine;

namespace Geraldine.OpenSpaceGame.GameFlow
{
    public class BuildingJob
    {
        public BuildingJob(object Blueprint, //DEFINE
            float overflowedProduction,
            ProductionCompleteDelegate OnProductionComplete,
            ProductionBonusDelegate ProductionBonusFunc
        )
        {
            if (OnProductionComplete == null)
                throw new UnityException();

            //this.BuildingJobIcon = BuildingJobIcon;
            //this.BuildingJobName = Blueprint.Name;
            //this.totalProductionNeeded = Blueprint.ProductionRequired;
            productionDone = overflowedProduction;
            this.OnProductionComplete = OnProductionComplete;
            this.ProductionBonusFunc = ProductionBonusFunc;

            this.Blueprint = Blueprint;
        }

        public object Blueprint;

        public float totalProductionNeeded;
        public float productionDone;

        //public Image BuildingJobIcon;   // Ex: Image for the Petra
        public string BuildingJobName;  // Ex:  "Petra"

        public delegate void ProductionCompleteDelegate();
        public event ProductionCompleteDelegate OnProductionComplete;

        public delegate float ProductionBonusDelegate();
        public ProductionBonusDelegate ProductionBonusFunc;

        /// <summary>
        /// Dos the work.
        /// </summary>
        /// <returns>Number of hammers remaining, or negative is complete/overflow</returns>
        /// <param name="rawProduction">Raw production.</param>
        public float DoWork(float rawProduction)
        {
            if (ProductionBonusFunc != null)
            {
                rawProduction *= ProductionBonusFunc();
            }

            productionDone += rawProduction;

            if (productionDone >= totalProductionNeeded)
            {
                OnProductionComplete();
            }

            return totalProductionNeeded - productionDone;
        }

    }
}
