using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VertexData = System.Tuple<UnityEngine.Vector3, UnityEngine.Vector3, UnityEngine.Vector2>;

namespace ProcedualWorld
{
    public static class MeshUtils
    {
        public enum BlockType
        {
            GRASS_TOP,
            GRASS_SIDE,
            SAND,
            DIRT,
            STONE,
            WATER
        }
        public static Mesh MergeMeshes(Mesh[] meshes)
        {
            Mesh resultMesh = new Mesh();
            Dictionary<VertexData, int> pointsOrder = new Dictionary<VertexData, int>();
            HashSet<VertexData> pointsHash = new HashSet<VertexData>();
            List<int> tris = new List<int>();

            int pIndex = 0;
            for (int i = 0; i < meshes.Length; i++)
            {
                if (meshes[i] == null)
                {
                    continue;
                }
                for (int j = 0; j < meshes[i].vertices.Length; j++)
                {
                    Vector3 v = meshes[i].vertices[j];
                    Vector3 n = meshes[i].normals[j];
                    Vector2 u = meshes[i].uv[j];
                    VertexData p = new VertexData(v, n, u);

                    if (!pointsHash.Contains(p))
                    {
                        pointsOrder.Add(p, pIndex++);
                        pointsHash.Add(p);
                    }
                }
                for (int t = 0; t < meshes[i].triangles.Length; t++)
                {
                    int triPoit = meshes[i].triangles[t];
                    Vector3 v = meshes[i].vertices[triPoit];
                    Vector3 n = meshes[i].normals[triPoit];
                    Vector2 u = meshes[i].uv[triPoit];
                    VertexData p = new VertexData(v, n, u);

                    int index;
                    pointsOrder.TryGetValue(p, out index);
                    //提取tris
                    tris.Add(index);
                }

                meshes[i] = null;
            }
            //提取vertices,normal,uv
            ExtractArrays(pointsOrder, resultMesh);
            resultMesh.triangles = tris.ToArray();
            resultMesh.RecalculateBounds();
            return resultMesh;

        }

        public static void ExtractArrays(Dictionary<VertexData, int> list, Mesh mesh)
        {
            List<Vector3> verts = new List<Vector3>();
            List<Vector3> norms = new List<Vector3>();
            List<Vector2> uvs = new List<Vector2>();

            foreach (var v in list.Keys)
            {
                verts.Add(v.Item1);
                norms.Add(v.Item2);
                uvs.Add(v.Item3);
            }

            mesh.vertices = verts.ToArray();
            mesh.normals = norms.ToArray();
            mesh.uv = uvs.ToArray();
        }
    }
}
