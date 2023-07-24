using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour
{
    // Start is called before the first frame update
    #region Fields
    [SerializeField]
    protected GameObject GFX;

    protected Animator anim;

    [SerializeField]
    protected Transform attackPointMid;

    [SerializeField]
    protected Transform attackPointHead;

    [SerializeField]
    protected float attackRangeMid = 0.5f;

    [SerializeField]
    protected float attackRangeHead = 0.5f;

    [SerializeField]
    protected LayerMask enemyLayer;

    protected float pushForce;

    [SerializeField]
    protected float damage = 20f;

    protected bool defend = false;

    protected Health health;

    protected MoveCharacter move;

    protected SpriteRenderer sr;

    protected const float attackCoolDownDuration = 0.5f;

    protected bool canAttack, canStun;
    #endregion

    #region Methods

    void Start()
    {
        pushForce = 10f;
        anim = GFX.GetComponent<Animator>();
        move = gameObject.GetComponent<MoveCharacter>();
        health = gameObject.GetComponent<Health>();
        sr = GFX.GetComponent<SpriteRenderer>();
        canAttack = true;
        canStun = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.tag == "Player1" || gameObject.tag == "Player")
        {
            if (!health.Dead)
            {
                if (Input.GetKeyDown("g") && canAttack)
                {
                    DoAttack1();
                    canAttack = false;
                    StartCoroutine(CoolDown());
                }
                else if (Input.GetKeyDown("h"))
                {
                    DoAttack2();
                    canStun = false;
                    StartCoroutine(CoolDownStun());
                }
                if (Input.GetKey("j"))
                {
                    SpecialSkill();
                    
                } else
                {
                    health.Defend = false;
                }
                //saved later for ranged attack
            }
        } else if (gameObject.tag == "Player2") {
            if(!health.Dead)
            {
                if (Input.GetKeyDown("k") && canAttack)
                {
                    DoAttack1();
                    canAttack = false;
                    StartCoroutine(CoolDown());
                }
                else if (Input.GetKeyDown("l"))
                {
                    DoAttack2();
                    canStun = false;
                    StartCoroutine(CoolDownStun());
                }
                if (Input.GetKey(";"))
                {
                    SpecialSkill();
                }
                else
                {
                    health.Defend = false;
                }
                //saved later for ranged attack
            }
        }
    }

    protected IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(0.5f);
        canAttack = true;
    }

    protected IEnumerator CoolDownStun()
    {
        yield return new WaitForSeconds(1.5f);
        canStun = true;
    }

    protected void DoAttack1()
    {
        anim.SetTrigger("Attack1");
        Collider2D hit;
        hit = Physics2D.OverlapCircle(attackPointHead.position, attackRangeHead, enemyLayer);
        if (hit != null)
        {
            hit.GetComponent<Health>().TakeDamage(2*damage);
        }
    }

    

    protected virtual void DoAttack2()
    {
        anim.SetTrigger("Attack2");
        Collider2D hit;
        hit = Physics2D.OverlapCircle(attackPointMid.position, attackRangeMid, enemyLayer);
        if (hit != null)
        {
            hit.GetComponent<Health>().Stun();
        }
    }

    protected virtual void SpecialSkill()
    {
        anim.SetTrigger("Special");
        health.Defend = true;   
    }

    void OnDrawGizmosSelected()
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
