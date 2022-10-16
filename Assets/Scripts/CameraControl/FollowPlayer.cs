using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class FollowPlayer : MonoBehaviour
{
    private GameObject player;
    private Transform followTarget;
    private CinemachineVirtualCamera vcam;
    
    void Start()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
    }

    void Update()
    {
        // Change follow target if current follow target ceases to exist or becomes inactive.
        if (player == null || !player.activeInHierarchy)
        {
            player = GameObject.FindWithTag("Player");
            if (player != null && player.activeInHierarchy)
            {
                followTarget = player.transform;
                vcam.LookAt = followTarget;
                vcam.Follow = followTarget;
            }
        }
    }
}
