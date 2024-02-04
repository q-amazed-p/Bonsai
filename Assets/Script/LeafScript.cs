using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafScript : PlantPartFam
{
    static float baseGrowthRate = 1;
    static float baseLvlUpCost = 50;
    static float baseMaxStorage = 100;


    void BuyLevelUp()
    {
        if (TryPayGrowth(lvlUpCost))
        {
            lvl++;
            lvlUpCost = StemStats.LvlCost.AdvanceStat(lvl);
            growthRate = StemStats.Growth.AdvanceStat(lvl);
            maxStorage = StemStats.Storage.AdvanceStat(lvl);
        }
    }

    private void Awake()
    {


    }

    private void Start()
    {
        lvl = 1;
        lvlUpCost = StemStats.LvlCost.AdvanceStat(lvl);
        growthRate = StemStats.Growth.AdvanceStat(lvl);
        maxStorage = StemStats.Storage.AdvanceStat(lvl);
    }

    public void FlipSprite()
    {
        mySprite.flipX = true;
    }
}
