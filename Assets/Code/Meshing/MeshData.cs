using System.Collections.Generic;
using UnityEngine;
using Voxic.Math;

namespace Voxic.Meshing
{
    // TODO: Consider changing the following from a class to a struct and updating parameters to be ref types

    /// <summary>
    /// Class representing mesh data
    /// </summary>
    public class MeshData
    {
        /// <summary>
        /// The mesh's vertices
        /// </summary>
        private List<Vector3> vertices;
        /// <summary>
        /// The mesh's triangles, grouped by sub-mesh ID
        /// </summary>
        private List<int>[] triangles;
        /// <summary>
        /// The mesh's uvs
        /// </summary>
        private List<Vector2> uvs;
        /// <summary>
        /// The mesh's collider vertices
        /// </summary>
        private List<Vector3> colliderVertices;
        /// <summary>
        /// The mesh's collider triangles
        /// </summary>
        private List<int> colliderTriangles;

        #region Constructor

        /// <summary>
        /// Create a new empty mesh data with the given number of submeshes
        /// </summary>
        /// <param name="subMeshes">The number of submeshes in the mesh (defaults to 1)</param>
        public MeshData(uint subMeshes = 1)
        {
            vertices = new List<Vector3>();
            triangles = new List<int>[subMeshes];
            for (int i = 0; i < subMeshes; i++)
                triangles[i] = new List<int>();
            uvs = new List<Vector2>();
            colliderVertices = new List<Vector3>();
            colliderTriangles = new List<int>();
        }

        /// <summary>
        /// Creates a copy of the given mesh data
        /// </summary>
        /// <param name="meshData">The mesh data to copy</param>
        public MeshData(MeshData meshData)
        {
            vertices = meshData.vertices;
            triangles = meshData.triangles;
            uvs = meshData.uvs;
            colliderVertices = meshData.colliderVertices;
            colliderTriangles = meshData.colliderTriangles;
        }

        #endregion

        #region Add Single Point(s)

        /// <summary>
        /// Add a vertex with the given components to the mesh data
        /// </summary>
        /// <param name="x">The x component</param>
        /// <param name="y">The y component</param>
        /// <param name="z">The z component</param>
        public void AddVertex(float x, float y, float z)
        {
            vertices.Add(new Vector3(x, y, z));
        }

        /// <summary>
        /// Add a vertex to the mesh data
        /// </summary>
        /// <param name="vertex">The vertex to add</param>
        public void AddVertex(Vector3 vertex)
        {
            vertices.Add(vertex);
        }

        /// <summary>
        /// Add a vertex with the given components to the collider data
        /// </summary>
        /// <param name="x">The x component</param>
        /// <param name="y">The y component</param>
        /// <param name="z">The z component</param>
        public void AddVertexToCollider(float x, float y, float z)
        {
            colliderVertices.Add(new Vector3(x, y, z));
        }

        /// <summary>
        /// Add a vertex to the collider data
        /// </summary>
        /// <param name="vertex">The vertex to add</param>
        public void AddVertexToCollider(Vector3 vertex)
        {
            colliderVertices.Add(vertex);
        }

        /// <summary>
        /// Add a uv with the given components to the mesh data
        /// </summary>
        /// <param name="x">The x component</param>
        /// <param name="y">The y component</param>
        public void AddUV(float x, float y)
        {
            uvs.Add(new Vector2(x, y));
        }

        /// <summary>
        /// Add a uv to the mesh data
        /// </summary>
        /// <param name="uv">The uv to add</param>
        public void AddUV(Vector2 uv)
        {
            uvs.Add(uv);
        }

        #endregion

        #region Add Triangle(s)

        /// <summary>
        /// Add a triangle to mesh data using vertex indices
        /// </summary>
        /// <param name="i1">Vertex index 1</param>
        /// <param name="i2">Vertex index 2</param>
        /// <param name="i3">Vertex index 3</param>
        /// <param name="subMesh">The submesh to add the triangle to</param>
        public void AddTriangle(int i1, int i2, int i3, int subMesh)
        {
            triangles[subMesh].Add(i1);
            triangles[subMesh].Add(i2);
            triangles[subMesh].Add(i3);
        }

