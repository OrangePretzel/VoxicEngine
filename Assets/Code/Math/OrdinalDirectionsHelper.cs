using UnityEngine;

namespace Voxic.Math
{
    /// <summary>
    /// Class for OrdinalDirections helpers
    /// </summary>
    public static class OrdinalDirectionsHelper
    {
        #region Lookup Arrays

        /// <summary>
        /// The face associated with each direction under a given rotation axis and rotation
        /// </summary>
        private static readonly OrdinalDirections[][][] DIR_MAP = new OrdinalDirections[][][]
        {
            // Up
            new OrdinalDirections[][]
            {
                // Up 0
                new OrdinalDirections[] { OrdinalDirections.Up, OrdinalDirections.Down, OrdinalDirections.North, OrdinalDirections.East, OrdinalDirections.South, OrdinalDirections.West },
                // Up 90
                new OrdinalDirections[] { OrdinalDirections.Up, OrdinalDirections.Down, OrdinalDirections.West, OrdinalDirections.North, OrdinalDirections.East, OrdinalDirections.South },
                // Up 180
                new OrdinalDirections[] { OrdinalDirections.Up, OrdinalDirections.Down, OrdinalDirections.South, OrdinalDirections.West, OrdinalDirections.North, OrdinalDirections.East },
                // Up 270
                new OrdinalDirections[] { OrdinalDirections.Up, OrdinalDirections.Down, OrdinalDirections.East, OrdinalDirections.South, OrdinalDirections.West, OrdinalDirections.North }
            },
            // Down
            new OrdinalDirections[][]
            {
                // Down 0
                new OrdinalDirections[] { OrdinalDirections.Down, OrdinalDirections.Up, OrdinalDirections.North, OrdinalDirections.West, OrdinalDirections.South, OrdinalDirections.East },
                // Down 90
                new OrdinalDirections[] { OrdinalDirections.Down, OrdinalDirections.Up, OrdinalDirections.East, OrdinalDirections.North, OrdinalDirections.West, OrdinalDirections.South },
                // Down 180
                new OrdinalDirections[] { OrdinalDirections.Down, OrdinalDirections.Up, OrdinalDirections.South, OrdinalDirections.East, OrdinalDirections.North, OrdinalDirections.West },
                // Down 270
                new OrdinalDirections[] { OrdinalDirections.Down, OrdinalDirections.Up, OrdinalDirections.West, OrdinalDirections.South, OrdinalDirections.East, OrdinalDirections.North }
            },
            // North
            new OrdinalDirections[][]
            {
                // North 0
                new OrdinalDirections[] { OrdinalDirections.North, OrdinalDirections.South, OrdinalDirections.Up, OrdinalDirections.East, OrdinalDirections.Down, OrdinalDirections.West },
                // North 90
                new OrdinalDirections[] { OrdinalDirections.West, OrdinalDirections.East, OrdinalDirections.Up, OrdinalDirections.North, OrdinalDirections.Down, OrdinalDirections.South },
                // North 180
                new OrdinalDirections[] { OrdinalDirections.South, OrdinalDirections.North, OrdinalDirections.Up, OrdinalDirections.West, OrdinalDirections.Down, OrdinalDirections.East },
                // North 270
                new OrdinalDirections[] { OrdinalDirections.East, OrdinalDirections.West, OrdinalDirections.Up, OrdinalDirections.South, OrdinalDirections.Down, OrdinalDirections.North }
            },
            // East
            new OrdinalDirections[][]
            {
                // East 0
                new OrdinalDirections[] { OrdinalDirections.North, OrdinalDirections.South, OrdinalDirections.West, OrdinalDirections.Up, OrdinalDirections.East, OrdinalDirections.Down },
                // East 90
                new OrdinalDirections[] { OrdinalDirections.West, OrdinalDirections.East, OrdinalDirections.South, OrdinalDirections.Up, OrdinalDirections.North, OrdinalDirections.Down },
                // East 180
                new OrdinalDirections[] { OrdinalDirections.South, OrdinalDirections.North, OrdinalDirections.East, OrdinalDirections.Up, OrdinalDirections.West, OrdinalDirections.Down },
                // East 270
                new OrdinalDirections[] { OrdinalDirections.East, OrdinalDirections.West, OrdinalDirections.North, OrdinalDirections.Up, OrdinalDirections.South, OrdinalDirections.Down }
            },
            // South
            new OrdinalDirections[][]
            {
                // South 0
                new OrdinalDirections[] { OrdinalDirections.North, OrdinalDirections.South, OrdinalDirections.Down, OrdinalDirections.East, OrdinalDirections.Up, OrdinalDirections.West },
                // South 90
                new OrdinalDirections[] { OrdinalDirections.West, OrdinalDirections.East, OrdinalDirections.Down, OrdinalDirections.North, OrdinalDirections.Up, OrdinalDirections.South },
                // South 180
                new OrdinalDirections[] { OrdinalDirections.South, OrdinalDirections.North, OrdinalDirections.Down, OrdinalDirections.West, OrdinalDirections.Up, OrdinalDirections.East },
                // South 270
                new OrdinalDirections[] { OrdinalDirections.East, OrdinalDirections.West, OrdinalDirections.Down, OrdinalDirections.South, OrdinalDirections.Up, OrdinalDirections.North }
            },
            // West
            new OrdinalDirections[][]
            {
                // West 0
                new OrdinalDirections[] { OrdinalDirections.North, OrdinalDirections.South, OrdinalDirections.East, OrdinalDirections.Down, OrdinalDirections.West, OrdinalDirections.Up },
                // West 90
                new OrdinalDirections[] { OrdinalDirections.West, OrdinalDirections.East, OrdinalDirections.North, OrdinalDirections.Down, OrdinalDirections.South, OrdinalDirections.Up },
                // West 180
                new OrdinalDirections[] { OrdinalDirections.South, OrdinalDirections.North, OrdinalDirections.West, OrdinalDirections.Down, OrdinalDirections.East, OrdinalDirections.Up },
                // West 270
                new OrdinalDirections[] { OrdinalDirections.East, OrdinalDirections.West, OrdinalDirections.South, OrdinalDirections.Down, OrdinalDirections.North, OrdinalDirections.Up }
            }
        };

