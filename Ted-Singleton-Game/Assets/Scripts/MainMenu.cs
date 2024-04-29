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
    }

    public void StartGame()
    {
        // Load the next scene using the build index
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
        // Lock the cursor when we start the game
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Settings()
    {
        // Load the settings menu using the build index
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        // Quit the game - this will only work in a built version of the game
        Application.Quit();

        // Stop the game in the editor - this will primarily be used for testing
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