        /// <summary>
        /// Add a triangle to the mesh data using vertex positions
        /// </summary>
        /// <param name="v1">Vertex 1</param>
        /// <param name="v2">Vertex 2</param>
        /// <param name="v3">Vertex 3</param>
        /// <param name="subMesh">The submesh to add the triangle to</param>
        public void AddTriangle(Vector3 v1, Vector3 v2, Vector3 v3, int subMesh)
        {
            vertices.Add(v1);
            vertices.Add(v2);
            vertices.Add(v3);

            var tris = triangles[subMesh];
            tris.Add(vertices.Count - 3);
            tris.Add(vertices.Count - 2);
            tris.Add(vertices.Count - 1);
        }

        /// <summary>
        /// Add a triangle to collider data using vertex indices
        /// </summary>
        /// <param name="i1">Vertex index 1</param>
        /// <param name="i2">Vertex index 2</param>
        /// <param name="i3">Vertex index 3</param>
        public void AddTriangleToCollider(int i1, int i2, int i3)
        {
            colliderTriangles.Add(i1);
            colliderTriangles.Add(i2);
            colliderTriangles.Add(i3);
        }

        /// <summary>
        /// Add a triangle to the collider data using vertex positions
        /// </summary>
        /// <param name="v1">Vertex 1</param>
        /// <param name="v2">Vertex 2</param>
        /// <param name="v3">Vertex 3</param>
        public void AddTriangleToCollider(Vector3 v1, Vector3 v2, Vector3 v3)
        {
            colliderVertices.Add(v1);
            colliderVertices.Add(v2);
            colliderVertices.Add(v3);

            colliderTriangles.Add(colliderVertices.Count - 3);
            colliderTriangles.Add(colliderVertices.Count - 2);
            colliderTriangles.Add(colliderVertices.Count - 1);
        }

        #endregion

        #region Add Quad

        /// <summary>
        /// Add a quad to the mesh data using vertex positions
        /// </summary>
        /// <param name="v1">Vertex 1</param>
        /// <param name="v2">Vertex 2</param>
        /// <param name="v3">Vertex 3</param>
        /// <param name="v4">Vertex 4</param>
        /// <param name="subMesh">The submesh to add the quad to</param>
        public void AddQuad(Vector3 v1, Vector3 v2, Vector3 v3, Vector3 v4, int submesh)
        {
            vertices.Add(v1);
            vertices.Add(v2);
            vertices.Add(v3);
            vertices.Add(v4);

            AddQuadTriangles(submesh);
        }

        /// <summary>
        /// Add a quad to the mesh data using a position, size and normal direction
        /// </summary>
        /// <param name="position">The position to add the quad (offset for face is added automatically)</param>
        /// <param name="dir">The direction of the face</param>
        /// <param name="size">The size of the face (half the width)</param>
        /// <param name="subMesh">The submesh to add the quad to</param>
        public void AddQuad(Vector3 position, OrdinalDirections dir, float size, int submesh)
        {
            var ps = +size;
            var ns = -size;
            switch (dir)
            {
                case OrdinalDirections.Up:
                    vertices.Add(position + new Vector3(ns, ps, ps));
                    vertices.Add(position + new Vector3(ps, ps, ps));
                    vertices.Add(position + new Vector3(ps, ps, ns));
                    vertices.Add(position + new Vector3(ns, ps, ns));
                    break;
                case OrdinalDirections.Down:
                    vertices.Add(position + new Vector3(ns, ns, ns));
                    vertices.Add(position + new Vector3(ps, ns, ns));
                    vertices.Add(position + new Vector3(ps, ns, ps));
                    vertices.Add(position + new Vector3(ns, ns, ps));
                    break;
                case OrdinalDirections.North:
                    vertices.Add(position + new Vector3(ps, ns, ps));
                    vertices.Add(position + new Vector3(ps, ps, ps));
                    vertices.Add(position + new Vector3(ns, ps, ps));
                    vertices.Add(position + new Vector3(ns, ns, ps));
                    break;
                case OrdinalDirections.East:
                    vertices.Add(position + new Vector3(ps, ns, ns));
                    vertices.Add(position + new Vector3(ps, ps, ns));
                    vertices.Add(position + new Vector3(ps, ps, ps));
                    vertices.Add(position + new Vector3(ps, ns, ps));
                    break;
                case OrdinalDirections.South:
                    vertices.Add(position + new Vector3(ns, ns, ns));
                    vertices.Add(position + new Vector3(ns, ps, ns));
                    vertices.Add(position + new Vector3(ps, ps, ns));
                    vertices.Add(position + new Vector3(ps, ns, ns));
                    break;
                case OrdinalDirections.West:
                    vertices.Add(position + new Vector3(ns, ns, ps));
                    vertices.Add(position + new Vector3(ns, ps, ps));
                    vertices.Add(position + new Vector3(ns, ps, ns));
                    vertices.Add(position + new Vector3(ns, ns, ns));
                    break;
                default:
                    break;
            }

            AddQuadTriangles(submesh);
        }

