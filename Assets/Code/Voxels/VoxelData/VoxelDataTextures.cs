using UnityEngine;
using Voxic.Math;

namespace Voxic.Voxels
{
    public struct VoxelDataTextures
    {
        // TODO: Improve this class

        public const float TEXTURE_DIMENSION = 16 / 288f;
        public const float TEXTURE_PADDING = 1 / 288f;
        public const float UV_SIZE_WITH_PADDING = TEXTURE_DIMENSION + TEXTURE_PADDING * 2;
        public static Vector2 TEXTURE_PADDING_OFFSET = new Vector2(TEXTURE_PADDING, TEXTURE_PADDING);
        public static Vector2 TEXTURE_SIZE = new Vector2(TEXTURE_DIMENSION, TEXTURE_DIMENSION);

        public byte UpX;
        public byte UpY;
        public byte UpSubMesh;

        public byte DownX;
        public byte DownY;
        public byte DownSubMesh;

        public byte NorthX;
        public byte NorthY;
        public byte NorthSubMesh;

        public byte SouthX;
        public byte SouthY;
        public byte SouthSubMesh;

        public byte EastX;
        public byte EastY;
        public byte EastSubMesh;

        public byte WestX;
        public byte WestY;
        public byte WestSubMesh;

        public VoxelDataTextures(byte x, byte y, byte subMesh = 0)
        {
            UpX = x;
            UpY = y;
            UpSubMesh = subMesh;

            DownX = x;
            DownY = y;
            DownSubMesh = subMesh;

            NorthX = x;
            NorthY = y;
            NorthSubMesh = subMesh;

            SouthX = x;
            SouthY = y;
            SouthSubMesh = subMesh;

            EastX = x;
            EastY = y;
            EastSubMesh = subMesh;

            WestX = x;
            WestY = y;
            WestSubMesh = subMesh;
        }

        public byte GetX(OrdinalDirections dir)
        {
            switch (dir)
            {
                case OrdinalDirections.Up:
                    return UpX;
                case OrdinalDirections.Down:
                    return DownX;
                case OrdinalDirections.North:
                    return NorthX;
                case OrdinalDirections.East:
                    return EastX;
                case OrdinalDirections.South:
                    return SouthX;
                case OrdinalDirections.West:
                    return WestX;
                default:
                    return 0;
            }
        }

        public byte GetY(OrdinalDirections dir)
        {
            switch (dir)
            {
                case OrdinalDirections.Up:
                    return UpY;
                case OrdinalDirections.Down:
                    return DownY;
                case OrdinalDirections.North:
                    return NorthY;
                case OrdinalDirections.East:
                    return EastY;
                case OrdinalDirections.South:
                    return SouthY;
                case OrdinalDirections.West:
                    return WestY;
                default:
                    return 0;
            }
        }

        public byte GetSubMesh(OrdinalDirections dir)
        {
            switch (dir)
            {
                case OrdinalDirections.Up:
                    return UpSubMesh;
                case OrdinalDirections.Down:
                    return DownSubMesh;
                case OrdinalDirections.North:
                    return NorthSubMesh;
                case OrdinalDirections.East:
                    return EastSubMesh;
                case OrdinalDirections.South:
                    return SouthSubMesh;
                case OrdinalDirections.West:
                    return WestSubMesh;
                default:
                    return 0;
            }
        }
    }
}