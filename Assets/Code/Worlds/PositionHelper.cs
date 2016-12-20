using UnityEngine;
using Voxic.Math;

namespace Voxic.Worlds
{
    public class PositionHelper
    {
        public World World { get; private set; }

        public PositionHelper(World world)
        {
            World = world;
        }

        public IntVector3 WorldToChunkPosition(IntVector3 worldPos)
        {
            return new IntVector3(
                Mathf.FloorToInt(worldPos.X / (float)World.WorldSettings.ChunkSizeInVoxels),
                Mathf.FloorToInt(worldPos.Y / (float)World.WorldSettings.ChunkSizeInVoxels),
                Mathf.FloorToInt(worldPos.Z / (float)World.WorldSettings.ChunkSizeInVoxels));
        }

        public IntVector3 WorldToLocalPosition(IntVector3 worldPos)
        {
            IntVector3 localPos = worldPos % World.WorldSettings.ChunkSizeInVoxels;
            return new IntVector3(
                localPos.X < 0 ? World.WorldSettings.ChunkSizeInVoxels + localPos.X : localPos.X,
                localPos.Y < 0 ? World.WorldSettings.ChunkSizeInVoxels + localPos.Y : localPos.Y,
                localPos.Z < 0 ? World.WorldSettings.ChunkSizeInVoxels + localPos.Z : localPos.Z);
        }

        public IntVector3 ChunkToWorldPosition(IntVector3 chunkPos)
        {
            return chunkPos * World.WorldSettings.ChunkSizeInVoxels;
        }
    }
}