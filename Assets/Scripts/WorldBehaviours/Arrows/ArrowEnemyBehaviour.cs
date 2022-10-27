using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowEnemyBehaviour : ArrowBehaviour
{
    // Arrow Enemy
    /* Base Speed
     * Base Lifetime
     * Kills Players
     */

    protected override void Start()
    {
        base.Start();
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
