using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    [Header("Movement")]
    private float moveSpeed;
    public float walkSpeed;
    public float sprintSpeed;


    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;


    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;


    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    public Transform orientation;

    float horizontalInput;
    float veritcalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        readyToJump = true;
    }

    void Update()
    {
        MyInput();
        stateHandler();
    }

    private void FixedUpdate()
    {
        movePlayer();
        playerGrounded();
        speedControll();
        stateHandler();
    }

    public movementState state;
    public enum movementState{
        walking,
        sprinting,
        air
    }

    private void stateHandler()
    {

        //Sprinting, on ground and sprint key is pressed
        if(grounded && Input.GetKey(sprintKey))
        {
            state = movementState.sprinting;
            moveSpeed = sprintSpeed;


        }
        //Walking, on ground and sprinitKey is not pressed
        else if(grounded)
        {
            state = movementState.walking;
            moveSpeed = walkSpeed;
        }

        // In air
        else
        {
            state = movementState.air;


        }


    }


    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        veritcalInput = Input.GetAxisRaw("Vertical");

        //jump if space is pressed
        if (Input.GetKey(jumpKey) && readyToJump && grounded) {

            readyToJump = false;
            jump();

            //can hold in space to continually jump 
            Invoke(nameof(jumpReset), jumpCooldown);

        }


    }

    private void movePlayer()
    {

        moveDirection = orientation.forward * veritcalInput + orientation.right * horizontalInput;
        //normal movementspeed when on ground
        if(grounded)
        rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

        //applies airMultiplier to the players movementspeed when in air, gives small speedboost depending in value of airMultiplier.
        else if(!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
    }



    private void playerGrounded() {

        //cast raycast to check if the player is touching the ground, applies a drag to the player that emulates friction
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
        if (grounded)
        {
            rb.drag = groundDrag;


        }
        else
        {
            rb.drag = 0f;
        }
    }


    private void speedControll()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        //used to limit the velocity if it exceeds set moveSpeed
        if (flatVel.magnitude > moveSpeed)
        {

            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);

        }
    }


    private void jump() {

        //resets y velocity, makes sure that all jumps are of equal height
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);


        //the jumping code
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    
    }

    private void jumpReset() {

        readyToJump = true;
    }



}
