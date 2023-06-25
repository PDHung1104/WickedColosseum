using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    // Start is called before the first frame update
    #region Fields

    Animator anim;

    HealthBar healthBar;

    [SerializeField]
    int health = 100;

    bool dead;

    int dmgTakeDef = 1;

    bool hurt;

    Control control;

    #endregion

    #region Methods

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        dead = false;
        control = gameObject.GetComponent<Control>();
        hurt = false;
        healthBar.SetMaxHealth(health);
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
            hurt = true;
            health -= dmgTakeDef * dmg;
            healthBar.SetHealth(health);
            anim.SetTrigger("Dmg");
            if (health < 0)
            {
                health = 0;
                dead = true;
                Die();
                return;
            }
            hurt = false;
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

    public bool Hurt
    {
        get { return hurt; }
    }

    #endregion
}
