namespace Voxic.Voxels
{
    public struct VoxelDataSolidity
    {
        public bool NorthIsSolid;
        public bool SouthIsSolid;
        public bool EastIsSolid;
        public bool WestIsSolid;
        public bool UpIsSolid;
        public bool DownIsSolid;

        public bool HasSolidFace
        {
            get { return NorthIsSolid || SouthIsSolid || UpIsSolid || DownIsSolid || EastIsSolid || WestIsSolid; }
        }

        public VoxelDataSolidity(bool isSolid)
        {
            NorthIsSolid = isSolid;
            SouthIsSolid = isSolid;
            EastIsSolid = isSolid;
            WestIsSolid = isSolid;
            UpIsSolid = isSolid;
            DownIsSolid = isSolid;
        }
    }
}