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
    private bool collectibleGathered;
    //we need the textmeshpro gameobject that contains the level name text\
    public TextMeshProUGUI levelNameText;
    public TextMeshProUGUI levelTimeText;
    public TextMeshProUGUI collectibleText;

    //we need to set the color of the collectible text to purple if the collectible was gathered
    private Color collectibleColor = new Color(0.478f, 0f, 0.670f);

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
        levelTimeText.text = "Time: " + LevelData.time.ToString("F2") + "s";

        collectibleGathered = LevelData.collectibleGathered;
        //set the collectible text to "Collected" if the collectible was gathered, and "Not Collected" otherwise
        collectibleText = GameObject.Find("CollectibleText").GetComponent<TextMeshProUGUI>();
        collectibleText.color = collectibleGathered ? collectibleColor : collectibleText.color;
        collectibleText.text = collectibleGathered ? "Collected!" : "Not Collected";

        //we use the database manager to save the level data to the database
        /* As a note, the intent here is to only save the player's time to the database, not whether they gathered the collectible
           the collectible is meant as a personal challenge for the player, and not a requirement to complete the level and compete with other players*/
        string query = $"INSERT INTO PlayerTimes(LevelID, PlayerID, CompletionTime) VALUES ('{levelName}', '{0}', '{levelTime}')";
        DatabaseManager.ExecuteQuery(query);
    }
}
