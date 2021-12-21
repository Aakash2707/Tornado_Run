using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//This code decides movent of the pacman


public class PacMovement : MonoBehaviour
{   
    //variables for movements and looks
    public float moveSpeed, boostSpeed, rotationSpeed, lookz2, lookx2, lookz, lookx;
    public Vector3 velocity;
    public Vector3 moveDirection, LookDirection;

    //variable to make a gravity act on it so that it doesn't slip out.
    public bool isGrounded;
    public float gravity, groundCheckDistance;
    public LayerMask groundMask;

    //Puts in character controller and assigns button audios.
    public CharacterController controller;
    public AudioSource Buttonclicks;



    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        moveDirection = new Vector3(0,0,1);
    }

    // Update is called once per frame
    void Update()
    {   
        isGrounded = Physics.CheckSphere(transform.position,groundCheckDistance,groundMask);

        if(isGrounded)
        {
            velocity.y = -2f;
        }

        
        // updates for look directions

        lookx = Input.GetAxis("Horizontal");
        lookz = Input.GetAxis("Vertical");
        LookChecker();

        //sets up look direction of the player.
        LookDirection = new Vector3(lookx2 + 0.00001f , 0, lookz2 + 0.00001f);
        LookDirection.Normalize();

        //sets up movement direction to the look direction on update.
        moveDirection = LookDirection;
        moveDirection.Normalize();

        //code to rotate the player object on the input direction.
        Quaternion toRotate = Quaternion.LookRotation(LookDirection, Vector3.up);
        this.gameObject.transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotate, rotationSpeed * Time.deltaTime);


        //assigns speed to the player
         if (!Input.GetKey(KeyCode.LeftShift))
                {
                    moveSpeed = 9;
                }
        else if (Input.GetKey(KeyCode.LeftShift)) // extra set of code if adds a boost feature in the game.
                {
                    moveSpeed = boostSpeed;
                }
        

        // adss a multiplier to move direction and moves the controller.
        moveDirection *= moveSpeed;
        controller.Move(moveDirection * Time.deltaTime);
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        
        
        
        
    }


    //assigns the look direction
    void LookChecker()

    {
        if (lookz > 0)
        {
            lookz2 = 1;
            lookx2 = 0;
            
        }
        else if (lookz < 0)
        {
            lookz2 = -1;
            lookx2 = 0;
            
        }

        if (lookx > 0)
        {
            lookx2 = 1;
            lookz2 = 0;
            
        }
        else if (lookx < 0)
        {
            lookx2 = -1;
            lookz2 = 0;
            
        }
    }
}
