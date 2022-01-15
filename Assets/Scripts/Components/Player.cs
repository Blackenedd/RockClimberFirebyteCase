using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Ragdoll ragdoll;
    private Animations animations;

    private void Awake()
    {
        ragdoll = gameObject.AddComponent<Ragdoll>();
        animations = gameObject.AddComponent<Animations>();

        ragdoll.Construct(); 
        ragdoll.Disable();
        
        animations.Construct();
        animations.Idle();
    }
}
