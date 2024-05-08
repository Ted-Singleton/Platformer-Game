using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
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

        //get the current scene name
        levelName = LevelData.levelName;
        //set the text to the current scene name
        levelNameText = GameObject.Find("LevelName").GetComponent<TextMeshProUGUI>();
        levelNameText.text = levelName;

        levelTime = LevelData.time;
        //set the time text to the time taken to complete the level
        levelTimeText = GameObject.Find("LevelTime").GetComponent<TextMeshProUGUI>();
        levelTimeText.text = "Time: " + LevelData.time.ToString("F2") + "s";

        collectibleGathered = LevelData.collectibleGathered;
        //set the collectible text to "Collected" if the collectible was gathered, and "Not Collected" otherwise
        collectibleText = GameObject.Find("CollectibleText").GetComponent<TextMeshProUGUI>();
        collectibleText.color = collectibleGathered ? collectibleColor : collectibleText.color;
        collectibleText.text = collectibleGathered ? "Collected!" : "Not Collected";

        //we use the database manager to save the level data to the database

        /* NOTE: we're using an ID of 0 here, as we don't have a player ID system in place yet
        in a full game, we would most likely use something like the player's Steam ID or some other unique account identifier
        this would allow us to track the player's progress across multiple devices, as well as allow for the display of profile pictures and other player-specific data*/
        DatabaseManager.WriteTimeToDatabase(levelName, 0, levelTime);

        //now we need to save the level completion status to the local save file
        SaveToFile();
    }

    //update: we don't need to use Update() for this part, as you can set the load order of scripts in Project Settings -> Script Execution Order
    //so, we CAN guarantee that the LevelManager and SaveManager will be ready before this script runs, and we can use FindObjectOfType to get them
    private void SaveToFile()
    {
        //we need the level manager to get the level ID
        LevelManager levelManager = FindObjectOfType<LevelManager>();
        int levelID = levelManager.GetLevelID(levelName);

        //if the level ID is -1, we just return, and the GetLevelID call will have logged an error
        if (levelID == -1) return;

        //we use the save manager to save the level completion status
        SaveManager saveManager = FindObjectOfType<SaveManager>();
        saveManager.CompleteLevel(levelID, collectibleGathered);
    }
}
