using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update

    //enemy's animator controller
    Animator anim;

    //health stats of enemy
    int maxHealth = 100;
    int health;

    void Start()
    {
        health = maxHealth;
        anim = gameObject.GetComponent<Animator>();
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
    }

}
