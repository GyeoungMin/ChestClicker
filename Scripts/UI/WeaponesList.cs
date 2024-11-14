using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class WeaponesList : MonoBehaviour
{
    [SerializeField] GameObject prefabCell;
    [SerializeField] Transform contents;
    private SpriteAtlas atlas;

    void Start()
    {
        atlas = AtlasManager.Instance.AtlasWeapones;
        int count = atlas.spriteCount;
        for (int i = 0; i < count; i++)
        {
            string index = i.ToString();
            GameObject cellGo = Instantiate(prefabCell,contents);
            var cell = cellGo.GetComponent<WeaponesCell>();
            cell.Init(atlas.GetSprite(index));
        }
    }
}