using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    //we need a float to store the sensitivity for both axes
    public float xSensitivity = 100f;
    public float ySensitivity = 100f;

    //create a transform that will store our orientation
    public Transform orientation;

    //we need floats to store our x and y rotations
    public float xRotation;
    public float yRotation;

    private void Start()
    {
        //hide the cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void Update()
    {
        //get the mouse input
        float mouseX = Input.GetAxisRaw("Mouse X") * xSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxisRaw("Mouse Y") * ySensitivity * Time.deltaTime;

        //update our rotations
        //this seems a bit weird, but it's because we want to rotate the player's body on the y axis, and the camera on the x axis
        yRotation += mouseX;
        xRotation -= mouseY;

        //we clamp the x rotation to prevent the player from looking too far up or down
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        //we rotate the camera on both axes
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0f);
        //and the player's body on the y axis
        orientation.rotation = Quaternion.Euler(0f, yRotation, 0f);
    }
}
