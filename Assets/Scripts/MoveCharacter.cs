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
    float speed = 90f;

    Rigidbody2D rb;

    Vector2 move;

    Animator anim;

    SpriteRenderer sr;

    Health health;

    bool moving;

    #endregion

    #region Methods

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        anim = GFX.GetComponent<Animator>();
        sr = GFX.GetComponent<SpriteRenderer>();
        moving = false;
        health = gameObject.GetComponent<Health>();
    }
    void FixedUpdate()
    {  
        //move the character around
       
        rb.velocity = new Vector2(move.x * speed * Time.deltaTime, move.y);
        //mirror-flip the character if moving to the left of the screen
        //only for player 1 (the one on the left)
        
    }

    //get the moving direction of the game object
    float GetMoveDir()
    {
        if (!health.Dead)
        {
            if (gameObject.tag == "Player1")
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

    #endregion

    #region Properties

    public bool Moving
    {
        get { return moving; }
    }

    #endregion
}
