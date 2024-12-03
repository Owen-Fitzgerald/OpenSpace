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

    /// <summary>
    /// Hexagonal direction, pointy side up, which is north.
    /// </summary>
    public enum HexDirection
    {
        /// <summary>Northeast.</summary>
        NE,
        /// <summary>East.</summary>
        E,
        /// <summary>Southeast.</summary>
        SE,
        /// <summary>Southwest.</summary>
        SW,
        /// <summary>West.</summary>
        W,
        /// <summary>Northwest.</summary>
        NW
    }
    public enum HexCornerDirection
    {
        N, NE, SE, S, SW, NW
    }


    /// <summary>
    /// Flags that describe the contents of a cell.
    /// </summary>
    [System.Flags]
    public enum HexFlags
    {
        Empty = 0,

        RoadNE = 0b000001,
        RoadE = 0b000010,
        RoadSE = 0b000100,
        RoadSW = 0b001000,
        RoadW = 0b010000,
        RoadNW = 0b100000,

        Roads = 0b111111,

        RiverNE = 0b000001_000000,
        RiverE = 0b000010_000000,
        RiverSE = 0b000100_000000,
        RiverSW = 0b001000_000000,
        RiverW = 0b010000_000000,
        RiverNW = 0b100000_000000,

        River = 0b111111_000000,

        Unused1 = 0b000001_000000_000000,
        Unused2 = 0b000010_000000_000000,
        Unused3 = 0b000100_000000_000000,
        Unused4 = 0b001000_000000_000000,
        Unused5 = 0b010000_000000_000000,
        Unused6 = 0b100000_000000_000000,

        Unused = 0b111111_000000_000000,

        Walled = 0b1_000000_000000_000000,

        Explored = 0b010_000000_000000_000000,
        Explorable = 0b100_000000_000000_000000
    }
}
