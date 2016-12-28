using UnityEngine;
using Voxic.Extensions.Vector;
using Voxic.Math;
using Voxic.Meshing;

namespace Voxic.Voxels
{
    // TODO: Implement submesh support

    /// <summary>
    /// Class of helpers for adding voxel mesh data
    /// </summary>
    public static class BlockMesher
    {
        // TODO: Determine if the voxel size needs to be used as a factor in the position
        // Estimated equation: position = position * 2 * voxelSize

        #region Lookup Tables

        public static int[,,][] WEDGE_MESH_MAP = new int[,,][]
        {
            // Up
            {
                // 0
                {
                    // Up (Slope)
                    new int[] { -1, -1, 1, 1, -1, 1, 1, 1, -1, -1, 1, -1, 0, 1, 2, 3 },
                    // Down
                    new int[] { -1, -1, -1, 1, -1, -1, 1, -1, 1, -1, -1, 1, 0, 1, 2, 3 },
                    // North (Not used)
                    new int[] { },
                    // East
                    new int[] { 1, -1, -1, 1, 1, -1, 1, -1, 1, 0, 1, 2 },
                    // South
                    new int[] { -1, -1, -1, -1, 1, -1, 1, 1, -1, 1, -1, -1, 0, 1, 2, 3 },
                    // West
                    new int[] { -1, -1, 1, -1, 1, -1, -1, -1, -1, 0, 1, 2 }
                },
                // 90
                {
                    // Up (Slope)
                    new int[] { -1, 1, 1, 1, -1, 1, 1, -1, -1, -1, 1, -1, 0, 1, 2, 3 },
                    // Down
                    new int[] { -1, -1, -1, 1, -1, -1, 1, -1, 1, -1, -1, 1, 0, 1, 2, 3 },
                    // North
                    new int[] { 1, -1, 1, -1, 1, 1, -1, -1, 1, 0, 1, 2 },
                    // East (Not used)
                    new int[] { },
                    // South
                    new int[] { -1, -1, -1, -1, 1, -1, 1, -1, -1, 0, 1, 2 },
                    // West
                    new int[] { -1, -1, 1, -1, 1, 1, -1, 1, -1, -1, -1, -1, 0, 1, 2, 3 }
                },
                // 180
                {
                    // Up (Slope)
                    new int[] { -1, 1, 1, 1, 1, 1, 1, -1, -1, -1, -1, -1, 0, 1, 2, 3 },
                    // Down
                    new int[] { -1, -1, -1, 1, -1, -1, 1, -1, 1, -1, -1, 1, 0, 1, 2, 3 },
                    // North
                    new int[] { 1, -1, 1, 1, 1, 1, -1, 1, 1, -1, -1, 1, 0, 1, 2, 3 },
                    // East
                    new int[] { 1, -1, -1, 1, 1, 1, 1, -1, 1, 0, 1, 2 },
                    // South (Not used)
                    new int[] { },
                    // West
                    new int[] { -1, -1, 1, -1, 1, 1, -1, -1, -1, 0, 1, 2 }
                },
                // 270
                {
                    // Up (Slope)
                    new int[] { -1, -1, 1, 1, 1, 1, 1, 1, -1, -1, -1, -1, 0, 1, 2, 3 },
                    // Down
                    new int[] { -1, -1, -1, 1, -1, -1, 1, -1, 1, -1, -1, 1, 0, 1, 2, 3 },
                    // North
                    new int[] { 1, -1, 1, 1, 1, 1, -1, -1, 1, 0, 1, 2 },
                    // East
                    new int[] { 1, -1, -1, 1, 1, -1, 1, 1, 1, 1, -1, 1, 0, 1, 2, 3 },
                    // South
                    new int[] { -1, -1, -1, 1, 1, -1, 1, -1, -1, 0, 1, 2 },
                    // West (Not used)
                    new int[] { }
                }
            },
            // Down
            {
                // 0
                {
                    // Up
                    new int[] { -1, 1, 1, 1, 1, 1, 1, 1, -1, -1, 1, -1, 0, 1, 2, 3 },
                    // Down (Slope)
                    new int[] { -1, -1, -1, 1, -1, -1, 1, 1, 1, -1, 1, 1, 0, 1, 2, 3 },
                    // North (Not used)
                    new int[] { },
                    // East
                    new int[] { 1, -1, -1, 1, 1, -1, 1, 1, 1, 0, 1, 2 },
                    // South
                    new int[] { -1, -1, -1, -1, 1, -1, 1, 1, -1, 1, -1, -1, 0, 1, 2, 3 },
                    // West
                    new int[] { -1, 1, 1, -1, 1, -1, -1, -1, -1, 0, 1, 2 }
                },
                // 90
                {
                    // Up
                    new int[] { -1, 1, 1, 1, 1, 1, 1, 1, -1, -1, 1, -1, 0, 1, 2, 3 },
                    // Down (Slope)
                    new int[] { -1, -1, -1, 1, 1, -1, 1, 1, 1, -1, -1, 1, 0, 1, 2, 3 },
                    // North
                    new int[] { 1, 1, 1, -1, 1, 1, -1, -1, 1, 0, 1, 2 },
                    // East (Not used)
                    new int[] { },
                    // South
                    new int[] { -1, -1, -1, -1, 1, -1, 1, 1, -1, 0, 1, 2 },
                    // West
                    new int[] { -1, -1, 1, -1, 1, 1, -1, 1, -1, -1, -1, -1, 0, 1, 2, 3 }
                },
                // 180
                {
                    // Up
                    new int[] { -1, 1, 1, 1, 1, 1, 1, 1, -1, -1, 1, -1, 0, 1, 2, 3 },
                    // Down (Slope)
                    new int[] { -1, 1, -1, 1, 1, -1, 1, -1, 1, -1, -1, 1, 0, 1, 2, 3 },
                    // North
                    new int[] { 1, -1, 1, 1, 1, 1, -1, 1, 1, -1, -1, 1, 0, 1, 2, 3 },
                    // East
                    new int[] { 1, 1, -1, 1, 1, 1, 1, -1, 1, 0, 1, 2 },
                    // South (Not used)
                    new int[] { },
                    // West
                    new int[] { -1, -1, 1, -1, 1, 1, -1, 1, -1, 0, 1, 2 }
                },
                // 270
                {
                    // Up
                    new int[] { -1, 1, 1, 1, 1, 1, 1, 1, -1, -1, 1, -1, 0, 1, 2, 3 },
                    // Down (Slope)
                    new int[] { -1, 1, -1, 1, -1, -1, 1, -1, 1, -1, 1, 1, 0, 1, 2, 3 },
                    // North
                    new int[] { 1, -1, 1, 1, 1, 1, -1, 1, 1, 0, 1, 2 },
                    // East
                    new int[] { 1, -1, -1, 1, 1, -1, 1, 1, 1, 1, -1, 1, 0, 1, 2, 3 },
                    // South
                    new int[] { -1, 1, -1, 1, 1, -1, 1, -1, -1, 0, 1, 2 },
                    // West (Not used)
                    new int[] { }
                }
            },
            // North
            {
                // 0
                {
                    // Up (Not used)
                    new int[] { },
                    // Down
                    new int[] { -1, -1, -1, 1, -1, -1, 1, -1, 1, -1, -1, 1, 0, 1, 2, 3 },
                    // North (Slope)
                    new int[] { 1, -1, 1, 1, 1, -1, -1, 1, -1, -1, -1, 1, 0, 1, 2, 3 },
                    // East
                    new int[] { 1, -1, -1, 1, 1, -1, 1, -1, 1, 0, 1, 2 },
                    // South
                    new int[] { -1, -1, -1, -1, 1, -1, 1, 1, -1, 1, -1, -1, 0, 1, 2, 3 },
                    // West
                    new int[] { -1, -1, 1, -1, 1, -1, -1, -1, -1, 0, 1, 2 }
                },
                // 90
                {
                    // Up
                    new int[] { -1, 1, 1, 1, 1, -1, -1, 1, -1, 0, 1, 2 },
                    // Down
                    new int[] { -1, -1, -1, 1, -1, -1, -1, -1, 1, 0, 1, 2 },
                    // North (Slope)
                    new int[] { 1, -1, -1, 1, 1, -1, -1, 1, 1, -1, -1, 1, 0, 1, 2, 3 },
                    // East (Not used)
                    new int[] { },
                    // South
                    new int[] { -1, -1, -1, -1, 1, -1, 1, 1, -1, 1, -1, -1, 0, 1, 2, 3 },
                    // West
                    new int[] { -1, -1, 1, -1, 1, 1, -1, 1, -1, -1, -1, -1, 0, 1, 2, 3 }
                },
                // 180
                {
                    // Up
                    new int[] { -1, 1, 1, 1, 1, 1, 1, 1, -1, -1, 1, -1, 0, 1, 2, 3 },
                    // Down (Not used)
                    new int[] { },
                    // North (Slope)
                    new int[] { 1, -1, -1, 1, 1, 1, -1, 1, 1, -1, -1, -1, 0, 1, 2, 3 },
                    // East
                    new int[] { 1, -1, -1, 1, 1, -1, 1, 1, 1, 0, 1, 2 },
                    // South
                    new int[] { -1, -1, -1, -1, 1, -1, 1, 1, -1, 1, -1, -1, 0, 1, 2, 3 },
                    // West
                    new int[] { -1, 1, 1, -1, 1, -1, -1, -1, -1, 0, 1, 2 }
                },
                // 270
                {
                    // Up
                    new int[] { 1, 1, 1, 1, 1, -1, -1, 1, -1, 0, 1, 2 },
                    // Down
                    new int[] { -1, -1, -1, 1, -1, -1, 1, -1, 1, 0, 1, 2 },
                    // North (Slope)
                    new int[] { 1, -1, 1, 1, 1, 1, -1, 1, -1, -1, -1, -1, 0, 1, 2, 3 },
                    // East
                    new int[] { 1, -1, -1, 1, 1, -1, 1, 1, 1, 1, -1, 1, 0, 1, 2, 3 },
                    // South
                    new int[] { -1, -1, -1, -1, 1, -1, 1, 1, -1, 1, -1, -1, 0, 1, 2, 3 },
                    // West (Not used)
                    new int[] { }
                }
            },
            // East
            {
                // 0
                {
                    // Up (Not used)
                    new int[] { },
                    // Down
                    new int[] { -1, -1, -1, 1, -1, -1, 1, -1, 1, -1, -1, 1, 0, 1, 2, 3 },
                    // North
                    new int[] { 1, -1, 1, -1, 1, 1, -1, -1, 1, 0, 1, 2 },
                    // East (Slope)
                    new int[] { 1, -1, -1, -1, 1, -1, -1, 1, 1, 1, -1, 1, 0, 1, 2, 3 },
                    // South
                    new int[] { -1, -1, -1, -1, 1, -1, 1, -1, -1, 0, 1, 2 },
                    // West
                    new int[] { -1, -1, 1, -1, 1, 1, -1, 1, -1, -1, -1, -1, 0, 1, 2, 3 }
                },
                // 90
                {
                    // Up
                    new int[] { -1, 1, 1, 1, 1, 1, -1, 1, -1, 0, 1, 2 },
                    // Down
                    new int[] { -1, -1, -1, 1, -1, 1, -1, -1, 1, 0, 1, 2 },
                    // North
                    new int[] { 1, -1, 1, 1, 1, 1, -1, 1, 1, -1, -1, 1, 0, 1, 2, 3 },
                    // East (Slope)
                    new int[] { -1, -1, -1, -1, 1, -1, 1, 1, 1, 1, -1, 1, 0, 1, 2, 3 },
                    // South (Not used)
                    new int[] { },
                    // West
                    new int[] { -1, -1, 1, -1, 1, 1, -1, 1, -1, -1, -1, -1, 0, 1, 2, 3 }
                },
                // 180
                {
                    // Up
                    new int[] { -1, 1, 1, 1, 1, 1, 1, 1, -1, -1, 1, -1, 0, 1, 2, 3 },
                    // Down (Not used)
                    new int[] { },
                    // North
                    new int[] { 1, 1, 1, -1, 1, 1, -1, -1, 1, 0, 1, 2 },
                    // East (Slope)
                    new int[] { -1, -1, -1, 1, 1, -1, 1, 1, 1, -1, -1, 1, 0, 1, 2, 3 },
                    // South
                    new int[] { -1, -1, -1, -1, 1, -1, 1, 1, -1, 0, 1, 2 },
                    // West
                    new int[] { -1, -1, 1, -1, 1, 1, -1, 1, -1, -1, -1, -1, 0, 1, 2, 3 }
                },
                // 270
                {
                    // Up
                    new int[] { -1, 1, 1, 1, 1, -1, -1, 1, -1, 0, 1, 2 },
                    // Down
                    new int[] { -1, -1, -1, 1, -1, -1, -1, -1, 1, 0, 1, 2 },
                    // North (Not used)
                    new int[] { },
                    // East (Slope)
                    new int[] { 1, -1, -1, 1, 1, -1, -1, 1, 1, -1, -1, 1, 0, 1, 2, 3 },
                    // South
                    new int[] { -1, -1, -1, -1, 1, -1, 1, 1, -1, 1, -1, -1, 0, 1, 2, 3 },
                    // West
                    new int[] { -1, -1, 1, -1, 1, 1, -1, 1, -1, -1, -1, -1, 0, 1, 2, 3 }
                }
            },
            // South
            {
                // 0
                {
                    // Up (Not used)
                    new int[] { },
                    // Down
                    new int[] { -1, -1, -1, 1, -1, -1, 1, -1, 1, -1, -1, 1, 0, 1, 2, 3 },
                    // North
                    new int[] { 1, -1, 1, 1, 1, 1, -1, 1, 1, -1, -1, 1, 0, 1, 2, 3 },
                    // East
                    new int[] { 1, -1, -1, 1, 1, 1, 1, -1, 1, 0, 1, 2 },
                    // South (Slope)
                    new int[] { -1, -1, -1, -1, 1, 1, 1, 1, 1, 1, -1, -1, 0, 1, 2, 3 },
                    // West
                    new int[] { -1, -1, 1, -1, 1, 1, -1, -1, -1, 0, 1, 2 }
                },
                // 90
                {
                    // Up
                    new int[] { -1, 1, 1, 1, 1, 1, -1, 1, -1, 0, 1, 2 },
                    // Down
                    new int[] { -1, -1, -1, 1, -1, 1, -1, -1, 1, 0, 1, 2 },
                    // North
                    new int[] { 1, -1, 1, 1, 1, 1, -1, 1, 1, -1, -1, 1, 0, 1, 2, 3 },
                    // East (Not used)
                    new int[] { },
                    // South (Slope)
                    new int[] { -1, -1, -1, -1, 1, -1, 1, 1, 1, 1, -1, 1, 0, 1, 2, 3 },
                    // West
                    new int[] { -1, -1, 1, -1, 1, 1, -1, 1, -1, -1, -1, -1, 0, 1, 2, 3 }
                },
                // 180
                {
                    // Up
                    new int[] { -1, 1, 1, 1, 1, 1, 1, 1, -1, -1, 1, -1, 0, 1, 2, 3 },
                    // Down (Not used)
                    new int[] { },
                    // North
                    new int[] { 1, -1, 1, 1, 1, 1, -1, 1, 1, -1, -1, 1, 0, 1, 2, 3 },
                    // East
                    new int[] { 1, 1, -1, 1, 1, 1, 1, -1, 1, 0, 1, 2 },
                    // South (Slope)
                    new int[] { -1, -1, 1, -1, 1, -1, 1, 1, -1, 1, -1, 1, 0, 1, 2, 3 },
                    // West
                    new int[] { -1, -1, 1, -1, 1, 1, -1, 1, -1, 0, 1, 2 }
                },
                // 270
                {
                    // Up
                    new int[] { -1, 1, 1, 1, 1, 1, 1, 1, -1, 0, 1, 2 },
                    // Down
                    new int[] { 1, -1, -1, 1, -1, 1, -1, -1, 1, 0, 1, 2 },
                    // North
                    new int[] { 1, -1, 1, 1, 1, 1, -1, 1, 1, -1, -1, 1, 0, 1, 2, 3 },
                    // East
                    new int[] { 1, -1, -1, 1, 1, -1, 1, 1, 1, 1, -1, 1, 0, 1, 2, 3 },
                    // South (Slope)
                    new int[] { -1, -1, 1, -1, 1, 1, 1, 1, -1, 1, -1, -1, 0, 1, 2, 3 },
                    // West (Not used)
                    new int[] { }
                }
            },
            // West
            {
                // 0
                {
                    // Up (Not used)
                    new int[] { },
                    // Down
                    new int[] { -1, -1, -1, 1, -1, -1, 1, -1, 1, -1, -1, 1, 0, 1, 2, 3 },
                    // North
                    new int[] { 1, -1, 1, 1, 1, 1, -1, -1, 1, 0, 1, 2 },
                    // East
                    new int[] { 1, -1, -1, 1, 1, -1, 1, 1, 1, 1, -1, 1, 0, 1, 2, 3 },
                    // South
                    new int[] { -1, -1, -1, 1, 1, -1, 1, -1, -1, 0, 1, 2 },
                    // West (Slope)
                    new int[] { -1, -1, 1, 1, 1, 1, 1, 1, -1, -1, -1, -1, 0, 1, 2, 3 }
                },
                // 90
                {
                    // Up
                    new int[] { 1, 1, 1, 1, 1, -1, -1, 1, -1, 0, 1, 2 },
                    // Down
                    new int[] { -1, -1, -1, 1, -1, -1, 1, -1, 1, 0, 1, 2 },
                    // North (Not used)
                    new int[] { },
                    // East
                    new int[] { 1, -1, -1, 1, 1, -1, 1, 1, 1, 1, -1, 1, 0, 1, 2, 3 },
                    // South
                    new int[] { -1, -1, -1, -1, 1, -1, 1, 1, -1, 1, -1, -1, 0, 1, 2, 3 },
                    // West (Slope)
                    new int[] { 1, -1, 1, 1, 1, 1, -1, 1, -1, -1, -1, -1, 0, 1, 2, 3 }
                },
                // 180
                {
                    // Up
                    new int[] { -1, 1, 1, 1, 1, 1, 1, 1, -1, -1, 1, -1, 0, 1, 2, 3 },
                    // Down (Not used)
                    new int[] { },
                    // North
                    new int[] { 1, -1, 1, 1, 1, 1, -1, 1, 1, 0, 1, 2 },
                    // East
                    new int[] { 1, -1, -1, 1, 1, -1, 1, 1, 1, 1, -1, 1, 0, 1, 2, 3 },
                    // South
                    new int[] { -1, 1, -1, 1, 1, -1, 1, -1, -1, 0, 1, 2 },
                    // West (Slope)
                    new int[] { 1, -1, 1, -1, 1, 1, -1, 1, -1, 1, -1, -1, 0, 1, 2, 3 }
                },
                // 270
                {
                    // Up
                    new int[] { -1, 1, 1, 1, 1, 1, 1, 1, -1, 0, 1, 2 },
                    // Down
                    new int[] { 1, -1, -1, 1, -1, 1, -1, -1, 1, 0, 1, 2 },
                    // North
                    new int[] { 1, -1, 1, 1, 1, 1, -1, 1, 1, -1, -1, 1, 0, 1, 2, 3 },
                    // East
                    new int[] { 1, -1, -1, 1, 1, -1, 1, 1, 1, 1, -1, 1, 0, 1, 2, 3 },
                    // South (Not used)
                    new int[] { },
                    // West (Slope)
                    new int[] { -1, -1, 1, -1, 1, 1, 1, 1, -1, 1, -1, -1, 0, 1, 2, 3 }
                }
            }
        };

