using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour
{
    // Start is called before the first frame update
    #region Fields

    Animator anim;

    [SerializeField]
    Transform attackPointMidRight;

    [SerializeField]
    Transform attackPointMidLeft;

    [SerializeField]
    Transform attackPointHeadRight;

    [SerializeField]
    Transform attackPointHeadLeft;

    [SerializeField]
    float attackRangeMid = 0.5f;

    [SerializeField]
    float attackRangeHead = 0.5f;

    [SerializeField]
    LayerMask enemyLayer;

    [SerializeField]
    float damage = 20f;

    bool defend = false;

    Health health;

    MoveCharacter move;

    Timer coolDownTimer;

    SpriteRenderer sr;

    const float attackCoolDownDuration = 0.5f;
    #endregion

    #region Methods

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        move = gameObject.GetComponent<MoveCharacter>();
        health = gameObject.GetComponent<Health>();
        coolDownTimer = gameObject.AddComponent<Timer>();
        sr = gameObject.GetComponent<SpriteRenderer>();
        coolDownTimer.Duration = 0.5f;
        coolDownTimer.Finish();
    }

    // Update is called once per frame
    void Update()
    {
        if (coolDownTimer.Finished)
        {
            Debug.Log("Cooldown has finished");
        }
        if (gameObject.tag == "Player1")
        {
            if (!health.Dead)
            {
                if (Input.GetKeyDown("g") && coolDownTimer.Finished)
                {
                    DoAttack1();
                    coolDownTimer.Restart(attackCoolDownDuration);
                }
                else if (Input.GetKeyDown("h"))
                {
                    DoAttack2();
                }
                //saved later for ranged attack
            }
        } else if (gameObject.tag == "Player2") {
            if(!health.Dead)
            {
                if (Input.GetKeyDown("j") && coolDownTimer.Finished)
                {
                    DoAttack1();
                    coolDownTimer.Restart(attackCoolDownDuration);
                }
                else if (Input.GetKeyDown("k"))
                {
                    DoAttack2();
                }
                //saved later for ranged attack
            }
        }
    }

    void DoAttack1()
    {
        anim.SetTrigger("Attack1");
        Collider2D hit;
        if (sr.flipX)
        {
            hit = Physics2D.OverlapCircle(attackPointHeadLeft.position, attackRangeHead, enemyLayer);
        } else
        {
            hit = Physics2D.OverlapCircle(attackPointHeadRight.position, attackRangeHead, enemyLayer);
        }
        if (hit != null)
        {
            hit.GetComponent<Health>().TakeDamage(2*damage);
        }
    }

    

    void DoAttack2()
    {
        anim.SetTrigger("Attack2");
        Collider2D hit;
        if (sr.flipX)
        {
            hit = Physics2D.OverlapCircle(attackPointMidLeft.position, attackRangeMid, enemyLayer);
        }
        else
        {
            hit = Physics2D.OverlapCircle(attackPointMidRight.position, attackRangeMid, enemyLayer);
        }
        if (hit != null)
        {
            hit.GetComponent<Health>().TakeDamage(damage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPointHeadRight == null || attackPointMidRight == null || attackPointHeadLeft == null || attackPointMidLeft == null) return;

        Gizmos.DrawWireSphere(attackPointMidLeft.position, attackRangeMid);
        Gizmos.DrawWireSphere(attackPointHeadLeft.position, attackRangeHead);

        Gizmos.DrawWireSphere(attackPointMidRight.position, attackRangeMid);
        Gizmos.DrawWireSphere(attackPointHeadRight.position, attackRangeHead);
    }

    #endregion

    #region Properties

    public bool IsDefend
    {
        get { return defend; }
    }

    public LayerMask EnemyLayer
    {
        set { enemyLayer = value; }
    }

    #endregion

}
