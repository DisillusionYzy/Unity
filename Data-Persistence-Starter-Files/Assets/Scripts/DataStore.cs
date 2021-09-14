using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class Record
{
    public int point;
    public string name = "";
}

public class DataStore : MonoBehaviour
{
    public static DataStore Instance { get; private set; }
    private static string savePath;
    public string Name { get; set; }
    public Record Record {get; set; }
    private void Awake()
    {
        if (Instance)
        {
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        savePath = Application.persistentDataPath + "/data.json";
        Record = new Record();
        LoadRecord();

        Debug.Log(savePath);
    }

    public void UpdateRecord(int point)
    {
        if (point > Record.point)
        {
            Record.point = point;
            Record.name = Name;
            SaveRecord();
        }
    }

    public void SaveRecord()
    {
        // ´æ´¢Êý¾Ý
        var json = JsonUtility.ToJson(Record);
        File.WriteAllText(savePath, json);
    }

    void LoadRecord()
    {
        if (File.Exists(savePath))
        {
            var json = File.ReadAllText(savePath);
            Record = JsonUtility.FromJson<Record>(json);
        }
    }
}
