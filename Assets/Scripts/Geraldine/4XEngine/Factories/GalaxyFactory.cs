using Geraldine._4XEngine.Initializers;
using Geraldine._4XEngine.GraphTree;
using Geraldine._4XEngine.GraphTree.Interfaces;
using Geraldine._4XEngine.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using Vector3 = UnityEngine.Vector3;

namespace Geraldine._4XEngine.Factories
{
    public static class GalaxyFactory
    {
        private static GalaxyInitializer _galaxyInitializer;
        private static List<SystemInitializer> _availableSystemInitializers;
        private static Galaxy generatedGalaxy;
        public static List<StarSystem> GenerateGalaxy(GalaxyInitializer galaxyInitializer, int seed)
        {
            _galaxyInitializer = galaxyInitializer;

            List<StarSystem> systems = new List<StarSystem>();

            // Step 1: Generate system locations and hyperlane connections
            generatedGalaxy = new Galaxy();
            List<SystemLite> systemLocations = GenerateSystemLocations();
            List<HyperlaneLite> hyperlanes = GenerateHyperlaneConnections(systemLocations);

            // Step 2: Place nation starting systems
            PlaceSystems(_availableSystemInitializers, systemLocations);

            // Step 3: Place precursor story systems, ensuring no overrides of nation systems
            //PlacePrecursorStorySystems();

            // Step 4: Randomly initialize any remaining systems
            do
            {
                SystemInitializer RandomInit = SystemFactory.RandomSystemInit();
            }
            while (systemLocations.Any(_ => !_.Initialized));

            return systems;
        }

        private static List<SystemLite> GenerateSystemLocations()
        {
            // Use galaxy radius and size to determine positions of systems in the galaxy
            List<SystemLite> locations = new List<SystemLite>();
            locations.Add(new SystemLite() { Position = new Vector3(0,0,0) });
            for (int i = 0; i < _galaxyInitializer.Size.Stars; i++)
            {
                SystemLite location = new SystemLite(){ Position = new Vector3(
                    SeededRandom.Range(-_galaxyInitializer.Size.Radius, _galaxyInitializer.Size.Radius),
                    SeededRandom.Range(-_galaxyInitializer.Size.Radius, _galaxyInitializer.Size.Radius),
                    0) }; // Assume 2D for simplicity
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

        private static void PlaceSystems(List<SystemInitializer> Initializers, List<SystemLite> lites)
        {
            foreach(SystemInitializer initializer in Initializers)
            {
                SystemLite startingLocation = GetValidStartingLocation(initializer, lites);

                StarSystem system = SystemFactory.CreateSystem(initializer, startingLocation.Position, (INodeGraph<INode>)generatedGalaxy);
                generatedGalaxy.AddChildNode(system);
                startingLocation.Initialized = true;

                //Initialize neighboring systems for nations
                PlaceSystems(initializer.NeighborSystems, GetNeighboringLites(startingLocation));
            }
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

        private static void PlacePrecursorStorySystems(List<StarSystem> systems, List<Hyperlane> hyperlanes)
        {
            // Use precursor story reference from _galaxyInitializer to place relevant systems
            if (!string.IsNullOrEmpty(_galaxyInitializer.PrecursorStory))
            {
                // Fetch precursor story related systems and place them
            }
        }

        private static SystemLite GetValidStartingLocation(SystemInitializer Initializer, List<SystemLite> liteSystems)
        {
            // Ensure that starting systems are spread apart by a minimum distance
            SystemLite location;

            List<SystemLite> possibleChoices = liteSystems.Where(_ => !_.Initialized &&
                                                                       _.Hyperlanes.Count() >= Initializer.NeighborSystems.Count()
                                                                       ).ToList();

            location = possibleChoices[SeededRandom.Next(possibleChoices.Count)];

            return location;
        }

        private static SystemLite GetRandomNearbySystem(SystemLite system, List<SystemLite> locations)
        {
            // Get a random nearby system within a certain distance range
            return locations[SeededRandom.Next(locations.Count)]; // Simplified
        }
        private struct SystemLite
        {
            public Vector3 Position;
            public List<HyperlaneLite> Hyperlanes;

            public bool Initialized;
        }

        private struct HyperlaneLite
        {
            public Vector3 Position;
            public SystemLite StartSystem;
            public SystemLite EndSystem;
        }
    }
}
