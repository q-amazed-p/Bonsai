using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MathfExt
{
    public static float BonsaiFunct(float baseVal, float scale, int lvl)
    {
        return baseVal * Mathf.Pow(scale, (lvl - 1));       
    }
}
