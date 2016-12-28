using System.Collections;
using System.Threading;
using UnityEngine;
using Voxic.Chunks;
using Voxic.Math;
using Voxic.Meshing;

namespace Voxic.Rendering
{
    /// <summary>
    /// Component for rendering a chunk
    /// </summary>
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshCollider))]
    public class ChunkRenderer : MonoBehaviour
    {
        /// <summary>
        /// The mode of rendering the chunk
        /// </summary>
        public int RenderMode = 0;
        /// <summary>
        /// The method for rendering the mesh
        /// </summary>
        public MeshingModes MeshMode = MeshingModes.MeshAndCollider;
        /// <summary>
        /// The chunk to render
        /// </summary>
        public Chunk Chunk { get; private set; }

        /// <summary>
        /// The MeshFilter
        /// </summary>
        private MeshFilter meshFilter;
        /// <summary>
        /// The MeshCollider
        /// </summary>
        private MeshCollider meshCollider;
        /// <summary>
        /// The MeshRenderer
        /// </summary>
        private MeshRenderer meshRenderer;

        /// <summary>
        /// The chunks meshdata
        /// </summary>
        private MeshData meshData = new MeshData();

        /// <summary>
        /// OnEnable
        /// </summary>
        private void OnEnable()
        {
            meshFilter = GetComponent<MeshFilter>();
            meshRenderer = GetComponent<MeshRenderer>();
            meshCollider = GetComponent<MeshCollider>();
        }

        /// <summary>
        /// Set the chunk
        /// </summary>
        /// <param name="chunk">The chunk</param>
        public void SetChunk(Chunk chunk)
        {
            Chunk = chunk;
        }

        /// <summary>
        /// Asynchronously render the chunk in the world
        /// </summary>
        public void RenderChunkAsync()
        {
            if (Chunk == null)
                return;

            StartCoroutine(RenderChunkCoroutine());
        }

        /// <summary>
        /// Render chunk coroutine
        /// </summary>
        /// <returns></returns>
        private IEnumerator RenderChunkCoroutine()
        {
            // Prepare the chunk
            Thread thread = new Thread(PrepareMeshDataAsync);
            thread.Start();

            // Wait
            while (thread.IsAlive)
            {
                yield return new WaitForEndOfFrame();
            }

            // Render the thread
            RenderChunkMesh();
        }

        /// <summary>
        /// Prepare the mesh data for rendering
        /// </summary>
        private void PrepareMeshDataAsync()
        {
            meshData.Clear();
            IntVector3 position;
            try
            {
                for (int i = 0; i < Chunk.World.WorldSettings.ChunkSizeInVoxels; i++)
                    for (int j = 0; j < Chunk.World.WorldSettings.ChunkSizeInVoxels; j++)
                        for (int k = 0; k < Chunk.World.WorldSettings.ChunkSizeInVoxels; k++)
                        {
                            position = new IntVector3(i, j, k);
                            Chunk.Voxels[i, j, k].AddMeshData(meshData, Chunk, position, (Vector3)position, Chunk.World.WorldSettings.HalfVoxelSizeInUnits);
                        }
            }
            catch (System.Exception ex)
            {
                Debug.LogException(ex);
            }
        }

        /// <summary>
        /// Render the chunk mesh data
        /// </summary>
        private void RenderChunkMesh()
        {
            if (MeshMode != MeshingModes.ColliderOnly)
                meshFilter.mesh = meshData.CreateMesh();

            if (MeshMode != MeshingModes.MeshOnly)
                meshCollider.sharedMesh = meshData.CreateColliderMesh();
        }
    }
}