        #endregion

        /// <summary>
        /// Create a simple block
        /// </summary>
        /// <param name="addToMeshData">The mesh data to add the block to</param>
        /// <param name="position">The position of the block</param>
        /// <param name="visibility">Which sides of the block are visible</param>
        /// <param name="voxelData">The type of voxel to render</param>
        /// <param name="voxelSize">The size of the voxel</param>
        public static void CreateSimpleBlockMeshData(MeshData addToMeshData, Vector3 position, OrdinalBoolean visibility, VoxelData voxelData, float voxelSize)
        {
            OrdinalDirections dir;

            for (int i = 0; i < 6; i++)
            {
                dir = (OrdinalDirections)i;
                // Check if the side is visible and has a face
                if (visibility.GetBoolForDir(dir) && voxelData.Faces.GetBoolForDir(dir))
                {
                    // Mesh Data
                    addToMeshData.AddQuad(position, dir, voxelSize, 0);
                    // Collider Data
                    addToMeshData.AddQuadToCollider(position, dir, voxelSize);

                    // UVs
                    addToMeshData.AddQuadUVs(new Rect(
                        new Vector2(voxelData.Textures.GetX(dir), voxelData.Textures.GetY(dir)) * VoxelDataTextures.UV_SIZE_WITH_PADDING + VoxelDataTextures.TEXTURE_PADDING_OFFSET, VoxelDataTextures.TEXTURE_SIZE),
                        dir);
                }
            }
        }

