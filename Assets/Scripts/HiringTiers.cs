using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct HiringTiers
{
    public int employeeThreshold;
    public int daysToFileNewDocument;

    public HiringTiers(int employeeThreshold, int daysToFileNewDocument)
    {
        this.employeeThreshold = employeeThreshold;
        this.daysToFileNewDocument = daysToFileNewDocument;
    }
}
