using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ArcherController : PlayerController
{
    // ARCHER CHARACTER \\
    /* CHANGES:
     * PLAYER SPEED BASE
     * JUMP HEIGHT BASE
     * CAN FIRE ARROWS
     */

    [SerializeField] private GameObject arrow;
    [SerializeField] private GameObject firingPosition;

    private float firingAnimTime = 0.1f;

    private bool isFiring = false;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void ActivateAbility()
    {
        base.ActivateAbility();
        if (!isFiring)
        {
            isFiring = true;
            StartCoroutine(FireArrow());
        }
    }

    private IEnumerator FireArrow()
    {
        // Start arrow firing animation

        yield return new WaitForSeconds(firingAnimTime);

        // Instantiate arrow at firing point, prefab faces up 
        float direction = facingRight ? transform.right.x : -transform.right.x;
        float rotation = -90 * direction;

        Instantiate(arrow, firingPosition.transform.position, Quaternion.Euler(0, 0, rotation));

        isFiring = false;
    }


}
