using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace ProcedualWorld
{
    public class Chunk : MonoBehaviour
    {
        public Material atlas;
        public int width = 2;
        public int height = 2;
        public int depth = 2;

        public Block[,,] blocks;
        private void Start()
        {
            MeshFilter mf = gameObject.AddComponent<MeshFilter>();
            MeshRenderer mr = gameObject.AddComponent<MeshRenderer>();
            mr.material = atlas;
            blocks = new Block[width, height, depth];

            for (int z = 0; z < depth; z++)
            {
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        blocks[x, y, z] = new Block(new Vector3(x, y, z), MeshUtils.BlockType.DIRT);
                    }
                }
            }
        }
    }
}
