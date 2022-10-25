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

    private void Update()
    {
        
    }

    private void FireArrow()
    {
        // Get direction the shooter is facing via scale, scale 1 = looking right
        float zRotation = -90 * transform.localScale.x;

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
