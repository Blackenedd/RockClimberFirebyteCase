using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") || other.gameObject.layer == 3)
        {
            Level.instance.player.hitObstacleEvent.Invoke();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 3)
        {
            Level.instance.player.hitObstacleEvent.Invoke();
        }
    }
}
