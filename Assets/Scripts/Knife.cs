using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    float speed = 1f, damage = 15f, hitRange = 0.2f;

    [SerializeField]
    Transform Hitbox;

    [SerializeField]
    LayerMask enemyLayer, endOfField;

    [SerializeField]
    GameObject character;
    float localScaleX;

    void Start()
    {
        localScaleX = character.GetComponent<Transform>().localScale.x;
        transform.localScale = new Vector3(localScaleX, 0, 0);
    }
    // Update is called once per frame
    void Update()
    {
        
        transform.position += -transform.right * Time.deltaTime * speed;
        
        if (Physics2D.OverlapCircle(Hitbox.position, hitRange, enemyLayer) != null)
        {
            if (Physics2D.OverlapCircle(Hitbox.position, hitRange, enemyLayer).GetComponent<Health>() != null)
            {
                Physics2D.OverlapCircle(Hitbox.position, hitRange, enemyLayer).GetComponent<Health>().TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(Hitbox.position, hitRange);
    }
}
