using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FileSaver
{
    public static PlayerStats savedFile = new PlayerStats();
    public static readonly string _savePath = Application.dataPath + "/PlayerStats.json";

    public static void SaveToJson() => File.WriteAllText(_savePath, JsonUtility.ToJson(savedFile));

    public static void LoadFromJson() => savedFile = JsonUtility.FromJson<PlayerStats>(File.ReadAllText(_savePath));

    public static void ClearJson()
    {
        savedFile = new PlayerStats();
        SaveToJson();
    }
}
