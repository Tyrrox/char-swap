using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBehaviour : MonoBehaviour
{
    // Arrow prefab always faces up, keep in mind when changing velocity.

    protected Rigidbody2D rb;

    protected float arrowSpeed = 9f;
    protected float arrowLifetime = 5f;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        SetStartVelocity();
        StartCoroutine(DestroyAfterLifetime());
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
}
