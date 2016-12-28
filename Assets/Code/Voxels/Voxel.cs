using UnityEngine;
using Voxic.Chunks;
using Voxic.Math;
using Voxic.Meshing;

namespace Voxic.Voxels
{
    /// <summary>
    /// Class representing a single voxel in the world
    /// </summary>
    public class Voxel
    {
        /// <summary>
        /// The inner voxel data
        /// </summary>
        protected VoxelData VoxelData { get; private set; }

        /// <summary>
        /// Create a new voxel with the given inner data
        /// </summary>
        /// <param name="voxelData">The inner voxel data</param>
        public Voxel(VoxelData voxelData)
        {
            VoxelData = voxelData;
        }

        /// <summary>
        /// Add mesh data to the given mesh for a voxel in the given chunk at the given localPosition. Renders at the given position with the given size
        /// </summary>
        /// <param name="addToMeshData">The mesh to add the voxel to</param>
        /// <param name="chunk">The chunk the voxel is in</param>
        /// <param name="localPosition">The position in the chunk of the voxel</param>
        /// <param name="position">The position in the world to render at</param>
        /// <param name="voxelSize">The size of the voxel</param>
        public virtual void AddMeshData(MeshData addToMeshData, Chunk chunk, IntVector3 localPosition, Vector3 position, float voxelSize)
        {
            // Ensure we have faces
            if (!VoxelData.Faces.HasAny)
                return;

            // Gather visibility data
            OrdinalBoolean visibility = new OrdinalBoolean();
            visibility.IsUp = !chunk.IsSolid(localPosition.X, localPosition.Y + 1, localPosition.Z, OrdinalDirections.Down);
            visibility.IsDown = !chunk.IsSolid(localPosition.X, localPosition.Y - 1, localPosition.Z, OrdinalDirections.Up);
            visibility.IsNorth = !chunk.IsSolid(localPosition.X, localPosition.Y, localPosition.Z + 1, OrdinalDirections.South);
            visibility.IsSouth = !chunk.IsSolid(localPosition.X, localPosition.Y, localPosition.Z - 1, OrdinalDirections.North);
            visibility.IsEast = !chunk.IsSolid(localPosition.X + 1, localPosition.Y, localPosition.Z, OrdinalDirections.West);
            visibility.IsWest = !chunk.IsSolid(localPosition.X - 1, localPosition.Y, localPosition.Z, OrdinalDirections.East);

            // Create the actual data
            BlockMesher.CreateSimpleBlockMeshData(addToMeshData, position, visibility, VoxelData, voxelSize);
        }

        /// <summary>
        /// Returns true if the face in the given direction is solid
        /// </summary>
        /// <param name="dir">The direction of the face</param>
        /// <returns></returns>
        public virtual bool IsSolid(OrdinalDirections dir)
        {
            return VoxelData.IsSolid(dir);
        }
    }
}