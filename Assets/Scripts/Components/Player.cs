using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Ragdoll ragdoll;

    private void Awake()
    {
        ragdoll.ConstructRagdoll();
    }
}
