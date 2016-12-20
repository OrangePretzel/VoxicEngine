using System;
using System.Collections.Generic;
using UnityEngine;
using Voxic.Chunks;
using Voxic.Math;
using Voxic.Voxels;

namespace Voxic.Worlds
{
    public class World
    {
        public WorldSettings WorldSettings { get; private set; }
        public PositionHelper PosHelper { get; private set; }
        public VoxelDataManager VoxelDataManager { get; private set; }

        private Dictionary<IntVector3, Chunk> loadedChunks;

        public World(WorldSettings settings)
        {
            WorldSettings = settings;
            PosHelper = new PositionHelper(this);
            loadedChunks = new Dictionary<IntVector3, Chunk>();
        }

        public Chunk LoadChunk(IntVector3 chunkPos)
        {
            if (IsChunkLoaded(chunkPos))
                return loadedChunks[chunkPos];

            Chunk chunk = new Chunk(this, chunkPos);
            loadedChunks.Add(chunkPos, chunk);

            return chunk;
        }

        public Chunk LoadChunk(int cX, int cY, int cZ)
        {
            IntVector3 chunkPos = new IntVector3(cX, cY, cZ);
            if (IsChunkLoaded(chunkPos))
                return loadedChunks[chunkPos];

            Chunk chunk = new Chunk(this, chunkPos);
            loadedChunks.Add(chunkPos, chunk);

            return chunk;
        }

        public bool IsChunkLoaded(int cX, int cY, int cZ)
        {
            return loadedChunks.ContainsKey(new IntVector3(cX, cY, cZ));
        }

        public bool IsChunkLoaded(IntVector3 chunkPos)
        {
            return loadedChunks.ContainsKey(chunkPos);
        }

        public bool IsInWorldBoundary(IntVector3 worldPosition)
        {
            return true;
        }

        public bool IsSolid(IntVector3 worldPosition, OrdinalDirections dir)
        {
            if (!IsInWorldBoundary(worldPosition))
                return false;

            IntVector3 chunkPos = PosHelper.WorldToChunkPosition(worldPosition);
            if (!IsChunkLoaded(chunkPos))
                return false;
            else
                return loadedChunks[chunkPos].IsSolid(PosHelper.WorldToLocalPosition(worldPosition), dir);
        }
    }
}