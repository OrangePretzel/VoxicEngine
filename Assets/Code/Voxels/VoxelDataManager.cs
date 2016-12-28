using System.Collections.Generic;

namespace Voxic.Voxels
{
    public class VoxelDataManager
    {
        // TODO: Properly implement this class

        public static readonly VoxelData NULL_VOXEL_DATA = new VoxelData(new VoxelDataID(0), "NULL").SetFaces(false).SetSolidity(false);
        public static VoxelData DirtVoxelData = new VoxelData(new VoxelDataID(0), "Dirt").SetTextures(new VoxelDataTextures(0, 3));
        public static VoxelData StoneVoxelData = new VoxelData(new VoxelDataID(0), "Stone").SetTextures(new VoxelDataTextures(0, 2));
        public static VoxelData GrassVoxelData = new VoxelData(new VoxelDataID(0), "Grass").SetTextures(new VoxelDataTextures(0, 3) { UpY = 1 });
        public static VoxelData RotatoVoxelData = new VoxelData(new VoxelDataID(0), "Rotato").SetTextures(new VoxelDataTextures(0, 3)
        {
            UpY = 1,
            UpX = 0,

            DownX = 1,
            DownY = 0,

            NorthX = 1,
            NorthY = 1,

            SouthX = 1,
            SouthY = 2,

            EastX = 1,
            EastY = 3,

            WestX = 1,
            WestY = 4
        });

        private Dictionary<VoxelDataID, VoxelData> voxelData;

        public VoxelDataManager()
        {
            voxelData = new Dictionary<VoxelDataID, VoxelData>();
        }
    }
}
