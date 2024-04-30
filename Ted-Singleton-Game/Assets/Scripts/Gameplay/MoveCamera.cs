using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    //directly attaching a camera to the player can be buggy, so we've created a separate object to hold the camera
    //this script will move the camera to the player's position and orientation
    public Transform cameraPos;

    // Update is called once per frame
    void Update()
    {
        //we simply apply the linked transform's position to the camera's position
        transform.position = cameraPos.position;
    }
}
