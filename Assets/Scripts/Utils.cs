using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{

    public static float CustomNormalize(float val,float newMin,float newMax)
    {
        float newVal;

        newVal = 2 * (val - newMin) / (newMax - newMin) - 1;


        return newVal;
    }

}
