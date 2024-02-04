using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StemStats
{
    public static BonsaiVar Storage;
    public static BonsaiVar Growth;
    public static BonsaiVar LvlCost;
    public static BonsaiVar BranchCost;
    public static BonsaiVar LeafCost;
    public static BonsaiVar Boost;

    static bool initialized = false;
    public static void Initialize(float storB, float storS, float growB, float growS, float lvlCB, float lvLCS, float branCB, float branCS, float leafCB, float leafCS, float boostB, float boostC)
    {
        if (!initialized)
        {
            Storage = new BonsaiVar(storB, storS);
            Growth = new BonsaiVar(growB, growS);
            LvlCost = new BonsaiVar(lvlCB, lvLCS);
            BranchCost = new BonsaiVar(branCB, branCS);
            LeafCost = new BonsaiVar(leafCB, leafCS);
            Boost = new BonsaiVar(boostB, boostC);
            initialized = true;
        }
    }
}
