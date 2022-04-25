using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;
    [SerializeField] Collider2D standingCollider, crouchingCollider;
    [SerializeField] Transform groundCheckCollider;
    [SerializeField] Transform overheadCheckCollider;
    [SerializeField] LayerMask groundLayer;

    const float groundCheckRadius = 0.2f;
    const float overheadCheckRadius = 0.2f;
    [SerializeField] float speed = 1;
    [SerializeField] float jumpPower = 500;
    [SerializeField] int totalJumps;
    int availableJumps;
    float horizontalValue;
    float runSpeedModifier = 2f;
    float crouchSpeedModifier = 0.5f;

    bool isGrounded;
    bool isRunning;
    bool facingRight = true;
    bool crouchPressed;
    bool multipleJump;
    bool isDead = false;

    

    void Awake()
    {
        
        availableJumps = totalJumps;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        

    }

    
    void Update()
    {
        if (isDead)
            return;

        
        //store the horizontal value
        horizontalValue = Input.GetAxisRaw("Horizontal"); 

        // if Lshift is clicked enable isRunnng
        if(Input.GetKeyDown(KeyCode.LeftShift))
           isRunning = true;
        //if LShift is released disable isRunning
        if(Input.GetKeyUp(KeyCode.LeftShift))
           isRunning = false;

        //if we press Jump button, enable Jump (character jumps)

        if(Input.GetButtonDown("Jump"))
           Jump();  
    
          

        //if we press crouchPressed button, enable crouchPressed (character crouchPressed)
        if (Input.GetButtonDown("Crouch"))
            crouchPressed = true;
        //Otherwise disable it
        else if(Input.GetButtonUp("Crouch"))
            crouchPressed = false;

        //Set the yVelocity in the animator
       
        animator.SetFloat("yVelocity", rb.velocity.y);

       
        
        
    }
    
    void FixedUpdate()
    {
        GroundCheck();
        
        Move(horizontalValue, crouchPressed);

    }

    

    void GroundCheck()
    {
        bool wasGrounded = isGrounded;
        isGrounded = false;
        //check if the GroundCheckObject is colliding with other
        //2D cp;;oders that are in the "Ground" Layer
        //if yes (isGrounded true) else(isGrounded false)
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckCollider.position, groundCheckRadius, groundLayer);
        if(colliders.Length > 0)
        {
            isGrounded = true;
            if(!wasGrounded)
            {
                availableJumps = totalJumps;
                multipleJump = false;

                AudioManager.instance.PlaySFX("landing");

            }
                
        }
        
        
            
        
            


        //As long as we are grounded the "Jump" bool
        //in the animator is disabled
        animator.SetBool("Jump", !isGrounded);
        
    }

    void Jump()
    {
        if(isGrounded)
        {
           multipleJump = true;
           availableJumps--;
           rb.velocity = Vector2.up * jumpPower;
           animator.SetBool("Jump", true);

        }
        else
        {
            if(multipleJump && availableJumps > 0)
            {
                availableJumps--;
                rb.velocity = Vector2.up * jumpPower;
                animator.SetBool("Jump", true);
            }
        }
    }

    void Move(float dir,bool crouchFlag)
    {
        #region Crouch

        if(!crouchFlag)
        {
            if(Physics2D.OverlapCircle(overheadCheckCollider.position,overheadCheckRadius,groundLayer))
                crouchFlag = true;                            
            
        }

        //if we press Crouch we disable the standing collider + animate the crouchPressed
        //reduce the speed 
        //if released resume the original spped and enable the standing collider + disable crouchPressed animation
       

        animator.SetBool("Crouch", crouchFlag);
        standingCollider.enabled = !crouchFlag;
        crouchingCollider.enabled = crouchFlag;

        #endregion

        #region Move & Run
        //set value of x using dir and speed
        float xVal = dir * speed * 100 * Time.fixedDeltaTime;
        //if we are running multiply with the running modifier
        if(isRunning)
            xVal *= runSpeedModifier;

        //if we are running multiply with the running modifier
        if(crouchFlag)
            xVal *= crouchSpeedModifier;

        //create vec2 for the velocity
        Vector2 targetVelocity = new Vector2(xVal,rb.velocity.y);
        //set the player velocity
        rb.velocity = targetVelocity;

        

        //if looking right and clicked left (flip to left)
        if(facingRight && dir < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            facingRight = false;

        }

        else if(!facingRight && dir > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            facingRight = true;
        }
        
        // 0 idle, 14 walking, 28, running
        //Set the float xvelocity according to the x value 
        //of the RigidBody2D velocity
        animator.SetFloat("xVelocity", Mathf.Abs(rb.velocity.x));
        #endregion
    }

    public void Die()
    {
        isDead = true;
        FindObjectOfType<LevelManager>().Restart();
    }

    public void ResetPlayer()
    {
        
        isDead = false;
    }

    
}
