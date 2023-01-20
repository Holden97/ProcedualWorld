using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MultipleTxture
{
    [CreateAssetMenu(fileName = "TextureDatabase", menuName = "ScriptableObject/TextureDatabase")]
    public class TextureDatabase : ScriptableObject
    {
        public List<TextureDetail> textureDetailList;

        public Sprite Get(string terrainName)
        {
            var item = textureDetailList.Find(x => x.spriteName == terrainName);
            if (item == null)
            {
                return null;
            }
            var itemSprite = item.sprite;
            if (itemSprite == null)
            {
                return null;
            }
            return itemSprite;
        }
    }
}
