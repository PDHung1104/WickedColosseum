using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    // Start is called before the first frame update
    #region Fields

    [SerializeField]
    GameObject GFX;

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

    [SerializeField]
    Vector2 posP1 = new Vector2(450, -56), posP2 = new Vector2(1500, -56);

    [SerializeField]
    float deadDuration = 2f;

    bool defending;

    #endregion

    #region Methods

    void Start()
    {
        anim = GFX.GetComponent<Animator>();
        dead = false;
        control = GFX.GetComponent<Control>();
        hurt = false;

        healthBar.SetMaxHealth(health);
        if (gameObject.layer == 7)
        {
            pos.anchoredPosition = posP1;
        }
        else if (gameObject.layer == 8)
        {
            pos.anchoredPosition = posP2;
        }
        defending = false;
    }

    public void TakeDamage(float dmg)

    {
        if (!defending && !dead)
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
                Destroy(gameObject, deadDuration);
                return;
            }
            StartCoroutine(Hurting(0.2f));
        }
    }

    private IEnumerator Hurting(float duration) {
        yield return new WaitForSeconds(duration);
        hurt = false;
    }
    private void Die()
    {
        anim.SetTrigger("Dead");
        Destroy(gameObject, 2f);
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

    public bool Defend
    {
        set { defending = value; }
        get { return defending; }
    }

    #endregion
}
