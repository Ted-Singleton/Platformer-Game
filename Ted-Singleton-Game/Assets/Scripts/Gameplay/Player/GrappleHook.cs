using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GrappleHook : MonoBehaviour
{
    //We need the PlayerInput object to get the grapple key
    public PlayerInput inputActions;
    private InputAction fireGrapple;

    //The line renderer is used to draw the grapple hook line
    public LineRenderer lr;
    //The camera and player transforms are used to determine the direction of the grapple hook
    public Transform cam, player, grapplePlayer;
    //The grapple hook can only attach to objects in the grapplableObjects layer
    public LayerMask grapplableObjects;

    //The grapple range is the maximum distance the grapple hook can reach
    public float grappleRange = 30f;
    //The swing point is the point where the grapple hook is attached
    private Vector3 swingPoint;

    //Spring joint is used to simulate the grapple hook swing
    //It is a physics joint that allows two objects to be connected by a spring
    private SpringJoint joint;

    //we need a bool to use as a flag for if this script has been destroyed
    private bool isDestroyed = false;

    //we 'll use a raycast to determine whether the grapple hook is in range
    RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        // We need to get and enable the PlayerInput object to get the grapple key
        inputActions = new PlayerInput();
        inputActions.Enable();
        // Get the FireGrapple action from the PlayerInput object
        fireGrapple = inputActions.OnFoot.FireGrapple;

        // Subscribe to the FireGrapple action's performed and canceled events
        // This allows us to call the StartSwing and EndSwing methods when the grapple key is pressed and released
        fireGrapple.performed += ctx => StartSwing();
        fireGrapple.canceled += ctx => EndSwing();
    }

    // Update is called once per frame
    void Update()
    {
        //Since we're subscribing to the FireGrapple action's events, we don't need to check for input here
    }

    private void LateUpdate()
    {
        if(isDestroyed) return;
        //if the player is swinging, draw the grapple hook line
        if (joint)
        {
            drawLine();
        }
    }

    // Start the grapple hook swing
    private void StartSwing()
    {
        if (isDestroyed) return;

        if(InGrappleRange())
        {
            //update the player's isSwinging bool to true
            player.GetComponent<PlayerMovement>().isSwinging = true;
            Debug.Log(player.GetComponent<PlayerMovement>().isSwinging);

            Debug.Log("hit something");
            //set the swing point to the point where the raycast hit, and create a spring joing
            swingPoint = hit.point;
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = swingPoint;

            float distance = Vector3.Distance(player.position, swingPoint);
            Debug.Log(distance);

            //we want to limit the maximum and minimum distance of the spring joint
            //this will ensure the grapple hook doesn't stretch too far or too close
            joint.maxDistance = distance * 0.5f;
            joint.minDistance = distance * 0.25f;

            //set the spring joint's spring and damper values
            //the spring value determines the grappling hook's strength
            joint.spring = 7.5f;
            joint.damper = 7f;
            joint.massScale = 4.5f;

            //use the line renderer to draw the grapple hook line
            lr.positionCount = 2;
            //and make the line visible
            lr.enabled = true;
        }
    }

    // Handle the end of the grapple hook swing
    private void EndSwing()
    {
        if (isDestroyed) return;

        //remove the spring joint and reset the line renderer
        lr.positionCount = 0;
        Destroy(joint);

        //update the player's isSwinging bool to false
        player.GetComponent<PlayerMovement>().isSwinging = false;
    }

    void drawLine()
    {
        if (isDestroyed) return;
        //if the line renderer has not been initialized, return
        if (!joint) return;
        //draw a line from the player to the swing point
        lr.SetPosition(0, grapplePlayer.position);
        lr.SetPosition(1, swingPoint);
    }

    private void OnDestroy()
    {
        //set the isDestroyed bool to true when the script is destroyed
        isDestroyed = true;
        //unsubscribe from the FireGrapple action's events
        fireGrapple.performed -= ctx => StartSwing();
        fireGrapple.canceled -= ctx => EndSwing();
    }

    //we use a method to check if the player is in range of a grapple point, so that we can use it in the HUD controller
    public bool InGrappleRange()
    {
        return Physics.Raycast(cam.position, cam.forward, out hit, grappleRange, grapplableObjects);
    }
}
