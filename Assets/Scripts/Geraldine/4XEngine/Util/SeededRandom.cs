using System;

namespace Geraldine._4XEngine.Util
{    public static class SeededRandom
    {
        private static System.Random random;

        // Initialize the random number generator with a specific seed
        public static void Initialize(int seed)
        {
            random = new System.Random(seed);
        }

        // Generate a random integer
        public static int Next(int next)
        {
            return random.Next(next);
        }

        // Generate a random integer
        public static int Range(int min, int max)
        {
            return random.Next(min, max);
        }

        // Generate a random float (mimicking UnityEngine.Random.Range)
        public static float Range(float min, float max)
        {
            return (float)(random.NextDouble() * (max - min) + min);
        }

        // Generate a random double
        public static double NextDouble()
        {
            return random.NextDouble();
        }
    }

}
