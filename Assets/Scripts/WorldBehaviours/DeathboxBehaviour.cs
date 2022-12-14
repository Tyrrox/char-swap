using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathboxBehaviour : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerController cont = collision.GetComponent<PlayerController>();
            cont.Die();
        }
    }
}
