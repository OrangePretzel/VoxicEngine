using System;
using UnityEngine;
using Voxic.Chunks;
using Voxic.Math;
using Voxic.Meshing;

namespace Voxic.Voxels
{
    public class VoxelData
    {
        public VoxelDataID ID;
        public string VoxelName;

        public VoxelDataSolidity Solidity;
        public VoxelDataTextures Textures;

        public VoxelData(VoxelDataID id, string name)
        {
            ID = id;
            VoxelName = name;
            Solidity = new VoxelDataSolidity(true);
            Textures = new VoxelDataTextures(0, 0);
        }

        public VoxelData SetSolidity(bool isSolid)
        {
            Solidity = new VoxelDataSolidity(isSolid);
            return this;
        }

        public VoxelData SetSolidity(VoxelDataSolidity solidity)
        {
            Solidity = solidity;
            return this;
        }

        public VoxelData SetTextures(VoxelDataTextures textures)
        {
            Textures = textures;
            return this;
        }

        public virtual void AddMeshData(MeshData addToMeshData, Chunk chunk, Vector3 renderPosition, IntVector3 localPosition, float voxelSize)
        {
            if (!Solidity.HasSolidFace)
                return;

            float x = renderPosition.x,
                y = renderPosition.y,
                z = renderPosition.z;
            Vector3 pos;
            Vector2 uvPos;
            float size = 16 / 288f;
            float padding = 1 / 288f;
            float uvSizeWithPadding = size + padding * 2;
            Vector2 paddingOffset = new Vector2(padding, padding);
            Vector2 uvSizeVec = new Vector2(size, size);

            // Up
            if (Solidity.UpIsSolid && !chunk.IsSolid(localPosition.X, localPosition.Y + 1, localPosition.Z, OrdinalDirections.Down))
            {
                pos = new Vector3(x, y + voxelSize, z);
                uvPos = new Vector2(Textures.UpX, Textures.UpY) * uvSizeWithPadding + paddingOffset;
                addToMeshData.AddQuad(pos, OrdinalDirections.Up, voxelSize, Textures.UpSubMesh);
                addToMeshData.AddQuadUVs(new Rect(uvPos, uvSizeVec), OrdinalDirections.Up);
                addToMeshData.AddQuadToCollider(pos, OrdinalDirections.Up, voxelSize);
            }

            // Down
            if (Solidity.DownIsSolid && !chunk.IsSolid(localPosition.X, localPosition.Y - 1, localPosition.Z, OrdinalDirections.Up))
            {
                pos = new Vector3(x, y - voxelSize, z);
                uvPos = new Vector2(Textures.DownX, Textures.DownY) * uvSizeWithPadding + paddingOffset;
                addToMeshData.AddQuad(pos, OrdinalDirections.Down, voxelSize, Textures.DownSubMesh);
                addToMeshData.AddQuadUVs(new Rect(uvPos, uvSizeVec), OrdinalDirections.Down);
                addToMeshData.AddQuadToCollider(pos, OrdinalDirections.Down, voxelSize);
            }

            // North
            if (Solidity.NorthIsSolid && !chunk.IsSolid(localPosition.X, localPosition.Y, localPosition.Z + 1, OrdinalDirections.South))
            {
                pos = new Vector3(x, y, z + voxelSize);
                uvPos = new Vector2(Textures.NorthX, Textures.NorthY) * uvSizeWithPadding + paddingOffset;
                addToMeshData.AddQuad(pos, OrdinalDirections.North, voxelSize, Textures.NorthSubMesh);
                addToMeshData.AddQuadUVs(new Rect(uvPos, uvSizeVec), OrdinalDirections.North);
                addToMeshData.AddQuadToCollider(pos, OrdinalDirections.North, voxelSize);
            }

            // South
            if (Solidity.SouthIsSolid && !chunk.IsSolid(localPosition.X, localPosition.Y, localPosition.Z - 1, OrdinalDirections.North))
            {
                pos = new Vector3(x, y, z - voxelSize);
                uvPos = new Vector2(Textures.SouthX, Textures.SouthY) * uvSizeWithPadding + paddingOffset;
                addToMeshData.AddQuad(pos, OrdinalDirections.South, voxelSize, Textures.SouthSubMesh);
                addToMeshData.AddQuadUVs(new Rect(uvPos, uvSizeVec), OrdinalDirections.South);
                addToMeshData.AddQuadToCollider(pos, OrdinalDirections.South, voxelSize);
            }

            // East
            if (Solidity.EastIsSolid && !chunk.IsSolid(localPosition.X + 1, localPosition.Y, localPosition.Z, OrdinalDirections.West))
            {
                pos = new Vector3(x + voxelSize, y, z);
                uvPos = new Vector2(Textures.EastX, Textures.EastY) * uvSizeWithPadding + paddingOffset;
                addToMeshData.AddQuad(pos, OrdinalDirections.East, voxelSize, Textures.EastSubMesh);
                addToMeshData.AddQuadUVs(new Rect(uvPos, uvSizeVec), OrdinalDirections.East);
                addToMeshData.AddQuadToCollider(pos, OrdinalDirections.East, voxelSize);
            }

            // West
            if (Solidity.WestIsSolid && !chunk.IsSolid(localPosition.X - 1, localPosition.Y, localPosition.Z, OrdinalDirections.East))
            {
                pos = new Vector3(x - voxelSize, y, z);
                uvPos = new Vector2(Textures.WestX, Textures.WestY) * uvSizeWithPadding + paddingOffset;
                addToMeshData.AddQuad(pos, OrdinalDirections.West, voxelSize, Textures.WestSubMesh);
                addToMeshData.AddQuadUVs(new Rect(uvPos, uvSizeVec), OrdinalDirections.West);
                addToMeshData.AddQuadToCollider(pos, OrdinalDirections.West, voxelSize);
            }
        }

        public virtual bool IsSolid(OrdinalDirections dir)
        {
            switch (dir)
            {
                case OrdinalDirections.Up:
                    return Solidity.UpIsSolid;
                case OrdinalDirections.Down:
                    return Solidity.DownIsSolid;
                case OrdinalDirections.North:
                    return Solidity.NorthIsSolid;
                case OrdinalDirections.East:
                    return Solidity.EastIsSolid;
                case OrdinalDirections.South:
                    return Solidity.SouthIsSolid;
                case OrdinalDirections.West:
                    return Solidity.WestIsSolid;
                default:
                    return false;
            }
        }
    }
}
