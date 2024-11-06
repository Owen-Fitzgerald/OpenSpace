using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geraldine._4XEngine.Util
{
    //Enum for empire controller
    public enum enPlayerController
    {
        Local = 0,
        AI = 1,
        Remote = 2
    }


    //Enum for Galaxy Age, which could affect system generation parameters like star types
    public enum enGalaxyAge
    {
        Young,
        Mature,
        Ancient
    }

    //Enum for Galaxy Size, which could affect system generation parameters like number and radius of stars and empires
    public enum enGalaxySize
    {
        Tiny,
        Small,
        Medium,
        Large,
        Epic
    }
}
