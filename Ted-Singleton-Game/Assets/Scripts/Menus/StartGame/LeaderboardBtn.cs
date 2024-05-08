using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LeaderboardBtn : MonoBehaviour
{
    public void LoadLeaderboard(int levelID)
    {
        //we use PlayerPrefs to pass the level ID to the leaderboard scene
        PlayerPrefs.SetInt("LevelID", levelID);

        //load the leaderboard scene
       SceneManager.LoadScene("LevelLeaderboard");
    }
}
