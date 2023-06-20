using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour
{
    // Start is called before the first frame update
    #region Fields

    Animator anim;

    [SerializeField]
    Transform attackPointMid;

    [SerializeField]
    Transform attackPointHead;

    [SerializeField]
    float attackRangeMid = 0.5f;

    [SerializeField]
    float attackRangeHead = 0.5f;

    [SerializeField]
    LayerMask enemyLayer;

    [SerializeField]
    int damage = 20;

    bool defend = false;

    Health health;

    MoveCharacter move;

    Timer coolDownTimer;
    #endregion

    #region Methods

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        move = gameObject.GetComponent<MoveCharacter>();
        health = gameObject.GetComponent<Health>();
        coolDownTimer = gameObject.AddComponent<Timer>();
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
                    coolDownTimer.Restart();
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
                    coolDownTimer.Restart();
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
        Collider2D hit = Physics2D.OverlapCircle(attackPointHead.position, attackRangeHead, enemyLayer);
        if (hit != null)
        {
            hit.GetComponent<Health>().TakeDamage(2*damage);
        }
    }

    

    void DoAttack2()
    {
        anim.SetTrigger("Attack2");
        Collider2D hit = Physics2D.OverlapCircle(attackPointMid.position, attackRangeMid, enemyLayer);
        if (hit != null)
        {
            hit.GetComponent<Health>().TakeDamage(damage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPointHead == null || attackPointMid == null) return;

        Gizmos.DrawWireSphere(attackPointMid.position, attackRangeMid);
        Gizmos.DrawWireSphere(attackPointHead.position, attackRangeHead);
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
