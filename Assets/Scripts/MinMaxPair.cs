using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct MinMaxPairInt
{
    public int min;
    public int max;

    public MinMaxPairInt(int min, int max)
    {
        this.min = min;
        this.max = max;
    }
}

[System.Serializable]
public struct MinMaxPair
{
    public float min;
    public float max;

    public MinMaxPair(float min, float max)
    {
        this.min = min;
        this.max = max;
    }
}