        /// <summary>
        /// Create a complex block with orientation and rotation
        /// </summary>
        /// <param name="addToMeshData">The mesh data to add the block to</param>
        /// <param name="position">The position of the block</param>
        /// <param name="orientation">The voxel's orientation</param>
        /// <param name="rotAmount">The voxel's rotation around the orientation axis (0, 90, 180, 270)</param>
        /// <param name="visibility">Which sides of the block are visible</param>
        /// <param name="voxelData">The type of voxel to render</param>
        /// <param name="voxelSize">The size of the voxel</param>
        public static void CreateComplexBlockMeshData(MeshData addToMeshData, Vector3 position, OrdinalDirections orientation, int rotAmount, OrdinalBoolean visibility, VoxelData voxelData, float voxelSize)
        {
            float rot;
            Vector2 uvC, uv1, uv2, uv3, uv4;
            OrdinalDirections dirGlob;
            OrdinalDirections dirLoc;

            for (int i = 0; i < 6; i++)
            {
                dirGlob = (OrdinalDirections)i;
                // Check if the side is visible and the localized face is actually a face
                if (visibility.GetBoolForDir(dirGlob) && voxelData.Faces.GetBoolForDir(dirLoc = OrdinalDirectionsHelper.GlobalToLocalDirection(dirGlob, orientation, rotAmount)))
                {
                    rot = OrdinalDirectionsHelper.DirectionToRotation(dirGlob, orientation, rotAmount);

                    // Mesh Data
                    addToMeshData.AddQuad(position, dirGlob, voxelSize, 0);
                    // Collider Data
                    addToMeshData.AddQuadToCollider(position, dirGlob, voxelSize);

                    // UVs
                    uv1 = new Vector2(voxelData.Textures.GetX(dirLoc), voxelData.Textures.GetY(dirLoc)) * VoxelDataTextures.UV_SIZE_WITH_PADDING + VoxelDataTextures.TEXTURE_PADDING_OFFSET;
                    uv2 = uv1 + new Vector2(0, VoxelDataTextures.TEXTURE_DIMENSION);
                    uv3 = uv1 + new Vector2(VoxelDataTextures.TEXTURE_DIMENSION, VoxelDataTextures.TEXTURE_DIMENSION);
                    uv4 = uv1 + new Vector2(VoxelDataTextures.TEXTURE_DIMENSION, 0);

                    // Average UV Pos
                    uvC = new Vector2((uv1.x + uv2.x + uv3.x + uv4.x) / 4, (uv1.y + uv2.y + uv3.y + uv4.y) / 4);

                    // UV Rotation
                    uv1 = (uv1 - uvC).Rotate(rot) + uvC;
                    uv2 = (uv2 - uvC).Rotate(rot) + uvC;
                    uv3 = (uv3 - uvC).Rotate(rot) + uvC;
                    uv4 = (uv4 - uvC).Rotate(rot) + uvC;

                    // UVs
                    addToMeshData.AddUV(uv1);
                    addToMeshData.AddUV(uv2);
                    addToMeshData.AddUV(uv3);
                    addToMeshData.AddUV(uv4);
                }
            }
        }

