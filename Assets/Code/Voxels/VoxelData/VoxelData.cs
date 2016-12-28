using Voxic.Math;

namespace Voxic.Voxels
{
    /// <summary>
    /// Class for voxel data/information
    /// </summary>
    public class VoxelData
    {
        /// <summary>
        /// The unique identifier for the voxel data type
        /// </summary>
        public VoxelDataID ID { get; private set; }
        /// <summary>
        /// The name of the voxel data
        /// </summary>
        public string VoxelName { get; private set; }

        /// <summary>
        /// True for direction if that direction has a face
        /// </summary>
        public OrdinalBoolean Faces { get; private set; }
        /// <summary>
        /// True for direction if that direction's face is opaque (the adjacent face should be rendered if false)
        /// </summary>
        public OrdinalBoolean Solidity { get; private set; }
        /// <summary>
        /// The texutre information for the voxel
        /// </summary>
        public VoxelDataTextures Textures { get; private set; }

        #region Constructor

        /// <summary>
        /// Create a new VoxelData
        /// </summary>
        /// <param name="id">The ID of the voxel data</param>
        /// <param name="name">The name of the voxel data</param>
        public VoxelData(VoxelDataID id, string name)
        {
            ID = id;
            VoxelName = name;
            Faces = new OrdinalBoolean(true);
            Solidity = new OrdinalBoolean(true);
            Textures = new VoxelDataTextures(0, 0);
        }

        #endregion

        #region Setters

        /// <summary>
        /// Set the face data for the voxel data
        /// </summary>
        /// <param name="hasFaces">The new face value for all faces</param>
        /// <returns></returns>
        public VoxelData SetFaces(bool hasFaces)
        {
            Faces = new OrdinalBoolean(hasFaces);
            return this;
        }

        /// <summary>
        /// Set the face data for the voxel data
        /// </summary>
        /// <param name="faces">The new face data</param>
        /// <returns></returns>
        public VoxelData SetFaces(OrdinalBoolean faces)
        {
            Faces = faces;
            return this;
        }

        /// <summary>
        /// Set the solidity data for the voxel data
        /// </summary>
        /// <param name="solidity">The new solidity value for all faces</param>
        /// <returns></returns>
        public VoxelData SetSolidity(bool solidity)
        {
            Solidity = new OrdinalBoolean(solidity);
            return this;
        }

        /// <summary>
        /// Set the solidity data for the voxel data
        /// </summary>
        /// <param name="solidity">The new solidity data</param>
        /// <returns></returns>
        public VoxelData SetSolidity(OrdinalBoolean solidity)
        {
            Solidity = solidity;
            return this;
        }

        /// <summary>
        /// Set the texture data for the voxel data
        /// </summary>
        /// <param name="textures">The new texture data</param>
        /// <returns></returns>
        public VoxelData SetTextures(VoxelDataTextures textures)
        {
            Textures = textures;
            return this;
        }

        #endregion

        #region Getters

        /// <summary>
        /// Returns true if the given direction has a face
        /// </summary>
        /// <param name="dir">The direction to check</param>
        /// <returns></returns>
        public bool HasFace(OrdinalDirections dir)
        {
            return Faces.GetBoolForDir(dir);
        }

        /// <summary>
        /// Returns true if the given direction's face is solid
        /// </summary>
        /// <param name="dir">The direction to check</param>
        /// <returns></returns>
        public bool IsSolid(OrdinalDirections dir)
        {
            return Solidity.GetBoolForDir(dir);
        }

        #endregion
    }
}