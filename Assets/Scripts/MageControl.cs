using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageControl : Control
{
    [SerializeField]
    int maxFireBalls;

    [SerializeField]
    GameObject FireBall;

    [SerializeField]
    Transform fireBallSpawn;

    int FireBallSpam;

    bool canShoot;

    // Start is called before the first frame update
    void Start()
    {
        anim = GFX.GetComponent<Animator>();
        move = gameObject.GetComponent<MoveCharacter>();
        health = gameObject.GetComponent<Health>();
        sr = GFX.GetComponent<SpriteRenderer>();
        FireBallSpam = maxFireBalls;
        canAttack = true;
        canShoot = true;
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
                    
                }
                else if (Input.GetKeyDown("j") && FireBallSpam > 0)
                {
                    SpecialSkill();
                    FireBallSpam--;
                }
            }
        }
        else if (gameObject.tag == "Player2")
        {
            if (!health.Dead)
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
                }
                else if (canShoot && Input.GetKeyDown(";") && FireBallSpam > 0)
                {
                    SpecialSkill();
                    FireBallSpam--;
                }
                if (FireBallSpam == 0)
                {
                    canShoot = false;
                }
            }
        }
    }
    protected override void SpecialSkill()
    {
        anim.SetTrigger("Special");
        ThrowFireBall();
    }

    protected override void DoAttack2()
    {
        anim.SetTrigger("Attack2");
        Collider2D hit;
        hit = Physics2D.OverlapCircle(attackPointMid.position, attackRangeMid, enemyLayer);
        if (hit != null)
        {
            hit.GetComponent<Health>().TakeDamage(damage);
            //recharge fire balls if dealing damage with melee attack
            FireBallSpam = maxFireBalls;
        }
    }
    void ThrowFireBall()
    {
        Instantiate(FireBall, fireBallSpawn.position, Quaternion.identity);
    }

    void OnDrawGizmos()
    {
        if (fireBallSpawn == null || attackPointHead == null || attackPointMid == null) return;

        Gizmos.DrawWireSphere(attackPointMid.position, attackRangeMid);
        Gizmos.DrawWireSphere(attackPointHead.position, attackRangeHead);
        Gizmos.DrawWireSphere(fireBallSpawn.position, 0.1f);
    }
}
