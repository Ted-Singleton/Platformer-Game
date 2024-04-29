using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //GENERAL MOVEMENT:
    //create a transform that will store our orientation
    public Transform orientation;
    //we need floats to store our x and y inputs
    float xInput;
    float yInput;
    //we need a vector3 to store our direction
    Vector3 direction;
    //we need a rigidbody to apply forces to
    Rigidbody playerBody;

    //GROUNDED:    
    /*While the player is moving on the ground, we need a float to determine their speed limit, a drag value to stop them from accelerating infinitely,
     the player's height to manage collisions with the top of the character, a layermask determining which objects count as ground, and a bool stating if they are touching the ground*/
    public float groundSpeed;
    public float drag;

    public float playerHeight;

    public LayerMask Ground;
    bool isGrounded;

    //AIRBORNE:
    /*To enable airborne movement, we need a float to hold their speed limit while in the air, as well as a multiplier to change how much control they have in the air
     We also need a bool that holds whether the player is currently allowed to jump, a key to mark as the jump key, and an upward force to jump with*/
    public float airSpeed;
    public float airMultiplier;

    public bool canJump;
    public KeyCode jumpKey = KeyCode.Space;
    public float jumpForce;

    //SWINGING:
    /*We need a bool to hold whether the player is currently swinging, and a float to hold their speed limit while swinging*/
    public bool isSwinging;
    public float swingSpeed;

    private void Start()
    {
        //we get the player's rigidbody
        //a rigidbody is a component that allows us to apply forces to an object
        //this allows us to move the player around using physics
        playerBody = GetComponent<Rigidbody>();
        //we freeze the player's rotation so they don't fall over
        playerBody.freezeRotation = true;

        //we also allow the player to jump by default
        canJump = true;

        isSwinging = false;
    }

    private void Update()
    {
        //we check if the player is grounded
        //this is done by casting a ray directly down from the middle of the player's body, and checking if it hits anything on the ground layer
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight / 2 + 0.2f, Ground);
        //if the player is grounded, we reset any jumps they can do
        if (isGrounded)
        {
            TouchedGround();
        }

        //we handle the player's input
        HandleInput();

        //we limit the player's speed, and apply drag if they are grounded
        if (isGrounded)
        {
            SpeedLimit(groundSpeed);
            playerBody.drag = drag;
        }
        else if (isSwinging)
        {
            SpeedLimit(swingSpeed);
            playerBody.drag = 0;
        }
        //if they're airborne, we apply a different speed limit and no drag
        else
        {
            SpeedLimit(airSpeed);
            playerBody.drag = 0;
        }
    }

    private void FixedUpdate()
    {
        //we update the player's movement
        Move();
    }

    private void HandleInput()
    {
        //we get the player's input
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");

        if(Input.GetKeyDown(jumpKey) && isGrounded)
        {
            canJump = false;

            Jump();
        }
    }

    private void Move()
    {
        //if the player is swinging, we let physics handle the movement
        if (isSwinging) return;

        //we calculate the direction the player should move in
        direction = orientation.forward* yInput + orientation.right * xInput;

        //we check if the player is grounded
        if (isGrounded)
        {
            //and apply a force in that direction to accelerate the player
            playerBody.AddForce(direction.normalized * 10*groundSpeed, ForceMode.Force);
        }
        else
        {
            //if the player is airborne, we let the player apply a greater force to promote moving in the air
            playerBody.AddForce(direction.normalized * airSpeed*airMultiplier, ForceMode.Force);
        }
    }

    private void SpeedLimit(float modeLimit)
    {
        //we want to limit the player's horizontal speed, so we check if it's greater than the max speed

        //first, we make a new vector3 that ignores the y axis
        Vector3 xzVelocity = new Vector3(playerBody.velocity.x, 0, playerBody.velocity.z);

        //then, if the magnitude of that vector is greater than the max speed, we set the player's velocity to the max speed
        if (xzVelocity.magnitude > modeLimit)
        {
            //we do this by creating a new vector3 that is the normalized velocity multiplied by the max speed
            Vector3 cappedVelocity = xzVelocity.normalized * modeLimit;
            //and we apply that to the player's velocity
            playerBody.velocity = new Vector3(cappedVelocity.x, playerBody.velocity.y, cappedVelocity.z);
        }
    }

    private void Jump()
    {
        //we reset the player's y velocity so that jumps are consistent
        playerBody.velocity = new Vector3(playerBody.velocity.x, 0, playerBody.velocity.z);

        //we apply a force to the player's y axis
        //we use an impulse force mode so that the player's velocity is instantly set to the jump force
        playerBody.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void TouchedGround()
    {
        canJump = true;
    }
}
