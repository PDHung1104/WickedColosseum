using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//This script can be utilized for any playable characters

public class MoveCharacter : MonoBehaviour
{
    #region Fields
    [SerializeField]
    GameObject GFX;

    const int PLAYER1 = 7;
    [SerializeField]
    float jump, speed = 90f;

    Rigidbody2D rb;

    Vector2 move;

    Animator anim;

    SpriteRenderer sr;

    Health health;

    bool moving, canJump;

    [SerializeField]
    float radius = 1f;

    float jumpForce;

    [SerializeField]
    Transform groundCheck;

    [SerializeField]
    LayerMask ground;

    [SerializeField]
    float fallMultiplier = 1f;
    
    Vector2 vecGravity;

    bool isGround;
    #endregion

    #region Methods

    void Start()
    {
        vecGravity = new Vector2(0, -Physics2D.gravity.y);
        rb = gameObject.GetComponent<Rigidbody2D>();
        anim = GFX.GetComponent<Animator>();
        sr = GFX.GetComponent<SpriteRenderer>();
        moving = false;
        health = gameObject.GetComponent<Health>();
        jumpForce = transform.position.y;
        isGround = true;
    }
    void FixedUpdate()
    {
        //move the character around
        GroundCheck();
        //mirror-flip the character if moving to the left of the screen
        //only for player 1 (the one on the left)
        if (!health.Dead)
        {
            jumpForce = jump;
            if (gameObject.tag == "Player1" || gameObject.tag == "Player")
            {
                if (Input.GetKey("w") && isGround)
                {
                    rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                    anim.SetBool("Jump", true);
                    
                }
            }
            else
            {
                if (Input.GetKey("up") && isGround)
                {
                    rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                    anim.SetBool("Jump", true);
                }
            }
            rb.velocity = new Vector2(move.x * speed * Time.deltaTime, rb.velocity.y);
            if (rb.velocity.y < 0)
            {
                rb.velocity -= vecGravity * fallMultiplier;
            }
            //rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
    }

    //get the moving direction of the game object
    float GetMoveDir()
    {
        if (!health.Defend && !health.Dead)
        {
            if (gameObject.tag == "Player1" || gameObject.tag == "Player")
            {
                if (Input.GetKey("a"))
                {
                    //for P1
                    transform.localScale = new Vector3(-1, 1, 1);
                    return -1;
                }
                else if (Input.GetKey("d"))
                {
                    transform.localScale = new Vector3(1, 1, 1);
                    return 1;
                } 
            }
            else
            { 
                if (Input.GetKey("left"))
                {
                    //for P1
                    transform.localScale = new Vector3(-1, 1, 1);
                    return -1;
                }
                else if (Input.GetKey("right"))
                {
                    transform.localScale = new Vector3(1, 1, 1);
                    return 1;
                }
            }
        }
        return 0;
    }

    void Update()
    {
        anim.SetFloat("yVelocity", rb.velocity.y);
        //update player's input every frame
        move = new Vector2(GetMoveDir(), gameObject.transform.position.y);
        if (move.x != 0)
        {
            moving = true;
            anim.SetInteger("Speed", 1);
        }
        else
        {
            moving = false;
            anim.SetInteger("Speed", 0);
        }
    }

    void GroundCheck()
    {
        isGround = false;
        if (Physics2D.OverlapCircle(groundCheck.position, radius, ground) != null)
        {
            isGround = true;
        }
        anim.SetBool("Jump", !isGround);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, radius);
    }

    #endregion

    #region Properties

    public bool Moving
    {
        get { return moving; }
    }

    #endregion
}
