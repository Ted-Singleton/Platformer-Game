using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    //we need an int to store the level id, which we'll set in the inspector
    public int levelID;

    //we need some booleans to store the level's completion status
    private bool levelUnlocked = false;
    private bool levelCompleted = false;
    private bool collectibleGathered = false;


    //we need a reference to the save manager and the player progress object
    private SaveManager saveManager;
    private PlayerProgress playerProgress;

    //we'll also need a reference to the LevelManager object to get the level's scene name
    private LevelManager levelManager;

    private void Start()
    {
        //get the save manager and its player progress object
        //we'll need these to check the level's status in the player's save file
        saveManager = FindObjectOfType<SaveManager>();
        playerProgress = saveManager.GetPlayerProgress();

        //we need the level manager to get the level's scene name
        levelManager = FindObjectOfType<LevelManager>();

        //first we check if the level has been completed, this can save us time with checking if the level is unlocked
        //we'll do this in a try-catch block to avoid errors if the level id is out of range during development
        try
        {
            //we get the level's completion status from the player progress object
            levelCompleted = playerProgress.levelsCompleted[levelID];
            collectibleGathered = playerProgress.collectibles[levelID];

            //if the level has been completed, or is the first, or the previous level has been completed, we can unlock the level
            if (levelCompleted || (levelID == 0) || playerProgress.levelsCompleted[levelID - 1])
            {
                levelUnlocked = true;
            }

            //if the level is not unlocked, we can disable the object
            if (!levelUnlocked)
            {
                gameObject.SetActive(false);
                return;
            }

            //if the level is unlocked, we can set the object's text to show the level's status
            //we can also change the object's color to show the level's status
            //to do this, we'll need to get the text component from the object
            TextMeshProUGUI text = gameObject.GetComponentInChildren<TextMeshProUGUI>();

            //if the level has been completed, we check if the collectible has been gathered
            //we can then set the text and color according to whichever status the level is in
            text.text = $"{levelManager.GetLevelName(levelID)} " + "\n" +
                        (levelCompleted ? (collectibleGathered ? "Perfected" : "Completed") : "Incomplete");
            text.color = levelCompleted ? (collectibleGathered ? Color.green : Color.yellow) : Color.red;
        }
        //we only really want to catch IndexOutOfRangeExceptions, and we can write to the log when this happens
        //we can also disable the object if the level is out of range
        catch (IndexOutOfRangeException e)
        {
            Debug.Log("Level with ID " + levelID + " is currently in development");
            Debug.Log(e.Message);
            gameObject.SetActive(false);
        }
    }

    public void LoadLevel()
    {
        //we can load the level if it's unlocked
        if (levelUnlocked)
        {
            string sceneName = levelManager.GetLevelName(levelID);
            SceneManager.LoadScene(sceneName);
        }
    }
}
