using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LeaderboardBack : MonoBehaviour
{
    public void Back()
    {
        SceneManager.LoadScene("LevelSelect");
    }
}
