﻿using System;
using System.Collections.Generic;

namespace Geraldine._4XEngine.Initializers
{
    /// <summary>
    /// Represents a planet initilizer, which can either be a standalone planet or a moon.
    /// This class defines various properties such as the planet's size, orbit details, and associated moons.
    /// </summary>
    public class PlanetInitializer
    {
        /// <summary>
        /// Unique identifier for the planet instance, used for distinguishing between different planets.
        /// </summary>
        public string UniqueId { get; set; }

        /// <summary>
        /// Reference to the entity or model used for rendering the planet in the game engine.
        /// </summary>
        public string Entity { get; set; }

        /// <summary>
        /// Display name of the planet that will appear in the UI.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Class or type of the planet (e.g., barren, gas giant, continental, etc.).
        /// </summary>
        public string PlanetClass { get; set; }

        /// <summary>
        /// List of moons that orbit this planet.
        /// Each moon is itself a <see cref="PlanetInitializer"/> instance.
        /// </summary>
        public List<PlanetInitializer> Moons { get; set; }

        /// <summary>
        /// Distance from the central star or planet (for moons).
        /// </summary>
        public int OrbitDistance { get; set; }

        /// <summary>
        /// The angle range for orbiting this planet or the central star, represented as a tuple of minimum and maximum angles.
        /// </summary>
        public Tuple<int, int> OrbitAngle { get; set; }

        /// <summary>
        /// Size of the planet, typically represented as an integer value.
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        /// Indicates if this object is a moon orbiting another larger planet or orbital body.
        /// </summary>
        public bool IsMoon { get; set; }

        /// <summary>
        /// Indicates whether this planet has a visible ring system.
        /// </summary>
        public bool HasRing { get; set; }

        /// <summary>
        /// Constructor for the <see cref="PlanetInitializer"/> class.
        /// Initializes the list of moons.
        /// </summary>
        public PlanetInitializer()
        {
            Moons = new List<PlanetInitializer>(); // Initialize list for moons to avoid null reference errors.
        }
    }
}
