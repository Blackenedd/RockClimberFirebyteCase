using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    public Transform playerFinishPosition;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) GameController.instance.FinishLevel(true);
    }
}
