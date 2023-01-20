using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MultipleTxture
{
    public class TextureManager : MonoBehaviour
    {
        [SerializeField]
        private TextureDatabase database;


        public Sprite GetTerrain(string terrainName)
        {
            return database.Get(terrainName);
        }

    }
}
