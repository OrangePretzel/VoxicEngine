using UnityEngine;

namespace Voxic.Math
{
    /// <summary>
    /// Data to represent integer based vectors
    /// </summary>
    public struct IntVector3
    {
        /// <summary>
        /// The x component
        /// </summary>
        public int X;
        /// <summary>
        /// The y component
        /// </summary>
        public int Y;
        /// <summary>
        /// The z component
        /// </summary>
        public int Z;

        /// <summary>
        /// Create a new IntVector3 with the given components
        /// </summary>
        /// <param name="x">The x component</param>
        /// <param name="y">The y component</param>
        /// <param name="z">The z component</param>
        public IntVector3(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        #region Operators

        /// <summary>
        /// Add
        /// </summary>
        /// <param name="v1">a</param>
        /// <param name="v2">b</param>
        /// <returns>a + b</returns>
        public static IntVector3 operator +(IntVector3 v1, IntVector3 v2)
        {
            return new IntVector3(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
        }

        /// <summary>
        /// Subtract
        /// </summary>
        /// <param name="v1">a</param>
        /// <param name="v2">b</param>
        /// <returns>a - b</returns>
        public static IntVector3 operator -(IntVector3 v1, IntVector3 v2)
        {
            return new IntVector3(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
        }

        /// <summary>
        /// Multiply
        /// </summary>
        /// <param name="v1">a</param>
        /// <param name="s">b</param>
        /// <returns>a * b</returns>
        public static IntVector3 operator *(IntVector3 v1, int s)
        {
            return new IntVector3(v1.X * s, v1.Y * s, v1.Z * s);
        }

        /// <summary>
        /// Multiply
        /// </summary>
        /// <param name="s">a</param>
        /// <param name="v1">b</param>
        /// <returns>a * b</returns>
        public static IntVector3 operator *(int s, IntVector3 v1)
        {
            return new IntVector3(v1.X * s, v1.Y * s, v1.Z * s);
        }

        /// <summary>
        /// Divide
        /// </summary>
        /// <param name="v1">a</param>
        /// <param name="s">b</param>
        /// <returns>a / b</returns>
        public static IntVector3 operator /(IntVector3 v1, int s)
        {
            return new IntVector3(v1.X / s, v1.Y / s, v1.Z / s);
        }

        /// <summary>
        /// Modulus
        /// </summary>
        /// <param name="v1">a</param>
        /// <param name="s">b</param>
        /// <returns>a % b</returns>
        public static IntVector3 operator %(IntVector3 v1, int s)
        {
            return new IntVector3(v1.X % s, v1.Y % s, v1.Z % s);
        }

        #endregion

        #region Conversions

        /// <summary>
        /// Convert from IntVector3 to Vector3
        /// </summary>
        /// <param name="iv3">this</param>
        public static explicit operator Vector3(IntVector3 iv3)
        {
            return new Vector3(iv3.X, iv3.Y, iv3.Z);
        }

        /// <summary>
        /// Convert from Vector3 to IntVector3. (uses truncation)
        /// </summary>
        /// <param name="v3">this</param>
        public static explicit operator IntVector3(Vector3 v3)
        {
            return new IntVector3(
                (int)v3.x,
                (int)v3.y,
                (int)v3.z);
        }

        /// <summary>
        /// Convert from Vector3 to IntVector3 (rounds)
        /// </summary>
        /// <param name="v3">The Vector3 to convert</param>
        /// <returns></returns>
        public static IntVector3 RoundFromVector3(Vector3 v3)
        {
            return new IntVector3(
                Mathf.RoundToInt(v3.x),
                Mathf.RoundToInt(v3.y),
                Mathf.RoundToInt(v3.z));
        }

        #endregion

        /// <summary>
        /// Returns a string representing the components of the vector
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("({0}, {1}, {2})", X, Y, Z);
        }
    }
}
