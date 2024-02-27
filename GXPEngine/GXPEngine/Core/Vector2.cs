using System;

namespace GXPEngine.Core
{
    public struct Vector2
    {
        public float x;
        public float y;

        public Vector2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        /// <summary>
        /// another way of writing (0, -1)
        /// </summary>
        public static Vector2 Forward
        {
            get { return new Vector2(0f, -1f); }
        }

        /// <summary>
        /// another way of writing (0, 1)
        /// </summary>
        public static Vector2 Backward
        {
            get { return new Vector2(0f, 1f); }
        }

        /// <summary>
        /// another way of writing (-1, 0)
        /// </summary>
        public static Vector2 Left
        {
            get { return new Vector2(-1f, 0f); }
        }

        /// <summary>
        /// another way of writing (1, 0)
        /// </summary>
        public static Vector2 Right
        {
            get { return new Vector2(1f, 0f); }
        }

        /// <summary>
        /// Multiply the x and y by factor b
        /// </summary>
        public static Vector2 operator *(Vector2 a, float b)
        {
            return new Vector2(a.x * b, a.y * b);
        }

        /// <summary>
        /// Multiply the x and y by factor a
        /// </summary>
        public static Vector2 operator *(float a, Vector2 b)
        {
            return new Vector2(b.x * a, b.y * a);
        }

        /// <summary>
        /// Multiply the x of vector a by the x of vector b and the y of vector a by the y of vector b
        /// </summary>
        public static Vector2 operator *(Vector2 a, Vector2 b)
        {
            return new Vector2(a.x * b.x, a.y * b.y);
        }

        /// <summary>
        /// Divide the x and y by factor b
        /// </summary>
        public static Vector2 operator /(Vector2 a, float b)
        {
            return new Vector2(a.x / b, a.y / b);
        }

        /// <summary>
        /// Dont worry about it
        /// </summary>
        public static Vector2 operator /(Vector2 a, Vector2 b)
        {
            return new Vector2(a.x / b.x, a.y / b.y);
        }

        /// <summary>
        /// Add the value of b to both the x and y axis of vector2 a
        /// </summary>
        public static Vector2 operator +(Vector2 a, float b)
        {
            return new Vector2(a.x + b, a.y + b);
        }

        /// <summary>
        /// Add the value of a to both the x and y axis of vector2 b
        /// </summary>
        public static Vector2 operator +(float a, Vector2 b)
        {
            return new Vector2(b.x + a, b.y + a);
        }

        /// <summary>
        /// Add the x and y value of b to a
        /// </summary>
        public static Vector2 operator +(Vector2 a, Vector2 b)
        {
            return new Vector2(a.x + b.x, a.y + b.y);
        }

        /// <summary>
        /// Subtract the value of b to both the x and y axis of vector2 a
        /// </summary>
        public static Vector2 operator -(Vector2 a, float b)
        {
            return new Vector2(a.x - b, a.y - b);
        }
        /// <summary>
        /// Subtract the value of a to both the x and y axis of vector2 b
        /// </summary>
        public static Vector2 operator -(float a, Vector2 b)
        {
            return new Vector2(b.x - a, b.y - a);
        }

        /// <summary>
        /// Subtract the value of a to both the x and y axis of vector2 b
        /// </summary>
        public static Vector2 operator -(Vector2 a)
        {
            return new Vector2(-a.x, -a.y);
        }

        /// <summary>
        /// Subtract the x and y value of b from a
        /// </summary>
        public static Vector2 operator -(Vector2 a, Vector2 b)
        {
            return new Vector2(a.x - b.x, a.y - b.y);
        }

        /// <summary>
        /// Turn the vector into a string with pattern "(x, y)"
        /// </summary>
        /// <returns>string with patern "(x, y)"</returns>
        override public string ToString()
        {
            return "(" + x + ", " + y + ")";
        }

        /// <summary>
        /// Returns the distance between vector a and b
        /// </summary>
        /// <param name="a">Starting point</param>
        /// <param name="b">End point</param>
        /// <returns>Distance as a float</returns>
        public static float Distance(Vector2 a, Vector2 b)
        {
            return (a - b).Magnitude;
        }

        /// <summary>
        /// Generate a random vector2 with a magnitude of Strength
        /// </summary>
        /// <param name="strength">strength of the resulting vector</param>
        /// <returns></returns>
        public static Vector2 RandomVector(float strength = 1)
        {
            float r = Utils.Random(-3.1415f, 3.1415f);
            float x = Mathf.Cos(r);
            float y = Mathf.Sin(r);
            return new Vector2(x, y) * strength;
        }

        /// <summary>
        /// lerp the first vector2 to the target vector2
        /// </summary>
        /// <param name="Target">target Vector2</param>
        /// <param name="time">the point between 0 and 1 between the 2 vectors.</param>
        /// <returns>The lerped vector2</returns>
        public static Vector2 Lerp(Vector2 start, Vector2 end, float time)
        {
            time = Mathf.Clamp01(time);
            return start + (end - start) * time;
        }

        /// <summary>
        /// randomize the vector2
        /// </summary>
        /// <param name="strength">strenght of randomness</param>
        public Vector2 Random(float strength = 1)
        {
            return new Vector2(x + Utils.Random(-strength, strength), y + Utils.Random(-strength, strength)).Normalized;
        }

        /// <summary>
        /// The magnitude(length) of a vector.
        /// </summary>
        public float Magnitude
        {
            get { return Mathf.Sqrt(MagnitudeSquared); }
        }


        /// <summary>
        /// The magnitude(length) of a vector squared.
        /// </summary>
        public float MagnitudeSquared
        {
            get { return  x * x + y * y; }
        }

        /// <summary>
        /// returns the vector2 with a magnitude of 1
        /// </summary>
        public Vector2 Normalized
        {
            get { return Magnitude == 0.0f ? new Vector2() : this / Magnitude; }
        }

        /// <summary>
        /// lerp this vector2 to the target vector2
        /// </summary>
        /// <param name="Target">target Vector2</param>
        /// <param name="time">the point between 0 and 1 between the 2 vectors.</param>
        /// <returns>The lerped vector2</returns>
        public Vector2 Lerp(Vector2 Target, float time)
        {
            time = Mathf.Clamp01(time);
            return this + (Target - this) * time;
        }

        /// <summary>
        /// Reflect the vector2 among a certain normal
        /// </summary>
        /// <param name="Normal">the normal of the "mirror"</param>
        /// <returns></returns>
        public Vector2 Reflect(Vector2 Normal)
        {
            Vector2 In = Normalized;
            Vector2 Out = In - 2 * In * Normal * Normal;
            return Out * Magnitude;
        }

        /// <summary>
        /// Rotate the vector2 a certain angle
        /// </summary>
        /// <param name="Angle">the angle of the rotation</param>
        /// <returns></returns>
        public Vector2 Rotate(float Angle)
        {
            float cos = Mathf.Cos(Angle);
            float sin = Mathf.Sin(Angle);
            Vector2 i = new Vector2(cos, sin);
            Vector2 j = new Vector2(-sin, cos);
            return i*x + j*y;
        }
    }
}

