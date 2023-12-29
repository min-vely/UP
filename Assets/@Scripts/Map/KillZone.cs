using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            PlayerController playerController = col.gameObject.GetComponentInChildren<PlayerController>();

            // KillZone에 닿았을 때
            if (playerController != null)
            {
                playerController.LoadCheckPoint();
            }
        }
    }
}
