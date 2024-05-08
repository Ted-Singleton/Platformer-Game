using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour
{
    //these values are public, so they can be changed in the inspector
    //the values here are the defaults
    //we need the direction the door should open in, the speed at which it should open and how long it should stay open
    public Vector3 openDirection = new Vector3(0, 1, 0); 
    public float openSpeed = 2f;
    public float openDuration = 3f;

    //we also need to know the initial position of the door and the target position
    private Vector3 initialPosition;
    private Vector3 targetPosition;

    //as well as a bool to check if the door is open or not
    public bool isOpen { get; private set; } = false;

    private void Start()
    {
        //set the initial position and the target position
        initialPosition = transform.position;
        //the target position is the initial position plus the open direction
        targetPosition = initialPosition + openDirection;
    }

    public IEnumerator OpenDoorCoroutine()
    {
        if(!isOpen)
        {
            isOpen = true;
            //we need to keep track of how long the door has been open
            float elapsedTime = 0f;
            //while the door has not been open for the full duration
            while (elapsedTime < openDuration)
            {
                //move the door towards the target position
                transform.position = Vector3.Lerp(initialPosition, targetPosition, elapsedTime / openSpeed);
                //increment the elapsed time
                elapsedTime += Time.deltaTime;
                //wait for the next frame
                yield return null;
            }
        }
        else
        {
            yield return null;
        }
    }

    public IEnumerator CloseDoorCoroutine()
    {
        if (isOpen)
        {
            isOpen = false;

            //again, the process here is similar to the OpenDoorCoroutine, we just flip the positions
            float elapsedTime = 0f;
            while (elapsedTime < openDuration)
            {
                transform.position = Vector3.Lerp(targetPosition, initialPosition, elapsedTime / openSpeed);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }
        else
        {
            yield return null;
        }
    }

    public IEnumerator ToggleDoor()
    {
        //we can reuse the OpenDoorCoroutine and CloseDoorCoroutine methods to toggle the door
        //if the door is open, close it, if it is closed, open it
        if (isOpen)
        {
            yield return StartCoroutine(CloseDoorCoroutine());
        }
        else
        {
            yield return StartCoroutine(OpenDoorCoroutine());
        }
    }
}
