using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCompleteScreen : MonoBehaviour
{
    private string levelName;
    //we need the textmeshpro gameobject that contains the level name text\
    public TextMeshProUGUI levelNameText;
    public TextMeshProUGUI levelTime;

    private void Start()
    {
        //get the current scene name
        levelName = LevelData.levelName;
        //set the text to the current scene name
        Debug.Log(levelName);
        levelNameText = GameObject.Find("LevelName").GetComponent<TextMeshProUGUI>();
        levelNameText.text = levelName;

        //set the time text to the time taken to complete the level
        Debug.Log(levelTime);
        levelTime = GameObject.Find("LevelTime").GetComponent<TextMeshProUGUI>();
        levelTime.text = "Time: " + LevelData.time.ToString("F2") + " seconds";
    }

    //load the next level
    public void Next()
    {
        //get the current scene name
        string currentSceneName = levelName;
        //names are formatted as LevelX-Y where X is the chapter, and Y is the level number
        //split the name by the dash
        string[] split = currentSceneName.Split('-');
        //get the chapter number
        int chapter = int.Parse(split[0].Substring(5));
        //get the level number
        int level = int.Parse(split[1]);
        //increment the level number
        level++;
        //we have 4 levels per chapter, so we want to go the the next chapter if level 4 was completed
        //if the level number is now 5, increment the chapter number and set the level number to 1
        if (level == 5)
        {
            chapter++;
            level = 1;
        }
        string nextLevel = "Level" + chapter + "-" + level;
        Debug.Log(nextLevel);
        //if the level exists, load it
        if (SceneExists(nextLevel))
        {
            SceneManager.LoadScene(nextLevel);
        }
        //otherwise, return to the main menu
        else
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    //check if a scene exists by its name
    private bool SceneExists(string sceneName)
    {
        //loop through all the scenes in the build settings
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            //get the scene path
            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            //split the path by the slash
            string[] split = scenePath.Split('/');
            //get the scene name
            string name = split[split.Length - 1].Split('.')[0];
            Debug.Log(name);
            //if the scene name matches the name we're looking for, return true
            if (name == sceneName)
            {
                return true;
            }
        }
        //if we didn't find the scene, return false
        return false;
    }

    public void Retry()
    {
        //reload the current level
        SceneManager.LoadScene(levelName);
    }

    public void MainMenu()
    {
        //return to the main menu
        SceneManager.LoadScene("MainMenu");
    }
}
