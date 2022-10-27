using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ArrowShooterBehaviour : MonoBehaviour
{
    // Arrow to be fired
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private GameObject firingPoint;

    private float timeBetweenArrows = 2f;

    private void Start()
    {
        StartCoroutine(RepeatArrowFire());
    }

    private void FireArrow()
    {
        // Get direction the shooter is facing via scale, scale 1 = looking right
        float direction = (transform.localScale.x > 0) ? transform.right.x : -transform.right.x;
        float zRotation = -90 * direction;

        Instantiate(arrowPrefab, firingPoint.transform.position, Quaternion.Euler(0, 0, zRotation));
    }

    private IEnumerator RepeatArrowFire()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenArrows);
            FireArrow();
        }
    }
}
