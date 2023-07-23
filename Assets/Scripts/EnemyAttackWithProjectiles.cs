using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAttackWithProjectiles : EnemyAttack
{
    [SerializeField]
    GameObject Knife;
    [SerializeField]
    Transform bulletPos;

    float shootTimer;

    // Start is called before the first frame update
    private void Start()
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
        shootTimer = 0f;
    }
    // Update is called once per frame
    void Update()
    {
        if (!health.Dead)
        {
            shootTimer += Time.deltaTime;
            hit = Physics2D.OverlapCircle(attackPointMid.position, attackRangeMid, enemyLayer);
            float distance = 10f;
            if (GameObject.FindGameObjectWithTag("Player") != null)
            {
                distance = Vector2.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position);
            }
            if (hit != null)
            {
                if (timer.Finished)
                {
                    DoAttack();
                    timer.Restart(Random.Range(1, 3));
                }
            }
            else
            {
                if (shootTimer >= 2f && distance <= 4f && GetComponent<EnemyWandering>().OnSameSidePlayer())
                {
                    shootTimer = 0f;
                    shoot();
                }
            }
        }
    }

    void shoot()
    {
        anim.SetTrigger("Shoot");
        Instantiate(Knife, attackPointMid.position, Quaternion.identity);
    }

    
}
