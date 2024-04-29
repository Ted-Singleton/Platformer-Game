using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    // We don't need to do anything in the Start method for this script
    // This is because this menu will only be accessed from another menu

    public void DisplaySettings()
    {
        // Toggle fullscreen mode
        if(Screen.fullScreen)
        {
            Screen.fullScreen = false;
        }
        else
        {
            Screen.fullScreen = true;
        }

        // This does nothing in the editor, so we'll output a message to the console
        Debug.Log("Fullscreen toggled");
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
