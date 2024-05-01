using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameMenu : MonoBehaviour
{
    public void Back()
    {
        //load the main menu scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

    public void NewGame()
    {
        //load the first level
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level1-1");
    }

    public void LevelSelect()
    {
        //load the level select scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("LevelSelect");
    }
}
