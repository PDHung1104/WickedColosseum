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

    bool stun;

    [SerializeField]

    HealthBar healthBar;

    [SerializeField]
    float maxHealth = 100f;

    float health;

    bool dead;

    [SerializeField]
    float armour = 1f;

    bool hurt;

    Control control;

    [SerializeField]
    int score;

    [SerializeField]
    RectTransform pos;

    [SerializeField]
    float deadDuration = 2f;

    bool defending;

    #endregion

    #region Methods

    void Start()
    {
        stun = false;
        anim = GFX.GetComponent<Animator>();
        dead = false;
        control = GFX.GetComponent<Control>();
        hurt = false;
        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        defending = false;
    }

    public void TakeDamage(float dmg)

    {
        if (!defending && !dead)
        {
            hurt = true;
            health -= dmg/armour;
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

    public void Stun()
    {
        anim.SetBool("Stun", true);
        stun = true;
        StartCoroutine(StunTimer());
    }

    IEnumerator StunTimer()
    {
        yield return new WaitForSeconds(2.5f);
        stun = false;
        anim.SetBool("Stun", false);
    }
    private IEnumerator Hurting(float duration) {
        yield return new WaitForSeconds(duration);
        hurt = false;
    }
    private void Die()
    {
        anim.SetTrigger("Dead");
        if (gameObject.layer != 7)
        {
            ScoreScript.score += score;
            if (ScoreScript.score > PlayerPrefs.GetInt("highScore", 0))
            {
                ScoreScript.SetHighScore();
            }
            if (GameObject.FindWithTag("Player") != null)
            {
                GameObject.FindWithTag("Player").GetComponent<Health>().AddHealth(50);
            }
        } else {
            if (ScoreScript.score > PlayerPrefs.GetInt("highScore", 0))
            {
                ScoreScript.SetHighScore();
            }
        }
        Destroy(gameObject, 2f);
    }

    private void AddHealth(float health)
    {
        this.health += health;
        if (this.health > maxHealth)
        {
            this.health = maxHealth;
        }
        healthBar.SetHealth(this.health);
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

    public bool Stunned
    {
        get { return stun; }
    }

    #endregion
}
