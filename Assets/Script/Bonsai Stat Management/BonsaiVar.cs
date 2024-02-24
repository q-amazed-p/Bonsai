using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct BonsaiVar
{
    float varBase;
    //public float VarBase => varBase;

    float varScale;
    //public float VarScale => varScale;

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
