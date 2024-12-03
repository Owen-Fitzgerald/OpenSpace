using Geraldine._4XEngine.Initializers;
using Geraldine._4XEngine.Loaders;
using Geraldine._4XEngine.GraphTree;
using Geraldine._4XEngine.GraphTree.Interfaces;
using Geraldine._4XEngine.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;

namespace Geraldine._4XEngine.Factories
{
    public static class SystemFactory
    {
        /// <summary>
        /// Generates a star system based on the given system initializer.
        /// This includes creating the central star and planets, setting their properties.
        /// </summary>
        public static StarSystem CreateSystem(SystemInitializer systemInitializer, UnityEngine.Vector3 location, NodeGraph<StarSystem> ParentGraph)
        {
            // Create the central star
            StarSystem system = new StarSystem()
            {
                //Name = systemInitializer.Name,
                Location = location,
                //Star = star,
                //Flags = systemInitializer.UsageOdds?.ConvertAll(odds => odds.HasStarFlag)
            };

            PopulateSystem(systemInitializer, system);

            // Create the final star system object
            return system;
        }

        /// <summary>
        /// Generates a star system based on the given system initializer.
        /// This includes creating the central star and planets, setting their properties.
        /// </summary>
        public static StarSystem CreateSystem(SystemInitializer systemInitializer, NodeGraph<StarSystem> ParentGraph)
        {
            // Create the central star
            StarSystem system = new StarSystem()
            {
                //Name = systemInitializer.Name,
                //Star = star,
                //Flags = systemInitializer.UsageOdds?.ConvertAll(odds => odds.HasStarFlag)
            };

            PopulateSystem(systemInitializer, system);

            // Create the final star system object
            return system;
        }

        // Create a random procedural system
        public static StarSystem CreateSystem(NodeGraph<StarSystem> ParentGraph)
        {
            SystemInitializer systemInit = RandomSystemInit();
            return CreateSystem(systemInit, ParentGraph);
        }

        // Randomly select a system initializer
        public static SystemInitializer RandomSystemInit()
        {
            string[] systemInits = SystemInitializerLoader.GetAllContractReferences().ToArray();
            int randomIndex = SeededRandom.Range(0, systemInits.Count());
            SystemInitializer loadedClass = SystemInitializerLoader.GetContractByReference(systemInits[randomIndex]);

            return loadedClass;
        }
        private static void PopulateSystem(SystemInitializer systemInitializer, StarSystem system)
        {
            // Create planets based on the initializer
            List<OrbitalBody> planets = new List<OrbitalBody>();
            foreach (var predefinedPlanet in systemInitializer.Planets)
            {
                OrbitalBody planet = PlanetFactory.CreateBody(predefinedPlanet, system);
                planets.Add(planet);

                // If this planet has moons, create and assign them
                if (predefinedPlanet.Moons != null && predefinedPlanet.Moons.Count > 0)
                {
                    foreach (var moonData in predefinedPlanet.Moons)
                    {
                        OrbitalBody moon = PlanetFactory.CreateBody(moonData, planet);
                        planet.AddChildNode(moon);
                    }
                }

                system.AddChildNode(planet);
            }
        }

    }
}
