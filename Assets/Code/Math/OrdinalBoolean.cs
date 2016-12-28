namespace Voxic.Math
{
    /// <summary>
    /// Data to represent boolean data for each of the 6 ordinal directions
    /// </summary>
    public struct OrdinalBoolean
    {
        /// <summary>
        /// Returns true if north is true
        /// </summary>
        public bool IsNorth;
        /// <summary>
        /// Returns true if south is true
        /// </summary>
        public bool IsSouth;
        /// <summary>
        /// Returns true if east is true
        /// </summary>
        public bool IsEast;
        /// <summary>
        /// Returns true if west is true
        /// </summary>
        public bool IsWest;
        /// <summary>
        /// Returns true if up is true
        /// </summary>
        public bool IsUp;
        /// <summary>
        /// Returns true if down is true
        /// </summary>
        public bool IsDown;

        /// <summary>
        /// Returns true if any direction is true
        /// </summary>
        public bool HasAny
        {
            get { return IsNorth || IsSouth || IsUp || IsDown || IsEast || IsWest; }
        }

        /// <summary>
        /// Create a new Ordinal Boolean
        /// </summary>
        /// <param name="isVisible">The default value</param>
        public OrdinalBoolean(bool isVisible)
        {
            IsNorth = isVisible;
            IsSouth = isVisible;
            IsEast = isVisible;
            IsWest = isVisible;
            IsUp = isVisible;
            IsDown = isVisible;
        }

        /// <summary>
        /// Get the boolean value for the given direction
        /// </summary>
        /// <param name="dir">The direction to get the boolean for</param>
        /// <returns></returns>
        public bool GetBoolForDir(OrdinalDirections dir)
        {
            switch (dir)
            {
                case OrdinalDirections.Up:
                    return IsUp;
                case OrdinalDirections.Down:
                    return IsDown;
                case OrdinalDirections.North:
                    return IsNorth;
                case OrdinalDirections.East:
                    return IsEast;
                case OrdinalDirections.South:
                    return IsSouth;
                case OrdinalDirections.West:
                    return IsWest;
                default:
                    return false;
            }
        }
    }
}
