using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    // We don't need to do anything in the Start method for this script
    // This is because this menu will only be accessed from another menu

    public void FullscreenToggle()
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
        // Load the input settings menu
        SceneManager.LoadScene("InputMenu");
    }

    public void Back()
    {
        // Go back to the main menu
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}
