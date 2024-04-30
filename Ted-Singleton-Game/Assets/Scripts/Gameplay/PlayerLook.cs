using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLook : MonoBehaviour
{
    //we need the PlayerInput class to get our mouse input from the look action
    public PlayerInput inputActions;
    private InputAction look;

    //we need a float to store the sensitivity for both axes
    public float xSensitivity = 25f;
    public float ySensitivity = 25f;

    //create a transform that will store our orientation
    public Transform orientation;

    //we need floats to store our x and y rotations, as well as our input values
    public float xRotation;
    public float yRotation;
    private float mouseX;
    private float mouseY;

    private void Start()
    {
        //first, we need the PlayerInput object to get our look action
        inputActions = new PlayerInput();
        inputActions.Enable();
        look = inputActions.OnFoot.Look;

        //we subscribe to the look event to handle the player's input
        look.performed += ctx => HandleLook(look.ReadValue<Vector2>());

        //hide the cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Since we're subscribed to the look event, we don't need an Update method to check for input
    // Instead, we handle the input in the HandleLook method
    private void HandleLook(Vector2 lookValues)
    {
        //get the mouse input
        mouseX = lookValues.x * xSensitivity * Time.deltaTime;
        mouseY = lookValues.y * ySensitivity * Time.deltaTime;

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
