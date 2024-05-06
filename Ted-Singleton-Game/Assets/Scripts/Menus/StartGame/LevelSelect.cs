using System.Collections;
using System.Collections.Generic;
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

        //first we check if the level has been completed, this can save us time with checking if the level is unlocked
        levelCompleted = playerProgress.levelsCompleted[levelID];

        if (levelCompleted)
        {
            //if the level has been completed, we already know it's unlocked
            levelUnlocked = true;
            //we can now check if the level's collectible has been gathered
            collectibleGathered = playerProgress.collectibles[levelID];
        }
        //if the level has not been completed, we need to check if it's unlocked
        else
        {
            //we can check the playerprogress object's levelsCompleted array to see if the level or the prior one has been completed
            //we also need to be careful of the first level, as there is no prior level
            if (levelID == 0)
            {
                levelUnlocked = true;
            }
            else
            {
                levelUnlocked = playerProgress.levelsCompleted[levelID - 1];
            }
        }
    }

    private void LoadLevel()
    {
        //we can load the level if it's unlocked
        if (levelUnlocked)
        {
            string sceneName = levelManager.GetLevelName(levelID);
            SceneManager.LoadScene(sceneName);
        }
    }
}
