namespace Voxic.VoxelObjects
{
    /// <summary>
    /// Data representing the settings used to create/render a voxel object
    /// </summary>
    public struct VoxelObjectSettings
    {
        /// <summary>
        /// The size of a chunk in the number of voxels in each dimension
        /// </summary>
        public int ChunkSizeInVoxels { get; private set; }
        /// <summary>
        /// The size of a 1:1 rendered voxel in object units
        /// </summary>
        public float VoxelSizeInUnits { get; private set; }
        /// <summary>
        /// Half the size of a 1:1 rendered voxel in object units
        /// </summary>
        public float HalfVoxelSizeInUnits { get; private set; }

        /// <summary>
        /// Create new voxel object settings
        /// </summary>
        /// <param name="chunkSizeInVoxels">The size of a chunk in the number of voxels in each dimension (default 16)</param>
        /// <param name="voxelSizeInUnits">The size of a 1:1 rendered voxel in object units (default 1)</param>
        public VoxelObjectSettings(int chunkSizeInVoxels = 16, float voxelSizeInUnits = 1f)
        {
            ChunkSizeInVoxels = chunkSizeInVoxels;
            VoxelSizeInUnits = voxelSizeInUnits;
            HalfVoxelSizeInUnits = voxelSizeInUnits / 2;
        }
    }
}