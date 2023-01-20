using MultipleTxture;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

namespace ProcedualWorld
{
    public class PerlinNoiseMap : MonoBehaviour
    {
        Dictionary<int, Sprite> tileset;
        [SerializeField]
        TextureManager textureManager;
        [SerializeField]
        private GameObject prefab_terrain;
        [SerializeField]
        private string seed;

        private int SeedToInt()
        {
            return seed.GetHashCode() % 100;
        }

        public int map_width = 32;
        public int map_height = 18;

        public float lacunarity = 2;

        private void Start()
        {
            CreateTileset();
            GenerateMap();
        }

        private void CreateTileset()
        {
            tileset = new Dictionary<int, Sprite>();
            tileset.Add(0, textureManager.GetTerrain("TEX_plain"));
            tileset.Add(1, textureManager.GetTerrain("TEX_hill"));
            tileset.Add(2, textureManager.GetTerrain("TEX_mountain"));
            tileset.Add(3, textureManager.GetTerrain("TEX_forest"));
        }

        private void GenerateMap()
        {
            for (int x = 0; x < map_width; x++)
            {
                for (int y = 0; y < map_height; y++)
                {
                    int tile_id = GetIdUsingPerlin(x, y);
                    CreateTile(tile_id, x, y);
                }
            }
        }

        private int GetIdUsingPerlin(int x, int y)
        {
            float rawPerlin = Mathf.PerlinNoise((float)x / map_width * lacunarity + SeedToInt(), (float)y / map_height * lacunarity + SeedToInt());
            float clamp_perlin = Mathf.Clamp(rawPerlin, 0, 1);
            float scale_perlin = clamp_perlin * tileset.Count;
            if (scale_perlin == 4)
            {
                scale_perlin = 3;
            }
            return Mathf.FloorToInt(scale_perlin);
        }

        private void CreateTile(int tile_id, int x, int y)
        {
            Sprite tileSprite = tileset[tile_id];
            GameObject tile = Instantiate(prefab_terrain, transform);
            tile.GetComponent<SpriteRenderer>().sprite = tileSprite;

            tile.name = string.Format("tile_x{0}_y{1}", x, y);
            tile.transform.localPosition = new Vector3(x, y, 0);
        }
    }
}
