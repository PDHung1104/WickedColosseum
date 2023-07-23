using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

enum Direction
{
    right,
    left
}
public class EnemyWandering : MonoBehaviour
{
    // Start is called before the first frame update
    #region Fields

    Animator anim;

    [SerializeField]
    GameObject GFX;

    [SerializeField]
    LayerMask playZone;

    [SerializeField]
    float wanderRange = 0.5f;

    Transform temp;

    AIPath pathfinder;

    Health health;

    bool isPatrolling;

    #endregion

    #region Methods
    void Start()
    {
        health = GetComponent<Health>();
        pathfinder = GetComponent<AIPath>();
        isPatrolling = true;
        anim = GFX.GetComponent<Animator>();
        pathfinder.maxSpeed = 0.7f;
        pathfinder.destination = new Vector3(transform.position.x + wanderRange, transform.position.y, transform.position.z);
        anim.SetInteger("Speed", 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (isPatrolling)
        {
            pathfinder.maxSpeed = 0.9f;
           
            if (Vector3.Distance(transform.position, pathfinder.destination) < 0.4f)
            {
                pathfinder.destination = new Vector3(transform.position.x + wanderRange, transform.position.y, transform.position.z);
                wanderRange *= -1;
            }
            if (pathfinder.velocity.x < 0)
            {
                Vector3 localScale = transform.localScale;
                localScale.x = -1f;
                transform.localScale = localScale;
            } else
            {
                Vector3 localScale = transform.localScale;
                localScale.x = 1f;
                transform.localScale = localScale;
            }
            anim.SetInteger("Speed", 1);
            
        }
        else
        {
            pathfinder.maxSpeed = 1.4f;
        }

    }

    public bool OnSameSidePlayer()
    {
        if ((GameObject.FindGameObjectWithTag("Player").transform.position.x < transform.position.x && transform.localScale.x == -1) || (GameObject.FindGameObjectWithTag("Player").transform.position.x > transform.position.x && transform.localScale.x == 1)) {
            return true;
        }
        return false;
    }
    #endregion

    #region Properties
    public bool Patrol
    {
        set { isPatrolling = value; }
    }
    #endregion

}
