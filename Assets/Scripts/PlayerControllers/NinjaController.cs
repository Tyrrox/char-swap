using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaController : PlayerController
{
    // NINJA CHARACTER \\
    /* CHANGES:
     * PLAYER JUMP HEIGHT REDUCED
     * CAN DOUBLE JUMP
     * SLIGHTLY INCREASED SPEED
     */

    [SerializeField] private ParticleSystem jumpEffect;
    private bool canDoubleJump;

    // Adjust player values for ninja character
    protected override void Start()
    {
        base.Start();
        jumpStrength = 14.5f;
        playerSpeed = 8f;
    }

    protected override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.Space) && !grounded && canDoubleJump)
        {
            canDoubleJump = false;
            jumpEffect.Play();
            Jump();
        }
        if (!canDoubleJump && grounded)
        {
            canDoubleJump = true;
        }
    }

    public override IEnumerator TransformCharacterIn()
    {
        yield return StartCoroutine(base.TransformCharacterIn());
        canDoubleJump = false;
    }
}