        /// <summary>
        /// Add a quad to the mesh collider using vertex positions
        /// </summary>
        /// <param name="v1">Vertex 1</param>
        /// <param name="v2">Vertex 2</param>
        /// <param name="v3">Vertex 3</param>
        /// <param name="v4">Vertex 4</param>
        public void AddQuadToCollider(Vector3 v1, Vector3 v2, Vector3 v3, Vector3 v4)
        {
            colliderVertices.Add(v1);
            colliderVertices.Add(v2);
            colliderVertices.Add(v3);
            colliderVertices.Add(v4);

            AddQuadTrianglesToCollider();
        }

        /// <summary>
        /// Add a quad to the collider data using a position, size and normal direction
        /// </summary>
        /// <param name="position">The position to add the quad (offset for face is added automatically)</param>
        /// <param name="dir">The direction of the face</param>
        /// <param name="size">The size of the face (half the width)</param>
        public void AddQuadToCollider(Vector3 position, OrdinalDirections dir, float size)
        {
            switch (dir)
            {
                case OrdinalDirections.North:
                    colliderVertices.Add(position + new Vector3(+size, -size, +size));
                    colliderVertices.Add(position + new Vector3(+size, +size, +size));
                    colliderVertices.Add(position + new Vector3(-size, +size, +size));
                    colliderVertices.Add(position + new Vector3(-size, -size, +size));
                    break;
                case OrdinalDirections.South:
                    colliderVertices.Add(position + new Vector3(-size, -size, -size));
                    colliderVertices.Add(position + new Vector3(-size, +size, -size));
                    colliderVertices.Add(position + new Vector3(+size, +size, -size));
                    colliderVertices.Add(position + new Vector3(+size, -size, -size));
                    break;
                case OrdinalDirections.East:
                    colliderVertices.Add(position + new Vector3(+size, -size, -size));
                    colliderVertices.Add(position + new Vector3(+size, +size, -size));
                    colliderVertices.Add(position + new Vector3(+size, +size, +size));
                    colliderVertices.Add(position + new Vector3(+size, -size, +size));
                    break;
                case OrdinalDirections.West:
                    colliderVertices.Add(position + new Vector3(-size, -size, +size));
                    colliderVertices.Add(position + new Vector3(-size, +size, +size));
                    colliderVertices.Add(position + new Vector3(-size, +size, -size));
                    colliderVertices.Add(position + new Vector3(-size, -size, -size));
                    break;
                case OrdinalDirections.Up:
                    colliderVertices.Add(position + new Vector3(-size, +size, +size));
                    colliderVertices.Add(position + new Vector3(+size, +size, +size));
                    colliderVertices.Add(position + new Vector3(+size, +size, -size));
                    colliderVertices.Add(position + new Vector3(-size, +size, -size));
                    break;
                case OrdinalDirections.Down:
                    colliderVertices.Add(position + new Vector3(-size, -size, -size));
                    colliderVertices.Add(position + new Vector3(+size, -size, -size));
                    colliderVertices.Add(position + new Vector3(+size, -size, +size));
                    colliderVertices.Add(position + new Vector3(-size, -size, +size));
                    break;
                default:
                    break;
            }

            AddQuadTrianglesToCollider();
        }

