using System;
using UnityEngine;
using Voxic.Chunks;
using Voxic.Math;
using Voxic.Meshing;

namespace Voxic.Voxels
{
    /// <summary>
    /// Class representing a single voxel in the voxel object with a orientation, rotation and style
    /// </summary>
    public class ComplexVoxel : Voxel
    {
        /// <summary>
        /// The style of voxel
        /// </summary>
        public VoxelStyle Style { get; private set; }
        /// <summary>
        /// The voxel's orientation
        /// </summary>
        public OrdinalDirections Orientation { get; private set; }
        // TODO: Consider making the following into an enum instead of a value restricted int
        /// <summary>
        /// The rotation of the voxel around the orientation axis (0, 90, 180, 270)
        /// </summary>
        public int Rotation { get; private set; }

        /// <summary>
        /// Create a new voxel with the given inner data
        /// </summary>
        /// <param name="voxelData">The inner voxel data</param>
        /// <param name="style">The style of voxel (defaults to block)</param>
        public ComplexVoxel(VoxelData voxelData, VoxelStyle style = VoxelStyle.Block)
            : base(voxelData)
        {
            Style = style;
            Orientation = OrdinalDirections.Up;
            Rotation = 0;
        }

        /// <summary>
        /// Set the orientation and rotation of the voxel
        /// </summary>
        /// <param name="orientation">The orientation of the voxel</param>
        /// <param name="rotAmount">The rotation around the orientation axis (0, 90, 180, 270)</param>
        public void OrientRotateVoxel(OrdinalDirections orientation, int rotAmount)
        {
            if (rotAmount % 90 != 0)
                throw new System.Exception("Voxels don't bend that way honey!");
            if (rotAmount > 270)
                rotAmount = ((rotAmount / 90) % 4) * 90;

            Orientation = orientation;
            Rotation = rotAmount;
        }

        /// <summary>
        /// Set the voxels style
        /// </summary>
        /// <param name="style">The new style</param>
        public void SetStyle(VoxelStyle style)
        {
            Style = style;
        }

        /// <summary>
        /// Add mesh data to the given mesh for a voxel in the given chunk at the given localPosition. Renders at the given position with the given size
        /// </summary>
        /// <param name="addToMeshData">The mesh to add the voxel to</param>
        /// <param name="chunk">The chunk the voxel is in</param>
        /// <param name="localPosition">The position in the chunk of the voxel</param>
        /// <param name="position">The position in the scene to render at</param>
        /// <param name="voxelSize">The size of the voxel</param>
        public override void AddMeshData(MeshData addToMeshData, Chunk chunk, IntVector3 localPosition, Vector3 position, float voxelSize)
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

            // Create the actual data for the appropriate style
            switch (Style)
            {
                case VoxelStyle.Block:
                    BlockMesher.CreateComplexBlockMeshData(addToMeshData, position, Orientation, Rotation, visibility, VoxelData, voxelSize);
                    break;
                case VoxelStyle.Wedge:
                    BlockMesher.CreateComplexWedgeMeshData(addToMeshData, position, Orientation, Rotation, visibility, VoxelData, voxelSize);
                    break;
                case VoxelStyle.Corner:
                    BlockMesher.CreateComplexCornerMeshData(addToMeshData, position, Orientation, Rotation, visibility, VoxelData, voxelSize);
                    break;
                case VoxelStyle.InverseCorner:
                    BlockMesher.CreateComplexInverseCornerMeshData(addToMeshData, position, Orientation, Rotation, visibility, VoxelData, voxelSize);
                    break;
                default:
                    BlockMesher.CreateSimpleBlockMeshData(addToMeshData, position, visibility, VoxelData, voxelSize);
                    break;
            }
        }

        /// <summary>
        /// Returns true if the face in the given direction is solid
        /// </summary>
        /// <param name="dir">The direction of the face</param>
        /// <returns></returns>
        public override bool IsSolid(OrdinalDirections dir)
        {
            OrdinalDirections dirLoc = OrdinalDirectionsHelper.GlobalToLocalDirection(dir, Orientation, Rotation);

            switch (Style)
            {
                case VoxelStyle.Wedge:
                    if (dirLoc != OrdinalDirections.South && dirLoc != OrdinalDirections.Down)
                        return false;
                    return base.IsSolid(dirLoc);
                case VoxelStyle.Corner:
                // TODO: Implement Corner solidity
                case VoxelStyle.InverseCorner:
                // TODO: Implement Anti Corner solidity
                case VoxelStyle.Block:
                default:
                    return base.IsSolid(dirLoc);
            }
        }
    }
}
