using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//This script can be utilized for any playable characters

public class MoveCharacter : MonoBehaviour
{
    // Start is called before the first frame update

    // Update is called once per frame
    float speed = 90f;
    Rigidbody2D rb;
    Vector2 move;

    Animator anim;

    SpriteRenderer sr;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        sr = gameObject.GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        //rb.AddForce(move * speed * Time.deltaTime, ForceMode2D.Impulse);
        
        //move the character around
        anim.SetFloat("Speed", Mathf.Abs(move.x));
        rb.velocity = new Vector2(move.x * speed * Time.deltaTime, move.y);

        //mirror-flip the character if moving to the left of the screen
        if (move.x < 0)
        {
            sr.flipX = true;
        } else
        {
            sr.flipX = false;
        }
    }

    void Update()
    {
        //update player's input every frame
        move = new Vector2(Input.GetAxisRaw("Horizontal"), gameObject.transform.position.y);    
    }
}
