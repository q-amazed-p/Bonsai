using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LeafStats
{
    public static BonsaiVar Storage;
    public static BonsaiVar Growth;
    public static BonsaiVar LvlCost;

    static bool initialized = false;
    public static void Initialize(float storB, float storS, float growB, float growS, float lvlCB, float lvLCS)
    {
        if (!initialized)
        {
            Storage = new BonsaiVar(storB, storS);
            Growth = new BonsaiVar(growB, growS);
            LvlCost = new BonsaiVar(lvlCB, lvLCS);
            initialized = true;
        }
    }
}
