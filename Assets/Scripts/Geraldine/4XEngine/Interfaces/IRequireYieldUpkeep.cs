using Geraldine._4XEngine.Contracts;
using System.Collections.Generic;

namespace Geraldine._4XEngine.Interfaces
{
    public interface IRequireYieldUpkeep
    {
        /// <summary>
        /// Add Build Order To queue
        /// </summary>
        public abstract bool Maintain();

    }
}