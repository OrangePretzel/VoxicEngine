using UnityEngine;

namespace Voxic.Extensions.Vector
{
    /// <summary>
    /// Class of extensions to the Vector2 struct
    /// </summary>
    public static class Vector2Extension
    {
        /// <summary>
        /// Rotates the vector by the given degrees
        /// </summary>
        /// <param name="v">this</param>
        /// <param name="degrees">The amount to rotate the vector by in degrees</param>
        /// <returns></returns>
        public static Vector2 Rotate(this Vector2 v, float degrees)
        {
            float sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
            float cos = Mathf.Cos(degrees * Mathf.Deg2Rad);

            float tx = v.x;
            float ty = v.y;
            v.x = (cos * tx) - (sin * ty);
            v.y = (sin * tx) + (cos * ty);
            return v;
        }
    }
}