        /// <summary>
        /// Create a complex wedge with orientation and rotation
        /// </summary>
        /// <param name="addToMeshData">The mesh data to add the block to</param>
        /// <param name="position">The position of the block</param>
        /// <param name="orientation">The voxel's orientation</param>
        /// <param name="rotAmount">The voxel's rotation around the orientation axis (0, 90, 180, 270)</param>
        /// <param name="visibility">Which sides of the block are visible</param>
        /// <param name="voxelData">The type of voxel to render</param>
        /// <param name="voxelSize">The size of the voxel</param>
        public static void CreateComplexWedgeMeshData(MeshData addToMeshData, Vector3 position, OrdinalDirections orientation, int rotAmount, OrdinalBoolean visibility, VoxelData voxelData, float voxelSize)
        {
            // Vertices
            int[] comps;
            Vector3 v1, v2, v3, v4;

            // UVs
            Vector2 uvC;
            Vector2[] uvs = new Vector2[4];
            float rot;

            // General
            bool slopeDone = false;
            OrdinalDirections dirGlob;
            OrdinalDirections dirLoc;

            for (int i = 0; i < 6; i++)
            {
                dirGlob = (OrdinalDirections)i;
                if (visibility.GetBoolForDir(dirGlob) && voxelData.Faces.GetBoolForDir(dirLoc = OrdinalDirectionsHelper.GlobalToLocalDirection(dirGlob, orientation, rotAmount)))
                {
                    // Slope
                    if (!slopeDone && (dirLoc == OrdinalDirections.Up || dirLoc == OrdinalDirections.North || dirLoc == OrdinalDirections.East || dirLoc == OrdinalDirections.West))
                    {
                        comps = WEDGE_MESH_MAP[(int)orientation, rotAmount / 90, (int)orientation];

                        // UVs
                        rot = OrdinalDirectionsHelper.DirectionToRotation(OrdinalDirections.Up, orientation, rotAmount);
                        uvs[0] = new Vector2(voxelData.Textures.GetX(OrdinalDirections.Up), voxelData.Textures.GetY(OrdinalDirections.Up)) * VoxelDataTextures.UV_SIZE_WITH_PADDING + VoxelDataTextures.TEXTURE_PADDING_OFFSET;
                        uvs[1] = uvs[0] + new Vector2(0, VoxelDataTextures.TEXTURE_DIMENSION);
                        uvs[2] = uvs[0] + new Vector2(VoxelDataTextures.TEXTURE_DIMENSION, VoxelDataTextures.TEXTURE_DIMENSION);
                        uvs[3] = uvs[0] + new Vector2(VoxelDataTextures.TEXTURE_DIMENSION, 0);

                        // Average UV Pos
                        uvC = new Vector2((uvs[0].x + uvs[1].x + uvs[2].x + uvs[3].x) / 4, (uvs[0].y + uvs[1].y + uvs[2].y + uvs[3].y) / 4);

                        // UV Rotation
                        uvs[0] = (uvs[0] - uvC).Rotate(rot) + uvC;
                        uvs[1] = (uvs[1] - uvC).Rotate(rot) + uvC;
                        uvs[2] = (uvs[2] - uvC).Rotate(rot) + uvC;
                        uvs[3] = (uvs[3] - uvC).Rotate(rot) + uvC;

                        v1 = new Vector3(comps[0], comps[1], comps[2]) * voxelSize + position;
                        v2 = new Vector3(comps[3], comps[4], comps[5]) * voxelSize + position;
                        v3 = new Vector3(comps[6], comps[7], comps[8]) * voxelSize + position;
                        v4 = new Vector3(comps[9], comps[10], comps[11]) * voxelSize + position;

                        // Mesh Data
                        addToMeshData.AddQuad(v1, v2, v3, v4, 0);
                        // Collider Data
                        addToMeshData.AddQuadToCollider(v1, v2, v3, v4);

                        // UVs
                        addToMeshData.AddUV(uvs[comps[12]]);
                        addToMeshData.AddUV(uvs[comps[13]]);
                        addToMeshData.AddUV(uvs[comps[14]]);
                        addToMeshData.AddUV(uvs[comps[15]]);

                        // Only render the top once
                        slopeDone = true;
                    }

                    // Quad Faces
                    if (dirLoc == OrdinalDirections.South || dirLoc == OrdinalDirections.Down)
                    {
                        comps = WEDGE_MESH_MAP[(int)orientation, rotAmount / 90, (int)dirGlob];

                        // UVs
                        rot = OrdinalDirectionsHelper.DirectionToRotation(dirGlob, orientation, rotAmount);
                        uvs[0] = new Vector2(voxelData.Textures.GetX(dirLoc), voxelData.Textures.GetY(dirLoc)) * VoxelDataTextures.UV_SIZE_WITH_PADDING + VoxelDataTextures.TEXTURE_PADDING_OFFSET;
                        uvs[1] = uvs[0] + new Vector2(0, VoxelDataTextures.TEXTURE_DIMENSION);
                        uvs[2] = uvs[0] + new Vector2(VoxelDataTextures.TEXTURE_DIMENSION, VoxelDataTextures.TEXTURE_DIMENSION);
                        uvs[3] = uvs[0] + new Vector2(VoxelDataTextures.TEXTURE_DIMENSION, 0);

                        // Average UV Pos
                        uvC = new Vector2((uvs[0].x + uvs[1].x + uvs[2].x + uvs[3].x) / 4, (uvs[0].y + uvs[1].y + uvs[2].y + uvs[3].y) / 4);

                        // UV Rotation
                        uvs[0] = (uvs[0] - uvC).Rotate(rot) + uvC;
                        uvs[1] = (uvs[1] - uvC).Rotate(rot) + uvC;
                        uvs[2] = (uvs[2] - uvC).Rotate(rot) + uvC;
                        uvs[3] = (uvs[3] - uvC).Rotate(rot) + uvC;

                        v1 = new Vector3(comps[0], comps[1], comps[2]) * voxelSize + position;
                        v2 = new Vector3(comps[3], comps[4], comps[5]) * voxelSize + position;
                        v3 = new Vector3(comps[6], comps[7], comps[8]) * voxelSize + position;
                        v4 = new Vector3(comps[9], comps[10], comps[11]) * voxelSize + position;

                        // Mesh Data
                        addToMeshData.AddQuad(v1, v2, v3, v4, 0);
                        // Collider Data
                        addToMeshData.AddQuadToCollider(v1, v2, v3, v4);

                        // UVs
                        addToMeshData.AddUV(uvs[comps[12]]);
                        addToMeshData.AddUV(uvs[comps[13]]);
                        addToMeshData.AddUV(uvs[comps[14]]);
                        addToMeshData.AddUV(uvs[comps[15]]);
                    }
                    // Triangle Faces
                    else if (dirLoc == OrdinalDirections.East || dirLoc == OrdinalDirections.West)
                    {
                        comps = WEDGE_MESH_MAP[(int)orientation, rotAmount / 90, (int)dirGlob];

                        // UVs
                        rot = OrdinalDirectionsHelper.DirectionToRotation(dirGlob, orientation, rotAmount);
                        uvs[0] = new Vector2(voxelData.Textures.GetX(dirLoc), voxelData.Textures.GetY(dirLoc)) * VoxelDataTextures.UV_SIZE_WITH_PADDING + VoxelDataTextures.TEXTURE_PADDING_OFFSET;
                        uvs[1] = uvs[0] + new Vector2(0, VoxelDataTextures.TEXTURE_DIMENSION);
                        uvs[2] = uvs[0] + new Vector2(VoxelDataTextures.TEXTURE_DIMENSION, VoxelDataTextures.TEXTURE_DIMENSION);
                        uvs[3] = uvs[0] + new Vector2(VoxelDataTextures.TEXTURE_DIMENSION, 0);

                        // Average UV Pos
                        uvC = new Vector2((uvs[0].x + uvs[1].x + uvs[2].x + uvs[3].x) / 4, (uvs[0].y + uvs[1].y + uvs[2].y + uvs[3].y) / 4);

                        // UV Rotation
                        uvs[0] = (uvs[0] - uvC).Rotate(rot) + uvC;
                        uvs[1] = (uvs[1] - uvC).Rotate(rot) + uvC;
                        uvs[2] = (uvs[2] - uvC).Rotate(rot) + uvC;
                        uvs[3] = (uvs[3] - uvC).Rotate(rot) + uvC;

                        v1 = new Vector3(comps[0], comps[1], comps[2]) * voxelSize + position;
                        v2 = new Vector3(comps[3], comps[4], comps[5]) * voxelSize + position;
                        v3 = new Vector3(comps[6], comps[7], comps[8]) * voxelSize + position;

                        // Mesh Data
                        addToMeshData.AddTriangle(v1, v2, v3, 0);
                        // Collider Data
                        addToMeshData.AddTriangleToCollider(v1, v2, v3);

                        // UVs
                        addToMeshData.AddUV(uvs[comps[9]]);
                        addToMeshData.AddUV(uvs[comps[10]]);
                        addToMeshData.AddUV(uvs[comps[11]]);
                    }
                }
            }
        }

