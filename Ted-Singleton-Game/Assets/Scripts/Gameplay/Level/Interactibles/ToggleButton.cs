using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleButton : MonoBehaviour
{
    public Door door;
    private bool btnEnabled = true;

    private Color originalColor;

    private void Start()
    {
        //we store the original colour of the button
        originalColor = GetComponent<Renderer>().material.color;
    }

    private void Update()
    {
        //if the button is disabled, we set its colour to red
        if (!btnEnabled)
        {
            GetComponent<Renderer>().material.color = Color.red;
        }
        else
        {
            //if the button is enabled, we reset its colour to its original
            GetComponent<Renderer>().material.color = originalColor;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //we start a coroutine to open the door and close it after the duration
            StartCoroutine(ToggleDoor());
        }
    }

    private IEnumerator ToggleDoor()
    {
        //we only want to interact with the door if the button is enabled
        if (btnEnabled)
        {
            if (!door.isOpen)
            {
                //disable the button
                btnEnabled = false;
                Debug.Log("Button disabled");
                //open the door
                StartCoroutine(door.OpenDoorCoroutine());
                Debug.Log("Door opening");
                //wait for the door to open
                yield return new WaitForSeconds(door.openDuration);
                Debug.Log("Door open");
                //enable the button
                btnEnabled = true;
                Debug.Log("Button enabled");
            }
            else
            {
                //disable the button
                btnEnabled = false;
                Debug.Log("Button disabled");
                //open the door
                StartCoroutine(door.CloseDoorCoroutine());
                Debug.Log("Door closing");
                //wait for the door to open
                yield return new WaitForSeconds(door.openDuration);
                Debug.Log("Door closed");
                //enable the button
                btnEnabled = true;
                Debug.Log("Button enabled");
            }
        }
        else
        {
            //if the button is not enabled, do nothing
            yield return null;
        }
    }
}
