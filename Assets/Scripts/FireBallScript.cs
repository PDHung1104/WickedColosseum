using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FireBallScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    float damage = 20f;

    Rigidbody2D rb;

    [SerializeField]
    float force = 30f;

    Vector3 localScale;

    void Start()
    {
        transform.localScale = localScale; 
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = localScale.x * transform.right * force;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<Health>() != null)
        {
            other.gameObject.GetComponent<Health>().TakeDamage(damage);
        }
        Destroy(gameObject);
    }

    public Vector3 LocalScale
    {
        set { localScale = value; }
    }
}
