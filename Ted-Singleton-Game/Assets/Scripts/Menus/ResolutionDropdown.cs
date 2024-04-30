using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResolutionDropdown : MonoBehaviour
{
    public TMP_Dropdown resolutionDropdown;

    // Start is called before the first frame update
    void Start()
    {
        // we need to get the resolutions from the screen to populate the dropdown
        Resolution[] resolutions = Screen.resolutions;
        // clear the dropdown
        resolutionDropdown.ClearOptions();
        // create a list of strings to hold the resolution options
        List<string> options = new List<string>();
        // create a variable to hold the current resolution index
        int currentResolutionIndex = 0;

        // loop through the resolutions
        for (int i = 0; i < resolutions.Length; i++)
        {
            // create a string with the resolution width and height, as well as the refresh rate
            string option = $"{resolutions[i].width} x {resolutions[i].height} @ {resolutions[i].refreshRateRatio}Hz";
            // add the string to the options list
            options.Add(option);

            // check if the resolution is the current resolution
            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                // set the current resolution index to the current resolution
                currentResolutionIndex = i;
            }
        }

        // add the options list to the dropdown
        resolutionDropdown.AddOptions(options);
        // set the value of the dropdown to the current resolution index
        resolutionDropdown.value = currentResolutionIndex;
        // refresh the dropdown
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        Debug.Log(resolutionIndex);
        // get the resolutions from the screen
        Resolution[] resolutions = Screen.resolutions;
        // get the resolution at the index
        Resolution resolution = resolutions[resolutionIndex];
        Debug.Log($"Setting resolution to {resolution.width} x {resolution.height} @ {resolution.refreshRateRatio}Hz");
        // set the screen resolution
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
