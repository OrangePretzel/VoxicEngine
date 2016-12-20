namespace Voxic.Worlds
{
    public struct WorldSettings
    {
        public int ChunkSizeInVoxels { get; private set; }
        public float VoxelSizeInUnits { get; private set; }
        public float HalfVoxelSizeInUnits { get; private set; }

        public WorldSettings(int chunkSizeInVoxels = 16, float voxelSizeInUnits = 1f)
        {
            ChunkSizeInVoxels = chunkSizeInVoxels;
            VoxelSizeInUnits = voxelSizeInUnits;
            HalfVoxelSizeInUnits = voxelSizeInUnits / 2;
        }
    }
}