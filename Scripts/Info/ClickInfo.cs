using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickInfo
{
    public long totalCount;
    public int hitCount;
    public int gold;
    public int crystal;

    public ClickInfo(long totalCount, int hitCount, int gold, int crystal)
    {
        this.totalCount = totalCount;
        this.hitCount = hitCount;
        this.gold = gold;
        this.crystal = crystal;
    }
}
