using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldController : MonoBehaviour
{
    public GameObject block;
    public int worldWide = 10;
    public int worldHeight = 10;
    public int worldHigh = 2;

    public float probabilityToGen = 0.5f;

    public IEnumerator BuildWorld()
    {
        if (worldHigh < 2)
        {
            Debug.LogError("Too low to generate world.");
        }
        for (int z = 0; z < worldWide; z++)
        {
            for (int y = 0; y < worldHigh; y++)
            {
                for (int x = 0; x < worldHeight; x++)
                {
                    if (y >= worldHigh - 2 && (Random.Range(0f, 1f) > probabilityToGen)) continue;
                    GenerateCube(x, y, z);
                }
                yield return null;
            }
        }
    }

    private void GenerateCube(int z, int y, int x)
    {
        Vector3 pos = new Vector3(x, y, z);
        GameObject cube = GameObject.Instantiate(block, pos, Quaternion.identity);
        cube.name = x + "_" + y + "_" + z;
        cube.GetComponent<Renderer>().material = new Material(Shader.Find("Standard"));
    }

    private void Start()
    {
        StartCoroutine(BuildWorld());
    }

}
