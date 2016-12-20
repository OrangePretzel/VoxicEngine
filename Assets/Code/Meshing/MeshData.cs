using System;
using System.Collections.Generic;
using UnityEngine;

namespace Voxic.Meshing
{
    public class MeshData
    {
        private List<Vector3> _vertices;
        private List<int>[] _triangles;
        private List<Vector2> _uvs;
        private List<Vector3> _colliderVertices;
        private List<int> _colliderTriangles;

        public List<Vector3> Vertices
        {
            get { return _vertices; }
        }
        public List<int>[] Triangles
        {
            get { return _triangles; }
        }
        public List<Vector2> UVs
        {
            get { return _uvs; }
        }
        public List<Vector3> ColliderVertices
        {
            get { return _colliderVertices; }
        }
        public List<int> ColliderTriangles
        {
            get { return _colliderTriangles; }
        }

        #region Constructor

        public MeshData(int subMeshes = 1)
        {
            _vertices = new List<Vector3>();
            _triangles = new List<int>[subMeshes];
            for (int i = 0; i < subMeshes; i++)
                _triangles[i] = new List<int>();
            _uvs = new List<Vector2>();
            _colliderVertices = new List<Vector3>();
            _colliderTriangles = new List<int>();
        }

        public MeshData(MeshData meshData, int subMeshes = 1)
        {
            _vertices = meshData.Vertices;
            _triangles = meshData.Triangles;
            _uvs = meshData.UVs;
            _colliderVertices = meshData.ColliderVertices;
            _colliderTriangles = meshData.ColliderTriangles;
        }

        #endregion

        #region Add Vertex

        public void AddVertex(float x, float y, float z)
        {
            _vertices.Add(new Vector3(x, y, z));
        }

        public void AddVertex(Vector3 vertex)
        {
            _vertices.Add(vertex);
        }

        public void AddVertexToCollider(float x, float y, float z)
        {
            _colliderVertices.Add(new Vector3(x, y, z));
        }

        public void AddVertexToCollider(Vector3 vertex)
        {
            _colliderVertices.Add(vertex);
        }

        public void AddUV(float x, float y)
        {
            _uvs.Add(new Vector2(x, y));
        }

        public void AddUV(Vector2 uv)
        {
            _uvs.Add(uv);
        }

        #endregion

        #region Add Triangle

        public void AddTriangle(int i1, int i2, int i3, int subMesh)
        {
            _triangles[subMesh].Add(i1);
            _triangles[subMesh].Add(i2);
            _triangles[subMesh].Add(i3);
        }

        public void AddTriangle(Vector3 v1, Vector3 v2, Vector3 v3, int subMesh)
        {
            _vertices.Add(v1);
            _vertices.Add(v2);
            _vertices.Add(v3);

            var tris = _triangles[subMesh];
            tris.Add(_vertices.Count - 3);
            tris.Add(_vertices.Count - 2);
            tris.Add(_vertices.Count - 1);
        }

        public void AddTriangleToCollider(int i1, int i2, int i3)
        {
            _colliderTriangles.Add(i1);
            _colliderTriangles.Add(i2);
            _colliderTriangles.Add(i3);
        }

        public void AddTriangleToColliders(Vector3 v1, Vector3 v2, Vector3 v3)
        {
            _colliderVertices.Add(v1);
            _colliderVertices.Add(v2);
            _colliderVertices.Add(v3);

            _colliderTriangles.Add(_colliderVertices.Count - 3);
            _colliderTriangles.Add(_colliderVertices.Count - 2);
            _colliderTriangles.Add(_colliderVertices.Count - 1);
        }

        public void AddTriangleUVs(Vector2 uv1, Vector2 uv2, Vector2 uv3)
        {
            // TODO: Test this function
            _uvs.Add(uv1);
            _uvs.Add(uv2);
            _uvs.Add(uv3);
        }

        #endregion

        #region Add Quad

        public void AddQuad(Vector3 position, OrdinalDirections dir, float size, int submesh)
        {
            switch (dir)
            {
                case OrdinalDirections.North:
                    _vertices.Add(position + new Vector3(+size, -size, 0));
                    _vertices.Add(position + new Vector3(+size, +size, 0));
                    _vertices.Add(position + new Vector3(-size, +size, 0));
                    _vertices.Add(position + new Vector3(-size, -size, 0));
                    break;
                case OrdinalDirections.South:
                    _vertices.Add(position + new Vector3(-size, -size, 0));
                    _vertices.Add(position + new Vector3(-size, +size, 0));
                    _vertices.Add(position + new Vector3(+size, +size, 0));
                    _vertices.Add(position + new Vector3(+size, -size, 0));
                    break;
                case OrdinalDirections.East:
                    _vertices.Add(position + new Vector3(0, -size, -size));
                    _vertices.Add(position + new Vector3(0, +size, -size));
                    _vertices.Add(position + new Vector3(0, +size, +size));
                    _vertices.Add(position + new Vector3(0, -size, +size));
                    break;
                case OrdinalDirections.West:
                    _vertices.Add(position + new Vector3(0, -size, +size));
                    _vertices.Add(position + new Vector3(0, +size, +size));
                    _vertices.Add(position + new Vector3(0, +size, -size));
                    _vertices.Add(position + new Vector3(0, -size, -size));
                    break;
                case OrdinalDirections.Up:
                    _vertices.Add(position + new Vector3(-size, 0, +size));
                    _vertices.Add(position + new Vector3(+size, 0, +size));
                    _vertices.Add(position + new Vector3(+size, 0, -size));
                    _vertices.Add(position + new Vector3(-size, 0, -size));
                    break;
                case OrdinalDirections.Down:
                    _vertices.Add(position + new Vector3(-size, 0, -size));
                    _vertices.Add(position + new Vector3(+size, 0, -size));
                    _vertices.Add(position + new Vector3(+size, 0, +size));
                    _vertices.Add(position + new Vector3(-size, 0, +size));
                    break;
                default:
                    break;
            }

            AddQuadTriangles(submesh);
        }

