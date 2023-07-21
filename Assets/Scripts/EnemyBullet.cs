using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    #region Fields
    // Start is called before the first frame update
    GameObject player;
    Rigidbody2D rb;

    [SerializeField]
    float force = 2f;

    [SerializeField]
    LayerMask enemyLayer;

    [SerializeField]
    float damage = 20f;

    #endregion

    #region Methods
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player1");

        Vector3 direction = player.transform.position - transform.position;
        rb.velocity = new Vector2(direction.x, 0).normalized * force;
        float rot = Mathf.Atan2(0, -direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Player1")
        {
            other.gameObject.GetComponent<Health> ().TakeDamage(damage);
        }
        Destroy(gameObject);
    }
    #endregion
}
