using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class SaveData
{
    public Color teamColor;
}

public class MainManager : MonoBehaviour
{
    public static MainManager Instance { get; private set; }
    public Color TeamColor { get; set; }
    private static string SavePath;

    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        SavePath = Application.persistentDataPath + "/savefile.json";
        Debug.Log(SavePath);
        LoadColor();
    }

    public void SaveColor()
    {
        var data = new SaveData();
        data.teamColor = TeamColor;

        var json = JsonUtility.ToJson(data);
        File.WriteAllText(SavePath, json);
    }

    public void LoadColor()
    {
        if (File.Exists(SavePath))
        {
            var json = File.ReadAllText(SavePath);
            var data = JsonUtility.FromJson<SaveData>(json);
            TeamColor = data.teamColor;
        }
    }
}
