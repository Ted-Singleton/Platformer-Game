using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCompleteScreen : MonoBehaviour
{
    private string levelName;
    private float levelTime;
    //we need the textmeshpro gameobject that contains the level name text\
    public TextMeshProUGUI levelNameText;
    public TextMeshProUGUI levelTimeText;

    private void Start()
    {
        //unlock the cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Debug.Log(this.ToString());
        //get the current scene name
        levelName = LevelData.levelName;
        //set the text to the current scene name
        Debug.Log(levelName);
        levelNameText = GameObject.Find("LevelName").GetComponent<TextMeshProUGUI>();
        levelNameText.text = levelName;

        levelTime = LevelData.time;
        //set the time text to the time taken to complete the level
        Debug.Log(levelTime);
        levelTimeText = GameObject.Find("LevelTime").GetComponent<TextMeshProUGUI>();
        levelTimeText.text = "Time: " + LevelData.time.ToString("F2") + " seconds";

        //we use the database manager to save the level data to the database
        string query = $"INSERT INTO PlayerTimes(LevelID, PlayerID, CompletionTime) VALUES ('{levelName}', '{0}', '{levelTime}')";
        DatabaseManager.ExecuteQuery(query);
    }
}
