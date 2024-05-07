using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectBack : MonoBehaviour
{
    public void Back()
    {
        //load the start game menu scene
        SceneManager.LoadScene("StartGame");
    }
}
