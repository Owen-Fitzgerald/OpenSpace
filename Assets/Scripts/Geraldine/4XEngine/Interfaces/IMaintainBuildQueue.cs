using Geraldine._4XEngine.Contracts;
using System.Collections.Generic;

namespace Geraldine._4XEngine.Interfaces
{
    public interface IMaintainBuildQueue
    {
        /// <summary>
        /// The number of orders that can be simultaneously worked on
        /// </summary>
        public int SimultaneousOrders { get; set; }

        /// <summary>
        /// Queue of Build Orders waiting completion
        /// </summary>
        public Queue<I4XBuildOrder> BuildOrders { get; set; }

        /// <summary>
        /// Add Build Order To queue
        /// </summary>
        public abstract void AddBuildOrderToQueue(ProducableInfo NewOrder);

        /// <summary>
        /// Do production process every step/turn
        /// </summary>
        public abstract void DoProduction();

        /// <summary>
        /// Callback method for completed build order
        /// </summary>
        public abstract void OnBuildOrderCompleted(I4XBuildOrder CompletedOrder);

    }
}