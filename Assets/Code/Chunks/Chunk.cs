using UnityEngine;
using Voxic.Math;
using Voxic.Voxels;
using Voxic.VoxelObjects;

namespace Voxic.Chunks
{
    /// <summary>
    /// Class representing chunks of voxel data within a voxel object
    /// </summary>
    public class Chunk
    {
        /// <summary>
        /// The voxel object the chunk belongs to
        /// </summary>
        public VoxelObject VoxelObject { get; private set; }
        /// <summary>
        /// The position of the chunk in the voxel object
        /// </summary>
        public IntVector3 ChunkPosition { get; private set; }

        private Voxel[,,] voxels;
        /// <summary>
        /// The chunks voxels
        /// </summary>
        public Voxel[,,] Voxels
        {
            // TODO: Consider making this readonly
            get { return voxels; }
        }

        #region Constructor

        /// <summary>
        /// Create a new chunk for the given voxel object at the given chunk position
        /// </summary>
        /// <param name="voxObj">The voxel object</param>
        /// <param name="chunkPosition">The position of the chunk</param>
        public Chunk(VoxelObject voxObj, IntVector3 chunkPosition)
        {
            VoxelObject = voxObj;
            ChunkPosition = chunkPosition;

            // Create voxels
            CreateVoxels();
        }

        /// <summary>
        /// Create an set of empty voxels
        /// </summary>
        private void CreateVoxels()
        {
            int chunkSize = VoxelObject.Settings.ChunkSizeInVoxels;
            voxels = new Voxel[VoxelObject.Settings.ChunkSizeInVoxels, VoxelObject.Settings.ChunkSizeInVoxels, VoxelObject.Settings.ChunkSizeInVoxels];
            for (int i = 0; i < chunkSize; i++)
                for (int k = 0; k < chunkSize; k++)
                    for (int j = 0; j < chunkSize; j++)
                    {
                        Voxels[i, j, k] = new Voxel(VoxelDataManager.NULL_VOXEL_DATA);
                    }

			// TODO: Extract object generation from this method
			DoChunkyThings(chunkSize);
			//DoTestyThings();
        }

        private void DoTestyThings()
        {
            for (int i = 0; i < 6; i++)
                for (int k = 0; k < 4; k++)
                {
                    var voxW = new ComplexVoxel(VoxelDataManager.RotatoVoxelData, VoxelStyle.Wedge);
                    var voxB = new ComplexVoxel(VoxelDataManager.RotatoVoxelData, VoxelStyle.Block);
                    voxW.OrientRotateVoxel((OrdinalDirections)i, 90 * k);
                    voxB.OrientRotateVoxel((OrdinalDirections)i, 90 * k);
                    Voxels[i * 2, 15, k * 2] = voxW;
                    Voxels[i * 2, 13, k * 2] = voxB;
                }
        }

