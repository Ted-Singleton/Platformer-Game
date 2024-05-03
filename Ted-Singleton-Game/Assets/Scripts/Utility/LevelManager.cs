using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    //we need a dictionary to map the level names to their ID
    private Dictionary<string, int> levelIDs;

    private void Awake()
    {
        //initialize the dictionary
        levelIDs = new Dictionary<string, int>
        {
            { "Level1-1", 0 },
            { "Level1-2", 1 }
            //any new levels should be added here
        };
    }

    public int GetLevelID(string levelName)
    {
        //check if the level name exists in the dictionary
        if (!levelIDs.ContainsKey(levelName))
        {
            //if it doesn't, log an error and return -1
            Debug.LogError("Level name not found in dictionary");
            return -1;
        }
        else
        {
            //if it does, return the level ID
            return levelIDs[levelName];
        }
    }
}