        /// <summary>
        /// The UV rotation to apply for each direction under a given rotation axis and rotation
        /// </summary>
        private static readonly int[][][] ROT_MAP = new int[][][]
        {
            // Up
            new int[][]
            {
                // Up 0
                new int[] { 270, 90, 0, 0, 0, 0 },
                // Up 90
                new int[] { 0, 0, 0, 0, 0, 0 },
                // Up 180
                new int[] { 90, 270, 0, 0, 0, 0 },
                // Up 270
                new int[] { 180, 180, 0, 0, 0, 0 }
            },
            // Down
            new int[][]
            {
                // Down 0
                new int[] { 270, 90, 180, 180, 180, 180 },
                // Down 90
                new int[] { 0, 0, 180, 180, 180, 180 },
                // Down 180
                new int[] { 90, 270, 180, 180, 180, 180 },
                // Down 270
                new int[] { 180, 180, 180, 180, 180, 180 }
            },
            // North
            new int[][]
            {
                // North 0
                new int[] { 270, 90, 0, 90, 0, 270 },
                // North 90
                new int[] { 270, 90, 270, 90, 90, 270 },
                // North 180
                new int[] { 270, 90, 180, 90, 180, 270 },
                // North 270
                new int[] { 270, 90, 90, 90, 270, 270 }
            },
            // East
            new int[][]
            {
                // East 0
                new int[] { 0, 0, 270, 0, 90, 0 },
                // East 90
                new int[] { 0, 0, 270, 270, 90, 90 },
                // East 180
                new int[] { 0, 0, 270, 180, 90, 180 },
                // East 270
                new int[] { 0, 0, 270, 90, 90, 270 }
            },
            // South
            new int[][]
            {
                // West 0
                new int[] { 90, 270, 0, 270, 0, 90 },
                // West 90
                new int[] { 90, 270, 270, 270, 90, 90 },
                // West 180
                new int[] { 90, 270, 180, 270, 180, 90 },
                // West 270
                new int[] { 90, 270, 90, 270, 270, 90 }
            },
            // West
            new int[][]
            {
                // South 0
                new int[] { 180, 180, 90, 0, 270, 0 },
                // South 90
                new int[] { 180, 180, 90, 90, 270, 270 },
                // South 180
                new int[] { 180, 180, 90, 180, 270, 180 },
                // South 270
                new int[] { 180, 180, 90, 270, 270, 90 }
            }
        };

        #endregion

        /// <summary>
        /// Convert from a direction to a UV rotation, given the current up axis and amount rotated around it
        /// </summary>
        /// <param name="dir">The direction to convert</param>
        /// <param name="currUp">The current up axis</param>
        /// <param name="rotAmount">The amount rotated around the up axis</param>
        /// <returns></returns>
        public static float DirectionToRotation(OrdinalDirections dir, OrdinalDirections currUp, int rotAmount)
        {
            return ROT_MAP[(int)currUp][rotAmount / 90][(int)dir];
        }

        /// <summary>
        /// Convert from a global direction to a localized rotation, given the current up axis and amount rotated around it
        /// </summary>
        /// <param name="dir">The direction to convert</param>
        /// <param name="currUp">The current up axis</param>
        /// <param name="rotAmount">The amount rotated around the up axis</param>
        /// <returns></returns>
        public static OrdinalDirections GlobalToLocalDirection(OrdinalDirections dir, OrdinalDirections currUp, int rotAmount)
        {
            return DIR_MAP[(int)currUp][rotAmount / 90][(int)dir];
        }

        /// <summary>
        /// Convert the given current up axis and amount rotated around it to a Quaternion rotation
        /// </summary>
        /// <param name="currUp">The current up axis</param>
        /// <param name="rotAmount">The amount rotated around the up axis</param>
        /// /// <returns></returns>
        public static Quaternion DirectionToEulerRotation(OrdinalDirections currUp, int rotAmount)
        {
            int a =
                (rotAmount == 0) ? 1 :
                (rotAmount == 180) ? -1 :
                0,
                b =
                (rotAmount == 90) ? 1 :
                (rotAmount == 270) ? -1 :
                0;
            switch (currUp)
            {
                case OrdinalDirections.Up:
                    return Quaternion.LookRotation(new Vector3(b, 0, a), Vector3.up);
                case OrdinalDirections.Down:
                    return Quaternion.LookRotation(new Vector3(-b, 0, a), Vector3.down);
                case OrdinalDirections.North:
                    return Quaternion.LookRotation(new Vector3(b, a, 0), Vector3.forward);
                case OrdinalDirections.East:
                    return Quaternion.LookRotation(new Vector3(0, a, -b), Vector3.right);
                case OrdinalDirections.South:
                    return Quaternion.LookRotation(new Vector3(-b, a, 0), Vector3.back);
                case OrdinalDirections.West:
                    return Quaternion.LookRotation(new Vector3(0, a, b), Vector3.left);
                default:
                    throw new System.Exception("Unknown Direction");
            }
        }
    }
}
