using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerStats
{
    public static BonsaiVar Storage;
    public static BonsaiVar Growth;
    public static BonsaiVar LvlCost;
    public static BonsaiVar FruitCost;

    static bool initialized = false;
    public static void Initialize(float storB, float storS, float growB, float growS, float lvlCB, float lvLCS, float fruitCB, float FruitCS)
    {
        if (!initialized)
        {
            Storage = new BonsaiVar(storB, storS);
            Growth = new BonsaiVar(growB, growS);
            LvlCost = new BonsaiVar(lvlCB, lvLCS);
            FruitCost = new BonsaiVar(fruitCB, FruitCS);
            initialized = true;
        }
    }
}
