using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputMenu : MonoBehaviour
{
    public void Back()
    {
        // Go back to the settings menu
        UnityEngine.SceneManagement.SceneManager.LoadScene("SettingsMenu");
    }
}
