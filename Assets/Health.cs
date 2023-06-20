using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    // Start is called before the first frame update
    #region Fields

    int maxHealth = 100;

    Animator anim;

    int health;

    bool dead;

    int dmgTakeDef = 1;

    Control control;

    #endregion

    #region Methods

    void Start()
    {
        health = maxHealth;
        anim = gameObject.GetComponent<Animator>();
        dead = false;
        control = gameObject.GetComponent<Control>();
    }

    void Update()
    {
        if (control != null && control.IsDefend)
        {
            dmgTakeDef = 0;
        }
        dmgTakeDef = 1;
    }

    public void TakeDamage(int dmg)
    {
        if (!dead)
        {
            health -= dmgTakeDef * dmg;
            anim.SetTrigger("Dmg");
            if (health < 0)
            {
                health = 0;
                dead = true;
                Die();
            }
        }
    }

    public void Die()
    {
        anim.SetTrigger("Dead");
    }

    #endregion

    #region Properties

    public bool Dead
    {
        get { return dead; }
    }

    #endregion
}
