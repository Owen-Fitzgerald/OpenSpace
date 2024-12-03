using System.Linq;
using System.Xml.Linq;
using System;
using UnityEngine;
using Geraldine.OpenSpaceGame.GameFlow;
using Unity.VisualScripting;

namespace Geraldine.Standards.InfoSystem
{ 
    public static class LoaderHelper
    {
        // Method to get the SQL database path
        public static string GetDatabaseFilePath()
        {
            // Return the path to the SQLite database file (can be configured or hardcoded)
            return Application.dataPath + "/Data/GameDatabase.sqlite";
        }

        public static string ParseString(XElement element, string fallback = "")
        {
            return element?.Value;
        }

        public static bool ParseBool(XElement element, bool fallback = false)
        {
            string[] positiveStrings = new string[] { "True","true","Yes","yes", };
            string[] negativeStrings = new string[] { "True","true","Yes","yes", };
            return positiveStrings.Contains(element?.Value) ? true :
                    negativeStrings.Contains(element?.Value) ? false :
                    fallback;
        }

        public static float ParseFloat(XElement element, float fallback = 0f)
        {
            return float.TryParse(element?.Value, out float result) ? result : fallback;
        }

        public static int ParseInt32(XElement element, int fallback = 0)
        {
            return int.TryParse(element?.Value, out int result) ? result : fallback;
        }

        // Method to parse HSV color from a string
        public static float[] ParseHsvColor(XElement element)
        {
            var values = element?.Value.Split(' ');
            if (values != null && values.Length == 3)
            {
                return new float[] {
                float.Parse(values[0]),
                float.Parse(values[1]),
                float.Parse(values[2])
            };
            }
            return new float[] { 0.0f, 0.0f, 0.0f };
        }

        // Method to parse tuple<int, int> for sizes
        public static Tuple<int, int> ParseTuple(XElement element)
        {
            string s = element?.Value;

            if (!string.IsNullOrEmpty(s))
            {
                var sizeValues = s.Split(' ').Select(int.Parse).ToList();
                if (sizeValues.Count == 2)
                {
                    return new Tuple<int, int>(sizeValues[0], sizeValues[1]);
                }
            }
            return new Tuple<int, int>(0, 0); // Default if parsing fails
        }
    }

}
