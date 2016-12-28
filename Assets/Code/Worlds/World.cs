using System.Collections.Generic;
using Voxic.Chunks;
using Voxic.Math;
using Voxic.Voxels;

namespace Voxic.Worlds
{
    // TODO: Comment this code
    /// <summary>
    /// Class representing a voxel world
    /// </summary>
    public class World
    {
        /// <summary>
        /// The settings for creating/rendering a voxel world
        /// </summary>
        public WorldSettings WorldSettings { get; private set; }
        /// <summary>
        /// The position helper for translating coordinates
        /// </summary>
        public PositionHelper PosHelper { get; private set; }
        /// <summary>
        /// The voxel data manager for the world
        /// </summary>
        public VoxelDataManager VoxelDataManager { get; private set; }

        /// <summary>
        /// The list of chunks loaded into the world
        /// </summary>
        private Dictionary<IntVector3, Chunk> loadedChunks;

        /// <summary>
        /// Create a new voxel world
        /// </summary>
        /// <param name="settings">The settings for the world</param>
        public World(WorldSettings settings)
        {
            WorldSettings = settings;
            PosHelper = new PositionHelper(this);
            loadedChunks = new Dictionary<IntVector3, Chunk>();
        }

        /// <summary>
        /// Load a chunk into the world at the given position
        /// </summary>
        /// <param name="chunkPos">The position of the chunk to load</param>
        /// <returns></returns>
        public Chunk LoadChunk(IntVector3 chunkPos)
        {
            if (IsChunkLoaded(chunkPos))
                return loadedChunks[chunkPos];

            // TODO: Add chunk deserialization functionality
            Chunk chunk = new Chunk(this, chunkPos);
            loadedChunks.Add(chunkPos, chunk);

            return chunk;
        }

        /// <summary>
        /// Load the chunk at the given location
        /// </summary>
        /// <param name="cX">The x component of the position</param>
        /// <param name="cY">The y component of the position</param>
        /// <param name="cZ">The z component of the position</param>
        /// <returns></returns>
        public Chunk LoadChunk(int cX, int cY, int cZ)
        {
            IntVector3 chunkPos = new IntVector3(cX, cY, cZ);
            if (IsChunkLoaded(chunkPos))
                return loadedChunks[chunkPos];

            Chunk chunk = new Chunk(this, chunkPos);
            loadedChunks.Add(chunkPos, chunk);

            return chunk;
        }

        /// <summary>
        /// Check if the chunk is loaded at the given position
        /// </summary>
        /// <param name="cX">The x component of the position</param>
        /// <param name="cY">The y component of the position</param>
        /// <param name="cZ">The z component of the position</param>
        /// <returns></returns>
        public bool IsChunkLoaded(int cX, int cY, int cZ)
        {
            return loadedChunks.ContainsKey(new IntVector3(cX, cY, cZ));
        }

        /// <summary>
        /// Check if the chunk is loaded at the given position
        /// </summary>
        /// <param name="chunkPos">The chunk position to check</param>
        /// <returns></returns>
        public bool IsChunkLoaded(IntVector3 chunkPos)
        {
            return loadedChunks.ContainsKey(chunkPos);
        }

        /// <summary>
        /// Returns true if the position given is in the world
        /// </summary>
        /// <param name="worldPosition">The position to check</param>
        /// <returns></returns>
        public bool IsInWorldBoundary(IntVector3 worldPosition)
        {
            // TODO: Implement this method to factor in world parameters
            return true;
        }

        /// <summary>
        /// Returns true if the given face of the voxel at the given position is solid
        /// </summary>
        /// <param name="worldPosition">The position to check</param>
        /// <param name="dir">The face to check</param>
        /// <returns></returns>
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