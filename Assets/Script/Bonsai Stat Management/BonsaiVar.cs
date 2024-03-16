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


    /// <summary>
    /// Implementation for Cost Variables
    /// Provides scaling dependent on PlantPart generation
    /// </summary>
    /// <param name="lvl">the new level achieved</param>
    /// <param name="generation">generation of PlantPart</param>
    /// <param name="coefficient">scaling coefficient (should become constant after calibration)</param>
    /// <param name="inversePropotionalToGeneration"> set true if cost is to go down as generation increases </param>
    /// <returns></returns>
    public float AdvanceStat(int lvl, int generation, float coefficient, bool inversePropotionalToGeneration = false) 
    {
        return MathfExt.BonsaiFunct(MathfExt.BonsaiFunct(varBase, varScale, lvl), inversePropotionalToGeneration? 1/generation : generation, coefficient);
    }
}
