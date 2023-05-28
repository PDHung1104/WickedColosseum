using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    // Start is called before the first frame update
    Animator anim;

    public Transform attackPointMid;
    public Transform attackPointHead;
    
    public float attackRangeMid = 0.5f;
    public float attackRangeHead = 0.5f;

    public LayerMask enemyLayer;

    public int damage = 20;

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("g"))
        {
            DoAttack1();
        } else if (Input.GetKeyDown("h")){
            DoAttack2();
        }
    }

    void DoAttack1()
    {
        anim.SetTrigger("Attack1");
        Collider2D hit = Physics2D.OverlapCircle(attackPointHead.position, attackRangeHead, enemyLayer);
        if (hit != null)
        {
            hit.GetComponent<Enemy>().takeDamage(2*damage);
        }
    }

    void DoAttack2()
    {
        anim.SetTrigger("Attack2");
        Collider2D hit = Physics2D.OverlapCircle(attackPointMid.position, attackRangeMid, enemyLayer);
        if (hit != null)
        {
            hit.GetComponent<Enemy>().takeDamage(damage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPointHead == null || attackPointMid == null) return;

        Gizmos.DrawWireSphere(attackPointMid.position, attackRangeMid);
        Gizmos.DrawWireSphere(attackPointHead.position, attackRangeHead);
    }
}
