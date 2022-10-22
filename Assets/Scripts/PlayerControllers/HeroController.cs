using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController : PlayerController
{
    // HERO CHARACTER \\
    /* CHANGES:
     * BASE STATS
     * SPRINT ABILITY
     */

    [SerializeField] private ParticleSystem sprintDust;
    private bool isSprinting = false;

    private float baseSpeed;
    private float sprintSpeed = 10f;

    protected override void Start()
    {
        base.Start();
        baseSpeed = playerSpeed;
    }

    protected override void ActivateAbility()
    {
        // Only allow sprinting to begin if the player is grounded.
        if (!isSprinting && IsGrounded())
        {
            base.ActivateAbility();
            isSprinting = true;
            playerSpeed = sprintSpeed;
            sprintDust.Play();
            StartCoroutine(StopSprintingOnKeyUp());
        }
    }

    private IEnumerator StopSprintingOnKeyUp()
    {
        // Stop sprinting when the player raises the sprint key.
        while (isSprinting && (Input.GetKey(KeyCode.LeftControl) || !IsGrounded()))
        {
            yield return null;
        }
        isSprinting = false;
        playerSpeed = baseSpeed;
        sprintDust.Stop();
    }

    public override IEnumerator TransformCharacterIn()
    {
        yield return StartCoroutine(base.TransformCharacterIn());
        // Disable sprinting when transforming back in.
        isSprinting = false;
    }
}
