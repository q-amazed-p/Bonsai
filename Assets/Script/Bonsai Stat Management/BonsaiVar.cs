using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonsaiVar
{
    float varBase;
    float varScale;

    public BonsaiVar(float baseVal, float newScale)
    {
        varBase = baseVal;
        varScale = newScale;
    }

    public float AdvanceStat(int lvl)
    {
        return MathfExt.BonsaiFunct(varBase, varScale, lvl);
    }


}
