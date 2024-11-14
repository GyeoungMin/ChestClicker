using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum ChestType
{
    Wooden, Metal, Gold
}

public class ClickController : MonoBehaviour
{
    [SerializeField] ChestType type;
    Animator anim;
    public bool isHit;

    private Coroutine hitCoroutine;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void Init()
    {
        isHit = false;
        UpdateAnimator();
    }

    public void Hit()
    {
        if (hitCoroutine != null)
        {
            StopCoroutine(hitCoroutine);
        }

        isHit = true;
        UpdateAnimator();

        hitCoroutine = StartCoroutine(ResetHit());
    }

    IEnumerator ResetHit()
    {
        yield return new WaitForSeconds(0.5f);
        isHit = false;
        UpdateAnimator();
    }

    private void UpdateAnimator()
    {
        anim.SetBool("isHit", isHit);
    }



    public int GetGold()
    {
        switch (type)
        {
            case ChestType.Wooden:
                return 10;
            case ChestType.Metal:
                return 100;
            case ChestType.Gold:
                return 1000;
            default: return 10;
        }
    }

    public int GetCrystal()
    {
        switch (type)
        {
            case ChestType.Wooden:
                return 1;
            case ChestType.Metal:
                return 5;
            case ChestType.Gold:
                return 10;
            default: return 1;
        }
    }
}
