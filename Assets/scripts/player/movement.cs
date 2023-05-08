using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    [Header("movement")] 
    private float moveSped;
    public float walkSped;
    public float sprintSped;
    public float stamina;
    public float maxStamina;
    public float stamRegRate;
    
    [Header("Input related")]
    public Transform orientation;
    private float horiInput;
    private float vertInput;

    private Vector3 moveDirection;

    private Rigidbody rb;

    public float groundDrag;

    [Header("Jumping related")]
    public float jumpForce; 
    public float jumpCD;
    public float airMulti;
    private bool readyToJump;
    private bool slopExit;
    
    [Header("Ground check")] 
    public float playerHei;
    public LayerMask whatIsGround;
    private bool grounded;

    [Header("Crouching")] 
    public float crouchSped;
    public float crouchYScale; // size of player height
    public float startYScale; // starting height before shrinking

    [Header("Slop Detection")] 
    public float maxSlopeAngle;
    private RaycastHit slopeHit; //detects whether slope has been interacted with


    [Header("Keybinds")]
    public List<bindingKeys> keyBinds = new List<bindingKeys>();
    // public KeyCode jumpKey = KeyCode.Space;
    
    //advance movement related
    public moveState currentState;
    public enum moveState
    {
        walking,
        sprinting,
        crouching,
        air
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        readyToJump = true;
        
        stamina = maxStamina; //setting stam for UI 
        startYScale = transform.localScale.y;
    }

    private void Start()
    {
        moveDirection.y = 0;
      
        //necessary references 


        // playerHei = gameObject.transform.localScale.y;
    }

    private void Update()
    {
        //checking to see if player is grounded
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHei * 0.5f + 0.2F, whatIsGround);
        
        //manipulates drag depending on if grounded or not
        if (grounded)
        {
            rb.drag = groundDrag;
        }
        else rb.drag = 0;
        
        spedCon();
        myInput();
        stateHandler();
        
        // Debug.Log(moveSped);
    }

    private void FixedUpdate()
    {
        movePlayer();
    }

    void myInput()
    {
        //setting the inputs to use unity old input system 
        horiInput = Input.GetAxisRaw("Horizontal");
        vertInput = Input.GetAxisRaw("Vertical");
        
        // Debug.Log(readyToJump);
        // Debug.Log(grounded);
        
        if (Input.GetKey(keyBinds[0].keyCode) && readyToJump && grounded)
        {
            readyToJump = false;
            
            jump();
            
            Invoke(nameof(jumpReady), jumpCD); //reset jump so that player can continuously jump
        }

        if (Input.GetKeyDown(keyBinds[2].keyCode))
        {
            transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.z);
            rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
        }

        if (Input.GetKeyUp(keyBinds[2].keyCode))
        {
            transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);
        }
    }

    void stateHandler()
    {
        //crouching
        if (Input.GetKey(keyBinds[2].keyCode))
        {
            currentState = moveState.crouching;
            moveSped = crouchSped;
        }
        
        //sprinting
        else if (grounded && Input.GetKey(keyBinds[1].keyCode) && stamina > 0)
        {
            currentState = moveState.sprinting;
            moveSped = sprintSped;
            stamina -= 0.5f;
        }
        
        //walking
        else if (grounded)
        {
            currentState = moveState.walking;
            moveSped = walkSped;
        }

        //within air
        else
        {
            currentState = moveState.air;
        }
        
        if (!Input.GetKey(keyBinds[1].keyCode) && stamina < maxStamina)
        {
            StartCoroutine(RegenerateStamina());
        }
        
    }
    
    private IEnumerator RegenerateStamina()
    {
        yield return new WaitForSeconds(2f); //slight delay before activating stamina regen
        while (stamina < maxStamina)
        {
            stamina += stamRegRate;
            yield return new WaitForSeconds(1f); // wait for 1 second
        }
    }

    void movePlayer()
    { 
        moveDirection = orientation.forward * vertInput + orientation.right * horiInput; //calculating the orientation for the player for the force   

        if (Onslope() && !slopExit)
        {
            rb.AddForce(GetSlopeMoveDirection() * moveSped * 20f, ForceMode.Force);
            
            if(rb.velocity.y > 0) rb.AddForce(Vector3.down * 80f, ForceMode.Force);
            
        }
        
        if(grounded) rb.AddForce(moveDirection.normalized * moveSped * 10f * Time.deltaTime, ForceMode.Impulse);
        else if(!grounded) rb.AddForce(moveDirection.normalized * moveSped * 10f * airMulti * Time.deltaTime, ForceMode.Impulse);
        
        //turn off gravity if on slope
        rb.useGravity = !Onslope();
    }
    
    //to ensure the player remains at the set speed 
    void spedCon()
    {   
        //limiting speed on slope
        if (Onslope() && !slopExit)
        {
            if (rb.velocity.magnitude > moveSped)
                rb.velocity = rb.velocity.normalized * moveSped;
        }

        //limiting speed on ground
        else
        {
            Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        
            //limit velo
            if (flatVel.magnitude > moveSped)
            {
                Vector3 limitedVel = flatVel.normalized * moveSped;
                rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
            }    
        }
        
        
    }
    
    //the ability for player to jump
    void jump()
    {

        slopExit = true;
        
        //reseting the y velo 
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    void jumpReady()
    {
        readyToJump = true;
        
        slopExit = false;
        
    }

    bool Onslope() //returns either true or false
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHei * 0.5f + 0.3f))
        {
            //calculation of the angle 
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }
        
        //if there is no slope
        return false;
    }

    Vector3 GetSlopeMoveDirection()
    {
        return Vector3.ProjectOnPlane(moveDirection, slopeHit.normal).normalized;
    }
    
}
//making it serializable so that it can be edited within the inspector 
[System.Serializable]
public class bindingKeys
{
    public string action;
    public KeyCode keyCode;
}
