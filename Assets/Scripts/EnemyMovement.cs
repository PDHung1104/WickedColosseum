using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    GameObject GFX;

    [SerializeField]
    Transform approach;

    Collider2D approachEnemy;

    [SerializeField]
    LayerMask enemyLayer;

    [SerializeField]
    float approachRange = 1f;

    AIPath pathfinder;
    Animator anim;
    Health health;
    SpriteRenderer sr;
    AIDestinationSetter dest;
    // Start is called before the first frame update
    void Start()
    {
        anim = GFX.GetComponent<Animator>();
        pathfinder = GetComponent<AIPath>();
        health = GetComponent<Health>();
        sr = GFX.GetComponent<SpriteRenderer>();
        dest = GetComponent<AIDestinationSetter>();
    }

    // Update is called once per frame
    void Update()
    {
        approachEnemy = Physics2D.OverlapCircle(approach.position, approachRange, enemyLayer);
        if (!health.Dead)
        {
            if (approachEnemy == null)
            {
                pathfinder.canMove = false;
                anim.SetInteger("Speed", 0);
            }
            else {
                pathfinder.canMove = true;

                if (pathfinder.velocity.x > 0.1f)
                {
                    transform.localScale = new Vector3(1, 1, 1);
                    Debug.Log("Going to the right");
                    anim.SetInteger("Speed", 1);
                } else if (pathfinder.velocity.x < 0f)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                    Debug.Log("Going to the left");
                    anim.SetInteger("Speed", 1);
                } else
                {
                    anim.SetInteger("Speed", 0);
                }
            }
        } else
        {
            pathfinder.canMove = false;
        }
    }
    private void OnDrawGizmosSelected()
    {
        if (approach == null) return;
        Gizmos.DrawWireSphere(approach.position, approachRange);
    }
}
