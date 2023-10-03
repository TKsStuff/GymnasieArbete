using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterMovement : MonoBehaviour
{

    public float movementSpeed;
    public Animator camAnim;


    private bool isWalking;
    public CharacterController myCC;
    private Vector3 inputVector;
    private Vector3 movementVector;
    public float myGravity = -10f;
    public float jumpHeight = 15f;
    public float _yVelocity;
    public float jumpGrav = 1f;
    public float sprintSpeed;
    public float walkSpeed;
    private float movementDamping = 2f;







    void Start()
    {
        myCC = GetComponent<CharacterController>();
    }

    
    void Update()
    {
        getInput();
        movePlayer();
      
       // playerJump(); st�ng av tills fixad, fuckar upp movement.
        camAnim.SetBool("isWalking", isWalking);
    }

    void getInput() {
            


        if(Input.GetKey(KeyCode.W)|| Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            //r�relse andringar om wasd nere ge -1,0.,1 i vector �ndring
            inputVector = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
            // g�r s� att den f�ljer vart man tittar
            inputVector.Normalize();
            inputVector = transform.TransformDirection(inputVector);
            isWalking = true;

        }
        else
        {
            inputVector = Vector3.MoveTowards(inputVector, Vector3.zero, movementDamping * Time.deltaTime);
            isWalking = false;
        }


       

        


        //sj�lva r�relsen movementVector har all r�relse.
        movementVector = (inputVector * movementSpeed) + (Vector3.up * myGravity);


    }

    void movePlayer() {
        //G�r s� att CharacterControllen anv�ndersig av movementVector.
        myCC.Move(movementVector * Time.deltaTime); 


    }

    

    

    void playerJump() 
    {


        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput, 0, verticalInput);
        Vector3 velocity = direction * movementSpeed;
       

        if (myCC.isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _yVelocity = jumpHeight;
            }
        }
        else
        {
            _yVelocity -= jumpGrav;
        }

        velocity.y = _yVelocity;
        myCC.Move(velocity * Time.deltaTime);
     

    }

    
    

    void playerDash() //maybe air dash only or both.
    {


    }
}

