using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponesCell : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private GameObject locking;

    public void Init(Sprite icon)
    {
        this.icon.sprite = icon;
    }

    void UnLock()
    {
        locking.SetActive(false);
    }
}
