using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    public void LevelComplete()
    {
        LevelData.levelName = SceneManager.GetActiveScene().name;
        LevelData.time = Timer.elapsedTime;
        SceneManager.LoadScene("LevelComplete");
    }
}
