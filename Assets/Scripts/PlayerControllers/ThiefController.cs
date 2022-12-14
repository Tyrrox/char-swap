using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThiefController : PlayerController
{
    // THIEF CHARACTER \\
    /* CHANGES:
     * PLAYER SPEED REDUCED
     * CAN WALL CLIMB
     */

    private float climbingSpeed = 3f;
    private float baseGravityScale;

    // Overriding start method to change player speed
    protected override void Start()
    {
        base.Start();
        playerSpeed = 6f;
        baseGravityScale = rb.gravityScale;
    }

    // Overrided update method
    protected override void Update()
    {
        DetectWall();
        base.Update();
    }

    protected override void FixedUpdate()
    {
        // If the player is not climbing, move the player as normal, otherwise climb
        if (DetectWall() && Input.GetKey(KeyCode.LeftControl))
        {
            // disable gravity to climb, or hold still
            rb.gravityScale = 0f;
            
            // If not already playing the climbing animation, start the animation
            if (!animate.GetCurrentAnimatorStateInfo(0).IsName("Climbing"))
            {
                animate.SetBool("IsClimbing", true);
                animate.Play("Climbing");
            }

            ClimbPlayer();
        }
        else
        {
            animate.SetBool("IsClimbing", false);

            // enable gravity for moving, base movement
            rb.gravityScale = baseGravityScale;
            base.FixedUpdate();
        }
    }

    // Detect a wall in front of the player
    private bool DetectWall()
    {
        // get the current forward direction
        Vector2 currentDirection = facingRight ? transform.right : -transform.right;

        // Boxcast to hit a platform (including walls)
        float extraDistance = 0.1f;
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxColl.bounds.center, boxColl.bounds.size, 0f, currentDirection, extraDistance, platformLayerMask);

        // if the raycast hits a platform, check that the top of the platform is above the bottom of the player
        if (raycastHit.collider != null)
        {
            float collBottom = boxColl.bounds.center.y - boxColl.bounds.extents.y;
            float platTop = raycastHit.collider.bounds.center.y + raycastHit.collider.bounds.extents.y;

            if (collBottom < platTop)
            {
                return true;
            }
        }
        return false;
    }

    // Player climbs up or down the wall depending on movement input while holding climb button
    private void ClimbPlayer()
    {
        // Climb up if moving towards the wall, climb down if moving away from the wall
        float climbingInput = facingRight ? horizontalInput : -horizontalInput;
        rb.velocity = new Vector2(0f, climbingInput * climbingSpeed);

        // Play or reverse animation based on movement
        animate.SetFloat("ClimbingMultiplier", climbingInput);
    }
}
