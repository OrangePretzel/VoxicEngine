namespace Voxic.Voxels
{
    public struct VoxelDataTextures
    {
        public byte UpX;
        public byte UpY;
        public byte UpSubMesh;

        public byte DownX;
        public byte DownY;
        public byte DownSubMesh;

        public byte NorthX;
        public byte NorthY;
        public byte NorthSubMesh;

        public byte SouthX;
        public byte SouthY;
        public byte SouthSubMesh;

        public byte EastX;
        public byte EastY;
        public byte EastSubMesh;

        public byte WestX;
        public byte WestY;
        public byte WestSubMesh;

        public VoxelDataTextures(byte x, byte y, byte subMesh = 0)
        {
            UpX = x;
            UpY = y;
            UpSubMesh = subMesh;

            DownX = x;
            DownY = y;
            DownSubMesh = subMesh;

            NorthX = x;
            NorthY = y;
            NorthSubMesh = subMesh;

            SouthX = x;
            SouthY = y;
            SouthSubMesh = subMesh;

            EastX = x;
            EastY = y;
            EastSubMesh = subMesh;

            WestX = x;
            WestY = y;
            WestSubMesh = subMesh;
        }
    }
}