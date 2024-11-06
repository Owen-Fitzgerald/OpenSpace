using System.Linq;
using System.Xml.Linq;
using System;
using UnityEngine;

namespace Geraldine._4XEngine.Util
{
    public static class LoaderHelper
    {
        // Method to get the SQL database path
        public static string GetDatabaseFilePath()
        {
            // Return the path to the SQLite database file (can be configured or hardcoded)
            return Application.dataPath + "/Data/GameDatabase.sqlite";
        }

        // Method to parse HSV color from a string

        public static float[] ParseHsvColor(XElement hsvElement)
        {
            var values = hsvElement?.Value.Split(' ');
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
        public static Tuple<int, int> ParseSizeTuple(string sizeString)
        {
            if (!string.IsNullOrEmpty(sizeString))
            {
                var sizeValues = sizeString.Split(' ').Select(int.Parse).ToList();
                if (sizeValues.Count == 2)
                {
                    return new Tuple<int, int>(sizeValues[0], sizeValues[1]);
                }
            }
            return new Tuple<int, int>(0, 0); // Default if parsing fails
        }
    }

}
