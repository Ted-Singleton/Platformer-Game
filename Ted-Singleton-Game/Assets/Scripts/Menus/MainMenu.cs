using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Unlock the cursor when we return to the main menu
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void StartGame()
    {
        // Load the next scene using its name
        UnityEngine.SceneManagement.SceneManager.LoadScene("StartGame");
    }

    public void Settings()
    {
        // Load the settings menu using its name
        UnityEngine.SceneManagement.SceneManager.LoadScene("SettingsMenu");
    }

    public void Quit()
    {
        // Quit the game - this will only work in a built version of the game
        Application.Quit();

        // Stop the game in the editor - this will primarily be used for testing
        // This also must be commented out before building the game
        //UnityEditor.EditorApplication.isPlaying = false;
    }
}
