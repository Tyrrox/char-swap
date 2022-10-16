using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnTriggerBehaviour : MonoBehaviour
{
    Transform respawnPoint;

    private void Start()
    {
        // Get the child respawn point to pass to playerManager
        respawnPoint = transform.Find("RespawnPoint");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerManager.playerManager.UpdateRespawnLocation(respawnPoint);
        }
    }
}
