using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowEnemyBehaviour : MonoBehaviour
{
    // Arrow prefab always faces up, keep in mind when changing velocity.

    private Rigidbody2D rb;
    private BoxCollider2D coll;

    private float arrowSpeed = 9f;
    private float arrowLifetime = 5f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        SetStartVelocity();
        StartCoroutine(DestroyAfterLifetime());

    }

    private void Update()
    {
        
    }

    private void SetStartVelocity()
    {
        rb.velocity = transform.up * arrowSpeed;
    }

    private IEnumerator DestroyAfterLifetime()
    {
        yield return new WaitForSeconds(arrowLifetime);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().Die();
        }
        Destroy(gameObject);
    }
}
