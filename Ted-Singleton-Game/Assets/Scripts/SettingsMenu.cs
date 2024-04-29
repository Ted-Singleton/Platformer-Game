using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    // We don't need to do anything in the Start method for this script
    // This is because this menu will only be accessed from another menu

    public void DisplaySettings()
    {
        //for now, just make the game fullscreen
        Screen.fullScreen = true;
        Debug.Log("Settings displayed");
    }

    public void InputSettings()
    {
        //for now, just print a message to the console
        Debug.Log("Input settings not implemented yet");
    }

    public void Back()
    {
        // Go back to the main menu, which is at build index 0
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
