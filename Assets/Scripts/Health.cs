using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    // Start is called before the first frame update
    #region Fields

    Animator anim;

    [SerializeField]

    HealthBar healthBar;

    [SerializeField]
    float health = 100f;

    bool dead;

    [SerializeField]
    float armour = 1f;

    bool hurt;

    Control control;


    [SerializeField]
    RectTransform pos;


    #endregion

    #region Methods

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        dead = false;
        control = gameObject.GetComponent<Control>();
        hurt = false;

        healthBar.SetMaxHealth(health);
        if (gameObject.layer == 7)
        {
            pos.anchoredPosition = new Vector2(180, -56);
        }
        else if (gameObject.layer == 8 || gameObject.layer == 6)
        {
            pos.anchoredPosition = new Vector2(1760, -56);
        }
    }

    public void TakeDamage(float dmg)

    {
        if (!dead)
        {
            hurt = true;
            health -= armour * dmg;
            healthBar.SetHealth(health);
            anim.SetTrigger("Dmg");
            if (health <= 0)

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
