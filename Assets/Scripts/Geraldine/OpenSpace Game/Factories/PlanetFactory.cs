using Geraldine._4XEngine.Contracts;
using Geraldine._4XEngine.Initializers;
using Geraldine._4XEngine.Loaders;
using Geraldine._4XEngine.GraphTree;
using Geraldine._4XEngine.GraphTree.Interfaces;
using Geraldine._4XEngine.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using System.Drawing;

namespace Geraldine._4XEngine.Factories
{
    public static class PlanetFactory
    {
        const float minHexWidth = 1.0f;
        const float maxHexWidth = 1.5f;

        #region Constructors
        // Create an instance of a PredefinedPlanet loaded from xml
        public static OrbitalBody CreateBody(PlanetInitializer planetInitilizer, NodeGraph<OrbitalBody> ParentGraph)
        {
            //Check for errors
            if (!planetInitilizer.IsMoon && ParentGraph is OrbitalBody)
                throw new Exception("Attempted to create a moon with invalid PredefinedPlanet");

            //Define new orbital body
            OrbitalBody newBody = new OrbitalBody()
            {
                Name = planetInitilizer.Name,
                ParentGraph = ParentGraph,
                IsMoon = planetInitilizer.IsMoon,
                PlanetClass = planetInitilizer.PlanetClass,
                Size = planetInitilizer.Size
            };

            int targetHexCount = CalculateTargetHexCount(newBody);
            GenerateHexNodes(newBody, targetHexCount);

            return newBody;

        }

        // Create a procedural planet of given PlanetClass loaded from XML
        public static OrbitalBody CreateBody(PlanetClass planetClass, NodeGraph<OrbitalBody> ParentGraph)
        {
            //Check for errors
            if (!planetClass.CanBeMoon && ParentGraph is OrbitalBody)
                throw new Exception("Attempted to create a moon with invalid PlanetClass");

            //Define new orbital body
            bool isMoon = ParentGraph is OrbitalBody;
            OrbitalBody newBody = new OrbitalBody()
            {
                Name = "",
                ParentGraph = ParentGraph,
                IsMoon = isMoon,
                PlanetClass = planetClass.Entity,
                Size = isMoon ? 
                        SeededRandom.Range(planetClass.MoonSize.Item1, planetClass.MoonSize.Item2) :
                        SeededRandom.Range(planetClass.PlanetSize.Item1, planetClass.PlanetSize.Item2)
            };

            int targetHexCount = CalculateTargetHexCount(newBody);
            GenerateHexNodes(newBody, targetHexCount);

            return newBody;
        }

        // Create a random procedural planet
        public static OrbitalBody CreateBody(NodeGraph<OrbitalBody> ParentGraph)
        {
            PlanetClass planetClass = RandomPlanetClass(ParentGraph is OrbitalBody);
            return CreateBody(planetClass, ParentGraph);
        }
        #endregion

        //Private Methods

        #region Hex Projection
        private static int CalculateTargetHexCount(OrbitalBody orbitalBody)
        {
            // Step 1: Calculate sphere surface area
            float sphereSurfaceArea = 4 * Mathf.PI * orbitalBody.Size * orbitalBody.Size;

            // Step 2: Calculate average hex width and area
            float avgHexWidth = (minHexWidth + maxHexWidth) / 2;
            float hexArea = (3 * Mathf.Sqrt(3) / 2) * Mathf.Pow(avgHexWidth / 2, 2);

            // Step 3: Determine target hex count
            int targetHexCount = Mathf.RoundToInt(sphereSurfaceArea / hexArea);

            // Step 4: Adjust target count to nearest multiple for symmetry (e.g., divisible by 12)
            targetHexCount = AdjustHexCountForSymmetry(targetHexCount);

            return targetHexCount;
        }

        private static int AdjustHexCountForSymmetry(int count)
        {
            // Ensure count is divisible by 12 for better hex sphere distribution
            return (count / 12) * 12;
        }

        private static void GenerateHexNodes(OrbitalBody orbitalBody, int targetHexCount)
        {
            // Step 1: Calculate surface area of the sphere
            float sphereSurfaceArea = 4 * Mathf.PI * orbitalBody.Size * orbitalBody.Size;

            // Step 2: Generate hex nodes and distribute them on the sphere
            for (int i = 0; i < targetHexCount; i++)
            {
                Vector3 position = CalculateHexPositionOnSphere(orbitalBody, i, targetHexCount);
                HexNode hex = new HexNode() { Index = i, Position = position };
                orbitalBody.surfaceGraph.AddChildNode(hex);
            }
        }

        // This function distributes hexes around the sphere based on their index
        private static Vector3 CalculateHexPositionOnSphere(OrbitalBody orbitalBody, int index, int totalHexes)
        {
            // Distribute hexes evenly using spherical coordinates
            float phi = Mathf.Acos(1 - 2 * (index + 0.5f) / totalHexes);  // Latitude
            float theta = Mathf.PI * (1 + Mathf.Sqrt(5)) * index;         // Longitude (golden ratio angle)

            // Convert spherical coordinates to Cartesian coordinates
            float x = orbitalBody.Size * Mathf.Sin(phi) * Mathf.Cos(theta);
            float y = orbitalBody.Size * Mathf.Sin(phi) * Mathf.Sin(theta);
            float z = orbitalBody.Size * Mathf.Cos(phi);

            return new Vector3(x, y, z);
        }
        #endregion

        // Randomly select a planet class
        private static PlanetClass RandomPlanetClass(bool IsMoon = false)
        {
            string[] planetClasses = PlanetClassLoader.GetAllContractReferences().ToArray();
            int randomIndex = SeededRandom.Range(0, planetClasses.Count());
            PlanetClass loadedClass = PlanetClassLoader.GetContractByReference(planetClasses[randomIndex]);

            //Retry if IsMoon but loadedClass can't be a moon
            if (IsMoon && !loadedClass.CanBeMoon)
                loadedClass = RandomPlanetClass(IsMoon);

            return loadedClass;
        }
    }
}
