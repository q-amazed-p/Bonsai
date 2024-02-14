using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MathfExt
{
    public static float BonsaiFunct(float baseVal, float scale, int lvl)
    {
        return baseVal * Mathf.Pow(scale, (lvl - 1));       
    }

    /// <summary>
    /// Checks if a float value fits between the range between positive and negative of another float value.
    /// </summary>
    /// <param name="a"> tested float </param>
    /// <param name="signlessLimit"> float marking range of testing (from -signlessLimit to signlessLimit) </param>
    /// <returns> true - x within range; false - x out of range </returns>
    public static bool IsBoundBy(float a, float signlessLimit)
    {
        return Mathf.Abs(a) < signlessLimit;
    }

    /// <summary>
    /// Checks if a float value fits between the range between two other float values.
    /// </summary>
    /// <param name="x"> tested float </param>
    /// <param name="min"> float marking the lower bound of range of testing </param>
    /// <param name="max"> float marking the higher bound of range of testing </param>
    /// <returns> true - x within range; false - x out of range </returns>
    public static bool IsBoundBy(float x, float min, float max)
    {
        return Mathf.Abs(x-(max+min)/2) < (max-min)/2; 
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="vector"></param>
    /// <param name="minX"></param>
    /// <param name="maxX"></param>
    /// <param name="minY"></param>
    /// <param name="maxY"></param>
    /// <returns></returns>
    public static bool Vector3IsPlaneBoundBy(Vector3 vector, float minX, float maxX, float minY, float maxY)
    {
        return IsBoundBy(vector.x, minX, maxX) && IsBoundBy(vector.y, minY, maxY);
    }
}
