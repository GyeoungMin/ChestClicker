using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class AtlasManager : MonoBehaviour
{
    private static AtlasManager instance;

    [SerializeField] private SpriteAtlas atlasWeapones;

    public SpriteAtlas AtlasWeapones { get { return atlasWeapones; } }

    public static AtlasManager Instance { get { return instance; } }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }
}