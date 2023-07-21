using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEditor;
using Unity.VisualScripting;

public class EnemyMovement : MonoBehaviour
{
    #region Fields

    [SerializeField]
    GameObject GFX;

    Transform currentPoint;

    [SerializeField]
    Transform ground;

    [SerializeField]
    LayerMask groundLayer;

    [SerializeField]
    Transform approach;

    Collider2D approachEnemy;

    EnemyWandering patrolMovement;

    [SerializeField]
    LayerMask enemyLayer;

    Transform target;

    [SerializeField]
    float approachRange = 1f;

    AIPath pathfinder;
    Seeker seeker;
    AIDestinationSetter destinationSetter;

    Animator anim;
    Health health;
    bool isGround;
    #endregion

    #region Methods
    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        destinationSetter = GetComponent<AIDestinationSetter>();
        patrolMovement = GetComponent<EnemyWandering>();
        anim = GFX.GetComponent<Animator>();
        pathfinder = GetComponent<AIPath>();
        health = GetComponent<Health>();
        isGround = false;
    }

    // Update is called once per frame
    void Update()
    {
        GroundCheck();
        approachEnemy = Physics2D.OverlapCircle(approach.position, approachRange, enemyLayer);
        if (!health.Dead)
        {
            if (approachEnemy == null && isGround)
            {
                patrolMovement.Patrol = true;
            }
            else if (GameObject.FindWithTag("Player1") != null){
                patrolMovement.Patrol = false;
                pathfinder.destination = new Vector3(GameObject.FindWithTag("Player1").transform.position.x, transform.position.y, GameObject.FindWithTag("Player1").transform.position.z);
                if (pathfinder.velocity.x > 0.1f)
                {
                    transform.localScale = new Vector3(1, 1, 1);
                    Debug.Log("Going to the right");
                    anim.SetInteger("Speed", 1);
                }
                else if (pathfinder.velocity.x < 0f)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                    Debug.Log("Going to the left");
                    anim.SetInteger("Speed", 1);
                }
                else
                {
                    anim.SetInteger("Speed", 0);
                }
                if (pathfinder.velocity.y < 0f)
                {
                    anim.SetFloat("yVelocity", pathfinder.velocity.y);
                }
            }
                
        } else
        {
            pathfinder.canMove = false;
        }
    }

    void GroundCheck()
    {
        isGround = false;
        if (Physics2D.OverlapCircle(ground.position, 0.2f, groundLayer) != null)
        {
            isGround = true;
        }
        anim.SetBool("Jump", !isGround);
    }

    private void OnDrawGizmosSelected()
    {
        if (approach == null) return;
        Gizmos.DrawWireSphere(approach.position, approachRange);
        Gizmos.DrawWireSphere(ground.position, 0.2f);
    }

    #endregion
}
