using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ProcedualWorld.MeshUtils;

namespace ProcedualWorld
{
    public class Block
    {
        public Mesh mesh;
        public enum BlockSide
        {
            FRONT,
            BACK,
            LEFT,
            RIGHT,
            UP,
            DOWN,
        }
        public Material atlas;

        public Block(Vector3 offset, BlockType blockType)
        {
            Vector2Int blockUVs = new Vector2Int(0, 0);
            switch (blockType)
            {
                case BlockType.GRASS_TOP:
                    blockUVs = new Vector2Int(1, 6);
                    break;
                case BlockType.GRASS_SIDE:
                    blockUVs = new Vector2Int(3, 15);
                    break;
                case BlockType.SAND:
                    blockUVs = new Vector2Int(0, 3);
                    break;
                case BlockType.DIRT:
                    blockUVs = new Vector2Int(2, 15);
                    break;
                case BlockType.STONE:
                    blockUVs = new Vector2Int(1, 15);
                    break;
                case BlockType.WATER:
                    blockUVs = new Vector2Int(15, 3);
                    break;
                default:
                    break;
            }

            Quad[] quads = new Quad[6];
            quads[0] = new Quad(BlockSide.FRONT, offset, blockUVs);
            quads[1] = new Quad(BlockSide.BACK, offset, blockUVs);
            quads[2] = new Quad(BlockSide.LEFT, offset, blockUVs);
            quads[3] = new Quad(BlockSide.RIGHT, offset, blockUVs);
            quads[4] = new Quad(BlockSide.UP, offset, blockUVs);
            quads[5] = new Quad(BlockSide.DOWN, offset, blockUVs);

            Mesh[] sideMeshes = new Mesh[6];
            for (int i = 0; i < quads.Length; i++)
            {
                sideMeshes[i] = quads[i].mesh;
            }

            mesh = MeshUtils.MergeMeshes(sideMeshes);
            mesh.name = "Cube_0_0_0";
        }
    }
}
