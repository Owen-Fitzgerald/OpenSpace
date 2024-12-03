using Geraldine._4XEngine.Initializers;
using Geraldine._4XEngine.GraphTree;
using Geraldine._4XEngine.GraphTree.Interfaces;
using Geraldine._4XEngine.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using Vector3 = UnityEngine.Vector3;
using Unity.VisualScripting;

namespace Geraldine._4XEngine.Factories
{
    public static class GalaxyFactory
    {
        private static GalaxyInitializer _galaxyInitializer;
        private static List<SystemInitializer> _availableSystemInitializers = new List<SystemInitializer>();
        public static Galaxy GenerateGalaxy(GalaxyInitializer galaxyInitializer, int seed)
        {
            _galaxyInitializer = galaxyInitializer;

            // Step 1: Generate system locations and hyperlane connections
            Galaxy generatedGalaxy = new Galaxy();
            SeededRandom.Initialize(seed);
            List<SystemLite> systemLocations = GenerateSystemLocations();
            List<HyperlaneLite> hyperlanes = GenerateHyperlaneConnections(systemLocations);

            // Step 2: Place nation starting systems
            generatedGalaxy.AddChildNodes(PlacePredefinedSystems(_availableSystemInitializers, systemLocations));

            // Step 3: Place precursor story systems, ensuring no overrides of nation systems
            if (!string.IsNullOrEmpty(_galaxyInitializer.PrecursorStory))
            {
                //PlacePredefinedSystems(_precursorSystemInitializers, systemLocations.Where(_ => !_.Initialized)));
            }

            // Step 4: Randomly initialize any remaining systems
            do
            {
                SystemInitializer RandomInit = SystemFactory.RandomSystemInit();
                generatedGalaxy.AddChildNodes(LoadSystemFromInitializer(RandomInit, systemLocations.Where(_ => !_.Initialized)));
            }
            while (systemLocations.Any(_ => !_.Initialized));

            return generatedGalaxy;
        }

        private static List<SystemLite> GenerateSystemLocations()
        {
            // Use galaxy radius and size to determine positions of systems in the galaxy
            List<SystemLite> locations = new List<SystemLite>
            {
                new SystemLite()
                { 
                    Position = new Vector3(0, 0, 0),
                    Hyperlanes = new List<HyperlaneLite>()
                }
            };
            for (int i = 0; i < _galaxyInitializer.Size.Stars; i++)
            {
                SystemLite location = new SystemLite()
                { 
                    Position = new Vector3(
                        SeededRandom.Range(-_galaxyInitializer.Size.Radius, _galaxyInitializer.Size.Radius),
                        SeededRandom.Range(-_galaxyInitializer.Size.Radius, _galaxyInitializer.Size.Radius),
                        0),
                    Hyperlanes = new List<HyperlaneLite>()
                }; // Assume 2D for simplicity
                locations.Add(location);
            }
            return locations;
        }

        private static List<HyperlaneLite> GenerateHyperlaneConnections(List<SystemLite> liteSystems)
        {
            List<HyperlaneLite> hyperlanes = new List<HyperlaneLite>();

            // Generate hyperlanes between systems based on hyperlane density
            // Each system should have some number of connections (randomized by density)
            for (int i = 0; i < liteSystems.Count; i++)
            {
                SystemLite system1 = liteSystems[i];
                int connections = (int)(_galaxyInitializer.HyperlaneDensity * liteSystems.Count);

                if(connections < 1) { connections = 1; }

                for (int j = 0; j < connections; j++)
                {
                    SystemLite system2 = GetRandomNearbySystem(system1, liteSystems);
                    HyperlaneLite lane = new HyperlaneLite() { StartSystem = system1, EndSystem = system2 };

                    hyperlanes.Add(lane);
                    system1.Hyperlanes.Add(lane);
                    system2.Hyperlanes.Add(lane);
                }
            }
            return hyperlanes;
        }

        private static IList<StarSystem> PlacePredefinedSystems(List<SystemInitializer> Initializers, IEnumerable<SystemLite> lites)
        {
            IList<StarSystem> starSystems = new List<StarSystem>();
            foreach(SystemInitializer initializer in Initializers)
            {
                starSystems.AddRange(LoadSystemFromInitializer(initializer, lites));
            }

            return starSystems;
        }

        private static IList<StarSystem> LoadSystemFromInitializer(SystemInitializer initializer, IEnumerable<SystemLite> lites)
        {
            IList<StarSystem> starSystems = new List<StarSystem>();
            SystemLite startingLocation = GetValidStartingLocation(initializer, lites);

            starSystems.Add(SystemFactory.CreateSystem(initializer, startingLocation.Position, null));
            startingLocation.Initialized = true;

            //Initialize neighboring systems for nations
            if (initializer.NeighborSystems != null && initializer.NeighborSystems.Count() > 0)
                starSystems.AddRange(PlacePredefinedSystems(initializer.NeighborSystems, GetNeighboringLites(startingLocation)));

            return starSystems;
        }

        private static List<SystemLite> GetNeighboringLites(SystemLite lite)
        {
            List<SystemLite> neighbors = new List<SystemLite>();

            // Initialize neighboring systems for each nation using system initializer's tree logic
            foreach (var hyperlane in lite.Hyperlanes)
            {
                if(hyperlane.StartSystem.Equals(lite)) { neighbors.Add(hyperlane.EndSystem); }
                else { neighbors.Add(hyperlane.StartSystem); }
            }

            return neighbors;
        }

        private static SystemLite GetValidStartingLocation(SystemInitializer Initializer, IEnumerable<SystemLite> liteSystems)
        {
            // Ensure that starting systems are spread apart by a minimum distance
            SystemLite location;

            List<SystemLite> possibleChoices = liteSystems.Where(_ => !_.Initialized &&
                                                                        (Initializer.NeighborSystems == null ||
                                                                        _.Hyperlanes.Count() >= Initializer.NeighborSystems.Count())
                                                                       ).ToList();

            location = possibleChoices[SeededRandom.Next(possibleChoices.Count)];

            return location;
        }

        private static SystemLite GetRandomNearbySystem(SystemLite system, List<SystemLite> locations)
        {
            // Get a random nearby system within a certain distance range
            return locations[SeededRandom.Next(locations.Count)]; // Simplified
        }
        private class SystemLite
        {
            public Vector3 Position;
            public List<HyperlaneLite> Hyperlanes;

            public bool Initialized;
        }

        private class HyperlaneLite
        {
            public SystemLite StartSystem;
            public SystemLite EndSystem;
        }
    }
}
