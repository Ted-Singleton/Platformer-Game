using UnityEngine;
using System.IO;

[System.Serializable]
public class PlayerProgress
{
    //we want to save the levels completed and the collectibles collected
    //we will use a pair of boolean arrays to store these values
    public bool[] levelsCompleted;
    public bool[] collectibles;

    public PlayerProgress(int numberOfLevels)
    {
        //initialize the arrays
        levelsCompleted = new bool[numberOfLevels];
        collectibles = new bool[numberOfLevels];
    }
}

public class SaveManager : MonoBehaviour
{
    //we need a reference to the PlayerProgress object
    private PlayerProgress playerProgress;

    private void Start()
    {
        //load the saved data
        Load();
    }

    private void Save()
    {
        //convert the PlayerProgress object to a json string
        string json = JsonUtility.ToJson(playerProgress);

        //save the json string to a file
        File.WriteAllText(Application.persistentDataPath + "/playerProg.json", json);
    }

    private void Load()
    {
        //get the file path
        string path = Application.persistentDataPath + "/playerProg.json";

        //if the file exists, load it
        if(File.Exists(path))
        {
            string json = File.ReadAllText(path);
            playerProgress = JsonUtility.FromJson<PlayerProgress>(json);
        }
        //otherwise, create a new save file
        else
        {
            playerProgress = new PlayerProgress(2);
        }
    }

    public void CompleteLevel(int level, bool collectible)
    {
        //we only mark the level as completed if it hasn't been completed before
        if (!playerProgress.levelsCompleted[level])
        {
            playerProgress.levelsCompleted[level] = true;
        }

        //we also mark the collectible as collected if it hasn't been collected before
        if (!playerProgress.collectibles[level] && collectible)
        {
            playerProgress.collectibles[level] = true;
        }

        //this way, the player can replay levels to collect the collectibles without losing their progress
        //now, we can save the progress
        Save();
    }

    public bool IsLevelCompleted(int level)
    {
        return playerProgress.levelsCompleted[level];
    }

    public bool IsCollectibleCollected(int level)
    {
        return playerProgress.collectibles[level];
    }
}