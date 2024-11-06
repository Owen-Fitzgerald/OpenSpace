using System;
using System.Collections.Generic;
using static UnityEngine.UIElements.UxmlAttributeDescription;
using UnityEditor.Experimental.GraphView;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using Unity.VisualScripting;

namespace Geraldine._4XEngine.Initializers
{
    // Represents a system initializer, which can include specific properties such as planets, system usage, and spawn chances.
    public class SystemInitializer
    {
        // String UniqueId
        public string UniqueId { get; set; }

        // [Optional; default/blank = random name] Localization string for this system/its main star
        public string Name { get; set; }

        // System's star class: may be a specific class (e.g. 'sc_binary_10') or a random class from a list (e.g. 'rl_standard_stars')
        public string Class { get; set; }

        //Flags that may be referenced in triggers and effects ('has_star_flag')
        public List<String> Flags { get; set; }

        #region USAGE & SPAWN CHANCES
        //Valid commands and values include:
        //usage = empire_init				# May be chosen randomly for regular empires that do not specify a custom initializer
        //usage = fallen_empire_init		# May be chosen randomly for fallen empires that do not specify a custom initializer
        //usage = misc_system_init			# May be chosen randomly for unoccupied systems, i.e those which have not been initialized by empires or fallen empires
        //usage = custom_empire				# May be selected by the player during custom empire creation
        //usage = origin					# Used by Origins 

        // System usage, such as 'misc_system_init'
        public string Usage { get; set; }

        //NOTE: You may specify multiple usages for an initializer, or none at all.If no usage is specified(as is the case for this example) :
        //the game may only use this initializer when explicitly called from another script, such as for a prescripted empire or as part of setting up an Origin.

        // List of usage odds modifiers
        public List<UsageOdds> UsageOdds { get; set; }

        // Maximum number of times this system can appear
        public int MaxInstances { get; set; }

        // The following two are checked after the Usage script above, and are mutually exclusive with each other:
        // spawn_chance = 60					# [1–100; default = 100] Percentage chance that this initializer is considered at all
        // scaled_spawn_chance = 8			# [integer] Percentage chance that this initializer is considered, per a formula: (scaled_spawn_chance * total systems in galaxy ) / 100

        // Scaled spawn chance for procedural generation
        public int ScaledSpawnChance { get; set; }

        // Direct spawn chance for unique system
        public int SpawnChance { get; set; }

        #endregion

        #region SETTINGS
        // [default = no] Sets this initializer to appear more or less often depending on the 'Pre-FTL Civilizations' galaxy setting
        public bool PrimitveSystem { get; set; }

        // [default = no] Prevents generic anomalies from spawning inside this system
        public bool PreventAnamolies { get; set; }
        #endregion

        #region INITIALIZER EFFECT

        #endregion

        #region STARS & PLANETS
        //Stars are initialized as planets and then classified as stars:
        //planet = {
		//  class = star
        //  orbit_distance = 0
        //}

        // List of planets in the system
        public List<PlanetInitializer> Planets { get; set; }

        //Asteroid Belt within system
        public AsteroidBeltInitializer AsteroidBelt { get; set; }

        #endregion

        //Neighboring System Initializers Tree
        public List<SystemInitializer> NeighborSystems { get; set; }

        public SystemInitializer()
        {
            // Initialize list for usage odds
            UsageOdds = new List<UsageOdds>();

            // Initialize list for flags
            Flags = new List<String>();

            // Initialize list for planets
            Planets = new List<PlanetInitializer>();
        }
    }

    public class UsageOdds
    {
        // Modifier factor
        public int Factor { get; set; }

        // Boolean flag for cluster usage
        public bool IsFeCluster { get; set; }

        // Star flag, for empire cluster
        public string HasStarFlag { get; set; }

        // Boolean flag for bottleneck system
        public bool IsBottleneckSystem { get; set; }
    }

}
