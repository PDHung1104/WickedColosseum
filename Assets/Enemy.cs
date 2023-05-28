using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update

    //enemy's animator controller
    Animator anim;

    Timer deathDelay;

    //health stats of enemy
    int maxHealth = 100;
    int health;

    void Start()
    {
        health = maxHealth;
        anim = gameObject.GetComponent<Animator>();
        deathDelay = gameObject.AddComponent<Timer>();
        deathDelay.Duration = 1f;
    }

    // Update is called once per frame

    public void takeDamage(int damage)
    {
        health -= damage;
        anim.SetTrigger("Dmg");
        if (health <= 0)
        {
            health = 0;
            die();
        }
    }

    public void die()
    {
        //play die animation
        anim.SetBool("Die", true);
        deathDelay.Run();
        if (!(anim.GetCurrentAnimatorStateInfo(0).length >
            anim.GetCurrentAnimatorStateInfo(0).normalizedTime))
        {
            Destroy(gameObject);
        }
    }

}
