using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    List<int> Attack;
    
    Collider2D hit;
    
    Animator anim;
    
    SpriteRenderer sr;

    [SerializeField]
    Transform attackPointMidLeft, attackPointMidRight;

    [SerializeField]
    float attackRangeMid = 0.5f;

    [SerializeField]
    LayerMask enemyLayer;

    [SerializeField]
    int damage = 20;

    Timer timer;

    Health health;

    Timer coolDownTimer;
    
    const float attackCoolDownDuration = 0.5f;

    void Start()
    {
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        timer = gameObject.AddComponent<Timer>();
        timer.Duration = Random.Range(1, 3);
        coolDownTimer = gameObject.AddComponent<Timer>();
        coolDownTimer.Duration = attackCoolDownDuration;
        health = GetComponent<Health>();
        timer.Run();
        timer.Finish();
        coolDownTimer.Run();
        coolDownTimer.Finish();
    }

    void DoAttack()
    {
        if (coolDownTimer.Finished && !health.Hurt)
        {
            anim.SetTrigger("Attack");
            hit.GetComponent<Health>().TakeDamage(2 * damage);
            coolDownTimer.Restart(attackCoolDownDuration);
        }
    }

    // Update is called once per frame
    void Update()
    {
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
            if (timer.Finished)
            {
                DoAttack();
                timer.Restart(Random.Range(1, 3));
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        if (attackPointMidRight == null || attackPointMidLeft == null) return;

        Gizmos.DrawWireSphere(attackPointMidLeft.position, attackRangeMid);
        //Gizmos.DrawWireSphere(attackPointHeadLeft.position, attackRangeHead);

        Gizmos.DrawWireSphere(attackPointMidRight.position, attackRangeMid);
        //Gizmos.DrawWireSphere(attackPointHeadRight.position, attackRangeHead);
    }
}
