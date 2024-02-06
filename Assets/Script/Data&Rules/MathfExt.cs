using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MathfExt
{
    public static float BonsaiFunct(float baseVal, float scale, int lvl)
    {
        return baseVal * Mathf.Pow(scale, (lvl - 1));       
    }

    public static bool IsBoundBy(float a, float signlessLimit)
    {
        return Mathf.Abs(a) < signlessLimit;
    }

    public static bool IsBoundBy(float x, float min, float max)
    {
        return Mathf.Abs(x-(max+min)/2) < (max-min)/2; 
    }

    public static bool Vector3IsPlaneBoundBy(Vector3 vector, float minX, float maxX, float minY, float maxY)
    {
        return IsBoundBy(vector.x, minX, maxX) && IsBoundBy(vector.y, minY, maxY);
    }
}