        private void DoChunkyThings(int chunkSize)
        {
            int[,] hMap = new int[chunkSize, chunkSize];
            int[,] dMap = new int[chunkSize, chunkSize];
            for (int i = 0; i < chunkSize; i++)
                for (int k = 0; k < chunkSize; k++)
                {
                    int h = UnityEngine.Random.Range(chunkSize / 2, chunkSize);
                    int a = Mathf.RoundToInt(Mathf.Sqrt((i - chunkSize / 2f) * (i - chunkSize / 2f)));
                    int b = Mathf.RoundToInt(Mathf.Sqrt((k - chunkSize / 2f) * (k - chunkSize / 2f)));
                    int d = UnityEngine.Random.Range((a + b) / 2 - 1, chunkSize / 2 + 5);
                    hMap[i, k] = h;
                    dMap[i, k] = d;
                }

            for (int passes = 0; passes < 3; passes++)
            {
                int[,] smoothHMap = new int[chunkSize, chunkSize];
                int[,] smoothDMap = new int[chunkSize, chunkSize];
                for (int i = 0; i < chunkSize; i++)
                    for (int k = 0; k < chunkSize; k++)
                    {
                        float smoothH = 0;
                        float smoothD = 0;
                        int c = 0;
                        for (int x = -1; x <= 1; x++)
                            for (int y = -1; y <= 1; y++)
                            {
                                if (IsInChunk(i + x, 0, k + y))
                                {
                                    smoothH += hMap[i + x, k + y];
                                    smoothD += dMap[i + x, k + y];
                                    c++;
                                }
                            }

                        smoothH /= c;
                        smoothHMap[i, k] = Mathf.RoundToInt(smoothH);

                        smoothD /= c;
                        smoothDMap[i, k] = Mathf.RoundToInt(smoothD);
                    }
                hMap = smoothHMap;
                if (passes < 1)
                    dMap = smoothDMap;
            }

            for (int i = 0; i < chunkSize; i++)
                for (int k = 0; k < chunkSize; k++)
                    for (int j = 0; j < chunkSize; j++)
                    {
                        ComplexVoxel vox = null;
                        int h = hMap[i, k];
                        int d = dMap[i, k];
                        if (j == h && j >= d)
                            vox = new ComplexVoxel(VoxelDataManager.GrassVoxelData);
                        else if (j < h && j >= h - 2 && j >= d)
                            vox = new ComplexVoxel(VoxelDataManager.DirtVoxelData);
                        else if (j < h - 2 && j >= d)
                            vox = new ComplexVoxel(VoxelDataManager.StoneVoxelData);
                        else
                            vox = new ComplexVoxel(VoxelDataManager.NULL_VOXEL_DATA);

                        vox.OrientRotateVoxel((OrdinalDirections)(Random.Range(0, 6)), 90 * Random.Range(0, 4));
                        if (Random.Range(0, 3) == 0)
                            vox.SetStyle(VoxelStyle.Wedge);
                        Voxels[i, j, k] = vox;
                    }
        }

        #endregion

        #region Is Solid

        /// <summary>
        /// Returns true if the face of the voxel at the given position is solid
        /// </summary>
        /// <param name="localPos">The local position of the voxel</param>
        /// <param name="dir">The direction of the face</param>
        /// <returns></returns>
        public bool IsSolid(IntVector3 localPos, OrdinalDirections dir)
        {
            if (IsInChunk(localPos.X, localPos.Y, localPos.Z))
            {
                return voxels[localPos.X, localPos.Y, localPos.Z].IsSolid(dir);
            }
            else
            {
                return VoxelObject.IsSolid(VoxelObject.PosHelper.ChunkToObjectPosition(ChunkPosition) + localPos, dir);
            }
        }

        /// <summary>
        /// Returns true if the face of the voxel at the given position is solid
        /// </summary>
        /// <param name="lX">The local x coordinate</param>
        /// <param name="lY">The local y coordinate</param>
        /// <param name="lY">The local z coordinate</param>
        /// <param name="dir">The direction of the face</param>
        /// <returns></returns>
        public bool IsSolid(int lX, int lY, int lZ, OrdinalDirections dir)
        {
            if (IsInChunk(lX, lY, lZ))
            {
                return voxels[lX, lY, lZ].IsSolid(dir);
            }
            else
            {
                return VoxelObject.IsSolid(VoxelObject.PosHelper.ChunkToObjectPosition(ChunkPosition) + new IntVector3(lX, lY, lZ), dir);
            }
        }

        #endregion

        #region Is In Chunk

        /// <summary>
        /// Returns true if the given local position is inside this chunk
        /// </summary>
        /// <param name="lX">The local x coordinate</param>
        /// <param name="lY">The local y coordinate</param>
        /// <param name="lZ">The local z coordinate</param>
        /// <returns></returns>
        public bool IsInChunk(int lX, int lY, int lZ)
        {
            return (lX >= 0 && lX < VoxelObject.Settings.ChunkSizeInVoxels) && (lY >= 0 && lY < VoxelObject.Settings.ChunkSizeInVoxels) && (lZ >= 0 && lZ < VoxelObject.Settings.ChunkSizeInVoxels);
        }

        /// <summary>
        /// Returns true if the given local position is inside this chunk
        /// </summary>
        /// <param name="localPosition">The position to check</param>
        /// <returns></returns>
        public bool IsInChunk(IntVector3 localPosition)
        {
            return (localPosition.X >= 0 && localPosition.X < VoxelObject.Settings.ChunkSizeInVoxels) && (localPosition.Y >= 0 && localPosition.Y < VoxelObject.Settings.ChunkSizeInVoxels) && (localPosition.Z >= 0 && localPosition.Z < VoxelObject.Settings.ChunkSizeInVoxels);
        }

        #endregion
    }
}
