using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerUtils;
using TMPro;
using UnityEngine.UI;

public class Leaderboard : MonoBehaviour
{
    //we'll need the levelID to get the level name from the level manager
    private int levelID;
    private string levelName;

    //we need the level manager to get the level ID, and the database manager to get the player times
    private LevelManager levelManager;
    private DatabaseManager databaseManager;

    //we need the textmeshpro gameobject that contains the level name text
    public TextMeshProUGUI levelNameText;

    //we create a list of player times to store the top 100 times for the level
    private List<PlayerTime> playerTimes;

    //we need the prefab for the leaderboard entry, and the parent transform to instantiate the entries under
    //a prefab is a template for a game object, which can be used to create multiple instances of the same object
    //in this case, we have created a prefab for eaderboard entries, which we will instantiate for each player time
    public GameObject entryPrefab;
    public Transform entryParent;

    private void Start()
    {
        //first, we need to get the level ID from the PlayerPrefs
        levelID = PlayerPrefs.GetInt("LevelID");

        //we need the level manager to get the level ID
        levelManager = FindObjectOfType<LevelManager>();
        levelName = levelManager.GetLevelName(levelID);

        //we ant to set the level name text to the current level name
        levelNameText = GameObject.Find("LevelName").GetComponent<TextMeshProUGUI>();
        levelNameText.text = levelName;

        //we need the database manager to get the player times
        databaseManager = FindObjectOfType<DatabaseManager>();

        //we need to populate the leaderboard with the top 100 times for the level
        PopulateLeaderboard();
    }

    private void PopulateLeaderboard()
    {
        //first, we should clear any existing entries, in case this is a reload
        //this isn't explicitly necessary, but it's good practice to clean up before populating, since we may want to use placeholders in the editor
        foreach (Transform child in entryParent)
        {
            Destroy(child.gameObject);
        }

        //get the top 100 player times for the level, and store them in the playerTimes list
        playerTimes = databaseManager.GetTop100(levelName);

        //iterate through the playerTimes, keeping track of the position
        int position = 1;
        foreach (PlayerTime playerTime in playerTimes)
        {
            //instantiate a new entry prefab, and set the text fields to the player's position, name, and time
            GameObject newObj = Instantiate(entryPrefab, entryParent);

            //get the text components of the new entry
            TextMeshProUGUI[] texts = newObj.GetComponentsInChildren<TextMeshProUGUI>();

            //we make sure that the array is not null and has the correct number of elements before setting the text
            if (texts != null && texts.Length == 3)
            {
                //set the text fields to the player's position, name, and time
                texts[0].text = position.ToString();
                texts[1].text = playerTime.playerName;
                texts[2].text = playerTime.levelTime.ToString("F2") + "s";

                //increment the position for the next entry
                position++;
            }
        }
        
        //once we're done populating the leaderboard, we need to resize the scroll view content to fit all the entries
        ResizeScrollView();
    }

    private void ResizeScrollView()
    {
        //first, we need to get the scroll view content and its RectTransform
        //the scroll view content is the parent of all the leaderboard entries
        GameObject scrollViewContent = GameObject.Find("Content");

        //the rect transform is used to set the size of the content, which determines the scrollable area
        RectTransform contentTransform = scrollViewContent.GetComponent<RectTransform>();

        //we get the height of each entry using details from the prefab
        float heightPerEntry = entryPrefab.transform.GetComponent<RectTransform>().sizeDelta.y;

        //and set the size of the content to fit all the entries
        contentTransform.sizeDelta = new Vector2(contentTransform.sizeDelta.x, heightPerEntry * playerTimes.Count);
    }
}
