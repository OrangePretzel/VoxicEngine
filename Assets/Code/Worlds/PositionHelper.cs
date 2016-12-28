using UnityEngine;
using Voxic.Math;

namespace Voxic.Worlds
{
    /// <summary>
    /// Class representing helper for world position transformations
    /// </summary>
    public class PositionHelper
    {
        /// <summary>
        /// The world to which the position helper belongs
        /// </summary>
        private World World;

        /// <summary>
        /// Create a new position helper for the given world
        /// </summary>
        /// <param name="world">The world the position helper belongs to</param>
        public PositionHelper(World world)
        {
            World = world;
        }

        /// <summary>
        /// Convert a world position to a chunk position
        /// </summary>
        /// <param name="worldPos">The world position</param>
        /// <returns></returns>
        public IntVector3 WorldToChunkPosition(IntVector3 worldPos)
        {
            return new IntVector3(
                Mathf.FloorToInt(worldPos.X / (float)World.WorldSettings.ChunkSizeInVoxels),
                Mathf.FloorToInt(worldPos.Y / (float)World.WorldSettings.ChunkSizeInVoxels),
                Mathf.FloorToInt(worldPos.Z / (float)World.WorldSettings.ChunkSizeInVoxels));
        }

        /// <summary>
        /// Convert a world position to a local position
        /// </summary>
        /// <param name="worldPos">The world position</param>
        /// <returns></returns>
        public IntVector3 WorldToLocalPosition(IntVector3 worldPos)
        {
            IntVector3 localPos = worldPos % World.WorldSettings.ChunkSizeInVoxels;
            return new IntVector3(
                localPos.X < 0 ? World.WorldSettings.ChunkSizeInVoxels + localPos.X : localPos.X,
                localPos.Y < 0 ? World.WorldSettings.ChunkSizeInVoxels + localPos.Y : localPos.Y,
                localPos.Z < 0 ? World.WorldSettings.ChunkSizeInVoxels + localPos.Z : localPos.Z);
        }

        /// <summary>
        /// Convert a chunk position to a world position
        /// </summary>
        /// <param name="chunkPos">The chunk position to conver</param>
        /// <returns></returns>
        public IntVector3 ChunkToWorldPosition(IntVector3 chunkPos)
        {
            return chunkPos * World.WorldSettings.ChunkSizeInVoxels;
        }
    }
}