        /// <summary>
        /// Add triangle information for the last four vertices to form a quad in the mesh data
        /// </summary>
        /// <param name="subMesh">The submesh to add the quad to</param>
        public void AddQuadTriangles(int subMesh)
        {
            AddTriangle(vertices.Count - 4, vertices.Count - 3, vertices.Count - 2, subMesh);
            AddTriangle(vertices.Count - 4, vertices.Count - 2, vertices.Count - 1, subMesh);
        }

        /// <summary>
        /// Add triangle information for the last four vertices to form a quad in the collider data
        /// </summary>
        public void AddQuadTrianglesToCollider()
        {
            AddTriangleToCollider(colliderVertices.Count - 4, colliderVertices.Count - 3, colliderVertices.Count - 2);
            AddTriangleToCollider(colliderVertices.Count - 4, colliderVertices.Count - 2, colliderVertices.Count - 1);
        }

        /// <summary>
        /// Add uv data for the last four vertices in the mesh data from a rectangle
        /// </summary>
        /// <param name="uv">The uv rect information</param>
        /// <param name="dir">The direction to add the uvs</param>
        public void AddQuadUVs(Rect uv, OrdinalDirections dir)
        {
            // TODO: Consider simplifying this method
            switch (dir)
            {
                case OrdinalDirections.Up:
                    uvs.Add(uv.position + new Vector2(0, uv.height));
                    uvs.Add(uv.position + uv.size);
                    uvs.Add(uv.position + new Vector2(uv.width, 0));
                    uvs.Add(uv.position);
                    break;
                case OrdinalDirections.Down:
                    uvs.Add(uv.position + new Vector2(uv.width, 0));
                    uvs.Add(uv.position);
                    uvs.Add(uv.position + new Vector2(0, uv.height));
                    uvs.Add(uv.position + uv.size);
                    break;
                case OrdinalDirections.North:
                case OrdinalDirections.East:
                case OrdinalDirections.South:
                case OrdinalDirections.West:
                    uvs.Add(uv.position);
                    uvs.Add(uv.position + new Vector2(0, uv.height));
                    uvs.Add(uv.position + uv.size);
                    uvs.Add(uv.position + new Vector2(uv.width, 0));
                    break;
                default:
                    break;
            }
        }

        #endregion

        #region To Mesh

        /// <summary>
        /// Create a Mesh from the mesh data
        /// </summary>
        /// <returns></returns>
        public Mesh CreateMesh()
        {
            Mesh mesh = new Mesh();
            mesh.vertices = vertices.ToArray();
            for (int i = 0; i < triangles.Length; i++)
            {
                mesh.SetTriangles(triangles[i].ToArray(), i);
            }
            mesh.uv = uvs.ToArray();

            mesh.RecalculateBounds();
            mesh.RecalculateNormals();

            return mesh;
        }

        /// <summary>
        /// Create a Mesh from the collider data
        /// </summary>
        /// <returns></returns>
        public Mesh CreateColliderMesh()
        {
            Mesh mesh = new Mesh();
            mesh.vertices = colliderVertices.ToArray();
            mesh.triangles = colliderTriangles.ToArray();

            mesh.RecalculateBounds();
            mesh.RecalculateNormals();

            return mesh;
        }

        #endregion

        /// <summary>
        /// Clear the mesh data
        /// </summary>
        public void Clear()
        {
            vertices.Clear();
            for (int i = 0; i < triangles.Length; i++)
                triangles[i].Clear();
            uvs.Clear();
            colliderVertices.Clear();
            colliderTriangles.Clear();
        }
    }
}