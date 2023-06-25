using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyMovement : MonoBehaviour
{
    AIPath pathfinder;
    Animator anim;
    Health health;
    SpriteRenderer sr;
    AIDestinationSetter dest;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        pathfinder = GetComponent<AIPath>();
        health = GetComponent<Health>();
        sr = GetComponent<SpriteRenderer>();
        dest = GetComponent<AIDestinationSetter>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!health.Dead)
        {
            if (dest.target.position.y > gameObject.transform.position.y)
            {
                pathfinder.canMove = false;
            } else { pathfinder.canMove = true; }

            if (pathfinder.velocity.x >= 1)
            {
                Debug.Log("Going to the right");
                anim.SetInteger("Speed", 1);
            } else if (pathfinder.velocity.x <= -1)
            {
                sr.flipX = true;
                Debug.Log("Going to the left");
                anim.SetInteger("Speed", 1);
            }
            else
            {
                anim.SetInteger("Speed", 0);
            }
        } else
        {
            pathfinder.canMove = false;
        }
    }
}
