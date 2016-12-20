namespace Voxic.Voxels
{
    public struct VoxelMetaData
    {
        public VoxelData VoxelData { get; private set; }

        public VoxelMetaData(VoxelData voxelData)
        {
            VoxelData = voxelData;
        }
    }
}