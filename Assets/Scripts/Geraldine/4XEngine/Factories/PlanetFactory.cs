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

namespace Geraldine._4XEngine.Factories
{
    public static class PlanetFactory
    {
        // Create an instance of a PredefinedPlanet loaded from xml
        public static OrbitalBody CreateBody(PlanetInitializer planetInitilizer, INodeGraph<INode> ParentGraph)
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

            return newBody;

        }

        // Create a procedural planet of given PlanetClass loaded from XML
        public static OrbitalBody CreateBody(PlanetClass planetClass, INodeGraph<INode> ParentGraph)
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

            return newBody;
        }

        // Create a random procedural planet
        public static OrbitalBody CreateBody(INodeGraph<INode> ParentGraph)
        {
            PlanetClass planetClass = RandomPlanetClass(ParentGraph is OrbitalBody);
            return CreateBody(planetClass, ParentGraph);
        }

        // Randomly select a planet class
        private static PlanetClass RandomPlanetClass(bool IsMoon = false)
        {
            string[] planetClasses = PlanetClassLoader.GetAllPlanetClassReferences().ToArray();
            int randomIndex = SeededRandom.Range(0, planetClasses.Count());
            PlanetClass loadedClass = PlanetClassLoader.GetPlanetClassByReference(planetClasses[randomIndex]);

            //Retry if IsMoon but loadedClass can't be a moon
            if (IsMoon && !loadedClass.CanBeMoon)
                loadedClass = RandomPlanetClass(IsMoon);

            return loadedClass;
        }
    }
}
