using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    //we need the image component of the HUDCentre object to change its color
    private Image hudCentre;
    //we also use the GrappleHook object to check if the player is in range of a grapple point
    private GrappleHook grappleHook;

    private TextMeshProUGUI timerText;
    private Timer timer;

    private TextMeshProUGUI collectibleText;

    private void Start()
    {
        //get the image component of the HUDCentre object
        hudCentre = GameObject.Find("HUDCentre").GetComponent<Image>();
        //and get the GrappleHook object
        grappleHook = FindObjectOfType<GrappleHook>();

        timerText = GameObject.Find("Timer").GetComponent<TextMeshProUGUI>();
        timer = FindObjectOfType<Timer>();

        collectibleText = GameObject.Find("CollectibleText").GetComponent<TextMeshProUGUI>();
        collectibleText.gameObject.SetActive(false);
    }

    private void Update()
    {
        //if the player is in range of a grapple point, change the HUDCentre object's color to green
        //otherwise, we just reset it to white
        hudCentre.color = grappleHook.InGrappleRange() ? Color.green : Color.white;

        //we set the timer text to the current time
        timerText.text = $"Time: {timer.GetElapsedTime().ToString("F2")}s";
    }

    public void ShowCollectibleText()
    {
        //we get the collectible text object and set it to active
        collectibleText.gameObject.SetActive(true);
    }
}
