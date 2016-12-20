namespace Voxic.Voxels
{
    public struct VoxelDataID
    {
        public short ID;
        public byte SubID;

        public VoxelDataID(short id)
        {
            ID = id;
            SubID = 0;
        }

        public VoxelDataID(short id, byte subID)
        {
            ID = id;
            SubID = subID;
        }
    }
}