        /// <summary>
        /// Create a complex corner with orientation and rotation
        /// </summary>
        /// <param name="addToMeshData">The mesh data to add the block to</param>
        /// <param name="position">The position of the block</param>
        /// <param name="orientation">The voxel's orientation</param>
        /// <param name="rotAmount">The voxel's rotation around the orientation axis (0, 90, 180, 270)</param>
        /// <param name="visibility">Which sides of the block are visible</param>
        /// <param name="voxelData">The type of voxel to render</param>
        /// <param name="voxelSize">The size of the voxel</param>
        public static void CreateComplexCornerMeshData(MeshData addToMeshData, Vector3 position, OrdinalDirections orientation, int rotAmount, OrdinalBoolean visibility, VoxelData voxelData, float voxelSize)
        {
            // TODO: Implement this method
        }

        /// <summary>
        /// Create a complex inverse corner with orientation and rotation
        /// </summary>
        /// <param name="addToMeshData">The mesh data to add the block to</param>
        /// <param name="position">The position of the block</param>
        /// <param name="orientation">The voxel's orientation</param>
        /// <param name="rotAmount">The voxel's rotation around the orientation axis (0, 90, 180, 270)</param>
        /// <param name="visibility">Which sides of the block are visible</param>
        /// <param name="voxelData">The type of voxel to render</param>
        /// <param name="voxelSize">The size of the voxel</param>
        public static void CreateComplexInverseCornerMeshData(MeshData addToMeshData, Vector3 position, OrdinalDirections orientation, int rotAmount, OrdinalBoolean visibility, VoxelData voxelData, float voxelSize)
        {
            // TODO: Implement this method
        }
    }
}