        public void AddQuadToCollider(Vector3 position, OrdinalDirections dir, float size)
        {
            switch (dir)
            {
                case OrdinalDirections.North:
                    _colliderVertices.Add(position + new Vector3(+size, -size, 0));
                    _colliderVertices.Add(position + new Vector3(+size, +size, 0));
                    _colliderVertices.Add(position + new Vector3(-size, +size, 0));
                    _colliderVertices.Add(position + new Vector3(-size, -size, 0));
                    break;
                case OrdinalDirections.South:
                    _colliderVertices.Add(position + new Vector3(-size, -size, 0));
                    _colliderVertices.Add(position + new Vector3(-size, +size, 0));
                    _colliderVertices.Add(position + new Vector3(+size, +size, 0));
                    _colliderVertices.Add(position + new Vector3(+size, -size, 0));
                    break;
                case OrdinalDirections.East:
                    _colliderVertices.Add(position + new Vector3(0, -size, -size));
                    _colliderVertices.Add(position + new Vector3(0, +size, -size));
                    _colliderVertices.Add(position + new Vector3(0, +size, +size));
                    _colliderVertices.Add(position + new Vector3(0, -size, +size));
                    break;
                case OrdinalDirections.West:
                    _colliderVertices.Add(position + new Vector3(0, -size, +size));
                    _colliderVertices.Add(position + new Vector3(0, +size, +size));
                    _colliderVertices.Add(position + new Vector3(0, +size, -size));
                    _colliderVertices.Add(position + new Vector3(0, -size, -size));
                    break;
                case OrdinalDirections.Up:
                    _colliderVertices.Add(position + new Vector3(-size, 0, +size));
                    _colliderVertices.Add(position + new Vector3(+size, 0, +size));
                    _colliderVertices.Add(position + new Vector3(+size, 0, -size));
                    _colliderVertices.Add(position + new Vector3(-size, 0, -size));
                    break;
                case OrdinalDirections.Down:
                    _colliderVertices.Add(position + new Vector3(-size, 0, -size));
                    _colliderVertices.Add(position + new Vector3(+size, 0, -size));
                    _colliderVertices.Add(position + new Vector3(+size, 0, +size));
                    _colliderVertices.Add(position + new Vector3(-size, 0, +size));
                    break;
                default:
                    break;
            }

            AddQuadTrianglesToCollider();
        }

        private void AddQuadTriangles(int subMesh)
        {
            AddTriangle(_vertices.Count - 4, _vertices.Count - 3, _vertices.Count - 2, subMesh);
            AddTriangle(_vertices.Count - 4, _vertices.Count - 2, _vertices.Count - 1, subMesh);
        }

        private void AddQuadTrianglesToCollider()
        {
            AddTriangleToCollider(_colliderVertices.Count - 4, _colliderVertices.Count - 3, _colliderVertices.Count - 2);
            AddTriangleToCollider(_colliderVertices.Count - 4, _colliderVertices.Count - 2, _colliderVertices.Count - 1);
        }

        public void AddQuadUVs(Rect uv, OrdinalDirections dir)
        {
            switch (dir)
            {
                case OrdinalDirections.Up:
                    _uvs.Add(uv.position + new Vector2(0, uv.height));
                    _uvs.Add(uv.position + uv.size);
                    _uvs.Add(uv.position + new Vector2(uv.width, 0));
                    _uvs.Add(uv.position);
                    break;
                case OrdinalDirections.Down:
                    _uvs.Add(uv.position + new Vector2(uv.width, 0));
                    _uvs.Add(uv.position);
                    _uvs.Add(uv.position + new Vector2(0, uv.height));
                    _uvs.Add(uv.position + uv.size);
                    break;
                case OrdinalDirections.North:
                case OrdinalDirections.East:
                case OrdinalDirections.South:
                case OrdinalDirections.West:
                    _uvs.Add(uv.position);
                    _uvs.Add(uv.position + new Vector2(0, uv.height));
                    _uvs.Add(uv.position + uv.size);
                    _uvs.Add(uv.position + new Vector2(uv.width, 0));
                    break;
                default:
                    break;
            }
        }

        #endregion

        #region To Mesh

        public Mesh CreateMesh()
        {
            Mesh mesh = new Mesh();
            mesh.vertices = _vertices.ToArray();
            for (int i = 0; i < _triangles.Length; i++)
            {
                mesh.SetTriangles(_triangles[i].ToArray(), i);
            }
            mesh.uv = _uvs.ToArray();

            mesh.RecalculateBounds();
            mesh.RecalculateNormals();

            return mesh;
        }

        public Mesh CreateColliderMesh()
        {
            Mesh mesh = new Mesh();
            mesh.vertices = _colliderVertices.ToArray();
            mesh.triangles = _colliderTriangles.ToArray();

            mesh.RecalculateBounds();
            mesh.RecalculateNormals();

            return mesh;
        }

        #endregion

        public void Clear()
        {
            _vertices.Clear();
            for (int i = 0; i < _triangles.Length; i++)
                _triangles[i].Clear();
            _uvs.Clear();
            _colliderVertices.Clear();
            _colliderTriangles.Clear();
        }
    }
}