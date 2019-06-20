using System;
using SFML.System;

namespace SFMLReady.Libraries
{
    static class Vector2
    {
        public readonly static Vector2f Zero = new Vector2f(0, 0);
        public static float Distance(Vector2f First, Vector2f Second)
        {
            float distance = (float)Math.Sqrt(SqDistance(First, Second));

            return distance;
        }

        public static float SqDistance(Vector2f First, Vector2f Second)
        {
            float sqdistance;

            sqdistance = (float)(Math.Pow(Second.X - First.X, 2) + Math.Pow(Second.Y - First.Y, 2));

            return sqdistance;
        }

        public static Vector2f Normalize(Vector2f vector)
        {
            float magnitude = GetMagnitude(vector);

            if (magnitude == 0)
            {
                return Zero;
            }
            else
            {
                return vector / magnitude;
            }
        }

        public static float GetMagnitude(Vector2f vector)
        {
            float magnitude;

            magnitude = Distance(Zero, vector);

            return magnitude;
        }

        public static Vector2f SetMagnitude(Vector2f vector, float magnitude)
        {
            Vector2f result;

            result = Normalize(vector) * magnitude;

            return result;
        }

        public static Vector2f Random(Random rd, float minX, float maxX, float minY, float maxY)
        {
            float variationX = maxX - minX;
            float variationY = maxY - minY;

            float X = (float)rd.NextDouble() * variationX + minX;
            float Y = (float)rd.NextDouble() * variationY + minY;

            return new Vector2f(X, Y);
        }
    }
}
