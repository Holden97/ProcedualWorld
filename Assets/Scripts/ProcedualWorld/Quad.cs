using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace ProcedualWorld
{
    public class Quad
    {
        public static readonly Vector2 QuadSize = new Vector2(0.0625f, 0.0625f);
        public Mesh mesh;
        public Quad(Block.BlockSide side, Vector3 posOffset, Vector2Int blockVec2_00)
        {
            mesh = new Mesh();
            mesh.name = "SingleQuad";

            Vector3[] vertices = new Vector3[4];
            Vector3[] normals = new Vector3[4];
            Vector2[] uvs = new Vector2[4];
            int[] triangles = new int[6];

            var orginalX = QuadSize.x * blockVec2_00.x;
            var orginalY = QuadSize.y * blockVec2_00.y;
            Vector2 uv00 = new Vector2(orginalX, orginalY);
            Vector2 uv10 = new Vector2(orginalX + QuadSize.x, orginalY);
            Vector2 uv01 = new Vector2(orginalX, orginalY + QuadSize.y);
            Vector2 uv11 = new Vector2(orginalX + QuadSize.x, orginalY + QuadSize.y);

            Vector3 p0 = new Vector3(-0.5f, -0.5f, 0.5f) + posOffset;
            Vector3 p1 = new Vector3(0.5f, -0.5f, 0.5f) + posOffset;
            Vector3 p2 = new Vector3(0.5f, -0.5f, -0.5f) + posOffset;
            Vector3 p3 = new Vector3(-0.5f, -0.5f, -0.5f) + posOffset;

            Vector3 p4 = new Vector3(-0.5f, 0.5f, 0.5f) + posOffset;
            Vector3 p5 = new Vector3(0.5f, 0.5f, 0.5f) + posOffset;
            Vector3 p6 = new Vector3(0.5f, 0.5f, -0.5f) + posOffset;
            Vector3 p7 = new Vector3(-0.5f, 0.5f, -0.5f) + posOffset;

            uvs = new Vector2[] { uv11, uv01, uv00, uv10 };
            triangles = new int[]
            {
                3,1,0,3,2,1
            };

            switch (side)
            {
                case Block.BlockSide.FRONT:
                    vertices = new Vector3[] { p4, p5, p1, p0 };
                    normals = new Vector3[] { Vector3.forward, Vector3.forward, Vector3.forward, Vector3.forward };
                    break;
                case Block.BlockSide.BACK:
                    vertices = new Vector3[] { p6, p7, p3, p2 };
                    normals = new Vector3[] { Vector3.back, Vector3.back, Vector3.back, Vector3.back };
                    break;
                case Block.BlockSide.LEFT:
                    vertices = new Vector3[] { p5, p6, p2, p1 };
                    normals = new Vector3[] { Vector3.left, Vector3.left, Vector3.left, Vector3.left };
                    break;
                case Block.BlockSide.RIGHT:
                    vertices = new Vector3[] { p7, p4, p0, p3 };
                    normals = new Vector3[] { Vector3.right, Vector3.right, Vector3.right, Vector3.right };
                    break;
                case Block.BlockSide.UP:
                    vertices = new Vector3[] { p7, p6, p5, p4 };
                    normals = new Vector3[] { Vector3.up, Vector3.up, Vector3.up, Vector3.up };
                    break;
                case Block.BlockSide.DOWN:
                    vertices = new Vector3[] { p0, p1, p2, p3 };
                    normals = new Vector3[] { Vector3.down, Vector3.down, Vector3.down, Vector3.down };
                    break;
                default:
                    break;
            }

            mesh.vertices = vertices;
            mesh.normals = normals;
            mesh.uv = uvs;
            mesh.triangles = triangles;

            mesh.RecalculateBounds();
        }
    }
}
