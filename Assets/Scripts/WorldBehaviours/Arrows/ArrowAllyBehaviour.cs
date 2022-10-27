using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowAllyBehaviour : ArrowBehaviour
{
    // Arrow Ally
    /* Faster Speed
     * Longer Lifetime
     * Impacts Enemies (if created)
     * Impacts Unlocking Mechanisms
     */

    protected override void Start()
    {
        // Update values then start the object.
        arrowLifetime = 8f;
        arrowSpeed = 18f;
        base.Start();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Don't collide with the player, we start inside.
        if (collision.gameObject.tag != "Player")
        {
            // TODO: Functionality for unlocking doors or killing enemies.
            Destroy(gameObject);
        }
    }
}
