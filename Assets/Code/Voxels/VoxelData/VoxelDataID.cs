namespace Voxic.Voxels
{
    /// <summary>
    /// Data representing the ID of a VoxelData
    /// </summary>
    public struct VoxelDataID
    {
        /// <summary>
        /// The voxel data's ID. 0 to 65535
        /// </summary>
        public ushort ID;
        /// <summary>
        /// The voxel data's SubID. 0 to 255
        /// </summary>
        public byte SubID;

        /// <summary>
        /// Create a new VoxelDataID
        /// </summary>
        /// <param name="id">The voxel data ID</param>
        public VoxelDataID(ushort id)
        {
            ID = id;
            SubID = 0;
        }

        /// <summary>
        /// Create a new VoxelDataID
        /// </summary>
        /// <param name="id">The voxel data ID</param>
        /// <param name="subID">The voxel data SubID</param>
        public VoxelDataID(ushort id, byte subID)
        {
            ID = id;
            SubID = subID;
        }
    }
}