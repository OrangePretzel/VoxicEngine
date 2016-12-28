using UnityEngine;
using Voxic.Math;

namespace Voxic.VoxelObjects
{
    /// <summary>
    /// Class representing helper for object position transformations
    /// </summary>
    public class PositionHelper
    {
        /// <summary>
        /// The object to which the position helper belongs
        /// </summary>
        private VoxelObject voxelObject;

        /// <summary>
        /// Create a new position helper for the given voxel object
        /// </summary>
        /// <param name="obj">The object the position helper belongs to</param>
        public PositionHelper(VoxelObject obj)
        {
            voxelObject = obj;
        }

        /// <summary>
        /// Convert a object position to a chunk position
        /// </summary>
        /// <param name="objPos">The object position</param>
        /// <returns></returns>
        public IntVector3 ObjectToChunkPosition(IntVector3 objPos)
        {
            return new IntVector3(
                Mathf.FloorToInt(objPos.X / (float)voxelObject.Settings.ChunkSizeInVoxels),
                Mathf.FloorToInt(objPos.Y / (float)voxelObject.Settings.ChunkSizeInVoxels),
                Mathf.FloorToInt(objPos.Z / (float)voxelObject.Settings.ChunkSizeInVoxels));
        }

        /// <summary>
        /// Convert a object position to a local position
        /// </summary>
        /// <param name="objPos">The object position</param>
        /// <returns></returns>
        public IntVector3 ObjectToLocalPosition(IntVector3 objPos)
        {
            IntVector3 localPos = objPos % voxelObject.Settings.ChunkSizeInVoxels;
            return new IntVector3(
                localPos.X < 0 ? voxelObject.Settings.ChunkSizeInVoxels + localPos.X : localPos.X,
                localPos.Y < 0 ? voxelObject.Settings.ChunkSizeInVoxels + localPos.Y : localPos.Y,
                localPos.Z < 0 ? voxelObject.Settings.ChunkSizeInVoxels + localPos.Z : localPos.Z);
        }

        /// <summary>
        /// Convert a chunk position to a object position
        /// </summary>
        /// <param name="chunkPos">The chunk position to convert</param>
        /// <returns></returns>
        public IntVector3 ChunkToObjectPosition(IntVector3 chunkPos)
        {
            return chunkPos * voxelObject.Settings.ChunkSizeInVoxels;
        }
    }
}