using System.Collections.Generic;

namespace Voxic.Voxels
{
    public class VoxelDataManager
    {
        public static readonly VoxelData NULL_VOXEL_DATA = new VoxelData(new VoxelDataID(0), "NULL").SetSolidity(false);
        public static VoxelData GrassVoxelData = new VoxelData(new VoxelDataID(0), "Grass").SetTextures(new VoxelDataTextures(0, 3) { UpY = 1 });
        public static VoxelData DirtVoxelData = new VoxelData(new VoxelDataID(0), "Dirt").SetTextures(new VoxelDataTextures(0, 3));
        public static VoxelData StoneVoxelData = new VoxelData(new VoxelDataID(0), "Stone").SetTextures(new VoxelDataTextures(0, 2));

        private Dictionary<VoxelDataID, VoxelData> voxelData;

        public VoxelDataManager()
        {
            voxelData = new Dictionary<VoxelDataID, VoxelData>();
        }
    }
}
