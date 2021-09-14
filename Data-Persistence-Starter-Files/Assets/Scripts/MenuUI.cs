using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

// Sets the script to be executed later than all default scripts
// This is helpful for UI, since other things may need to be initialized before setting the UI
[DefaultExecutionOrder(1000)]
public class MenuUI : MonoBehaviour
{
    [SerializeField]
    private TMP_Text scoreText;

    private void Awake()
    {
        var record = DataStore.Instance.Record;
        if (record.point > 0)
        {
            scoreText.text = "BestScore: " + record.name + ": " + record.point;
        }
        else
        {
            scoreText.text = "";
        }
    }

    public void OnStart()
    {
        SceneManager.LoadScene(1);
    }

    public void OnQuit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    public void OnEndNameInput(InputField input)
    {
        if (input.text.Length > 0)
        {
            DataStore.Instance.Name = input.text;
        }
        Debug.Log("Name Input: " + DataStore.Instance.Name);
    }
}
