﻿using System;
using UnityEngine;
using Voxic.Math;
using Voxic.Voxels;
using Voxic.Worlds;

namespace Voxic.Chunks
{
    /// <summary>
    /// Class representing chunks of voxel data within the world
    /// </summary>
    public class Chunk
    {
        /// <summary>
        /// The world the chunk belongs to
        /// </summary>
        public World World { get; private set; }
        /// <summary>
        /// The position of the chunk in the world
        /// </summary>
        public IntVector3 ChunkPosition { get; private set; }

        private VoxelMetaData[,,] voxels;
        /// <summary>
        /// The chunks voxels
        /// </summary>
        public VoxelMetaData[,,] Voxels
        {
            // TODO: Consider making this readonly
            get { return voxels; }
        }

        #region Constructor

        /// <summary>
        /// Create a new chunk for the given world at the given chunk position
        /// </summary>
        /// <param name="world">The world</param>
        /// <param name="chunkPosition">The position of the chunk</param>
        public Chunk(World world, IntVector3 chunkPosition)
        {
            World = world;
            ChunkPosition = chunkPosition;

            // Create voxels
            CreateVoxels();
        }

        /// <summary>
        /// Create an set of empty voxels
        /// </summary>
        private void CreateVoxels()
        {
            int chunkSize = World.WorldSettings.ChunkSizeInVoxels;
            voxels = new VoxelMetaData[World.WorldSettings.ChunkSizeInVoxels, World.WorldSettings.ChunkSizeInVoxels, World.WorldSettings.ChunkSizeInVoxels];
            for (int i = 0; i < chunkSize; i++)
                for (int k = 0; k < chunkSize; k++)
                    for (int j = 0; j < chunkSize; j++)
                    {
                        Voxels[i, j, k] = new VoxelMetaData(VoxelDataManager.NULL_VOXEL_DATA);
                    }

            // TODO: Extract world generation from this method
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
                        int h = hMap[i, k];
                        int d = dMap[i, k];
                        if (j == h && j >= d)
                            Voxels[i, j, k] = new VoxelMetaData(VoxelDataManager.GrassVoxelData);
                        else if (j < h && j >= h - 2 && j >= d)
                            Voxels[i, j, k] = new VoxelMetaData(VoxelDataManager.DirtVoxelData);
                        else if (j < h - 2 && j >= d)
                            Voxels[i, j, k] = new VoxelMetaData(VoxelDataManager.StoneVoxelData);
                        else
                            Voxels[i, j, k] = new VoxelMetaData(VoxelDataManager.NULL_VOXEL_DATA);
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
                return voxels[localPos.X, localPos.Y, localPos.Z].VoxelData.IsSolid(dir);
            }
            else
            {
                return World.IsSolid(World.PosHelper.ChunkToWorldPosition(ChunkPosition) + localPos, dir);
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
                return voxels[lX, lY, lZ].VoxelData.IsSolid(dir);
            }
            else
            {
                return World.IsSolid(World.PosHelper.ChunkToWorldPosition(ChunkPosition) + new IntVector3(lX, lY, lZ), dir);
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
            return (lX >= 0 && lX < World.WorldSettings.ChunkSizeInVoxels) && (lY >= 0 && lY < World.WorldSettings.ChunkSizeInVoxels) && (lZ >= 0 && lZ < World.WorldSettings.ChunkSizeInVoxels);
        }

        #endregion
    }
}