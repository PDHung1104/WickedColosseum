using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    protected GameObject GFX;

    [SerializeField]
    protected List<int> Attack;

    protected Collider2D hit;

    protected Animator anim;

    protected SpriteRenderer sr;

    [SerializeField]
    protected Transform attackPointMid;

    [SerializeField]
    protected float attackRangeMid = 0.5f;

    [SerializeField]
    protected LayerMask enemyLayer;

    [SerializeField]
    protected float damage = 20f;

    protected Timer timer;

    protected Health health;

    protected Timer coolDownTimer;
    
    protected const float attackCoolDownDuration = 0.5f;

    void Start()
    {
        anim = GFX.GetComponent<Animator>();
        sr = GFX.GetComponent<SpriteRenderer>();
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

    protected virtual void DoAttack()
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
        hit = Physics2D.OverlapCircle(attackPointMid.position, attackRangeMid, enemyLayer);
        if (hit != null)
        {
            if (timer.Finished)
            {
                DoAttack();
                timer.Restart(Random.Range(1, 3));
            }
        }
    }
    protected void OnDrawGizmosSelected()
    {
        if (attackPointMid == null) return;

        Gizmos.DrawWireSphere(attackPointMid.position, attackRangeMid);
      
    }
}
