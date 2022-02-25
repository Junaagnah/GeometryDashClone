using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Checking if collider is the player
        if (other.gameObject.CompareTag("Player"))
        {
            // Kill the player
            other.gameObject.GetComponent<Player>().KillPlayer();
        }
    }
}
