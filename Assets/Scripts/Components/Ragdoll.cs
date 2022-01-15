using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Ragdoll : MonoBehaviour
{
    private List<Rigidbody> ragdoll;

    public void ConstructRagdoll()
    {
        ragdoll = GetComponentsInChildren<Rigidbody>().ToList();
        SetSpecsOfRagdoll();
        DisableRagdoll();
    }
    public void SetSpecsOfRagdoll()
    {
        ragdoll.ForEach(x => 
        {
            x.gameObject.layer = 3;
            x.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        });
    }
    public void ActiveRagdoll()
    {
        ragdoll.ForEach(x => 
        {
            x.isKinematic = false;
            x.GetComponent<Collider>().isTrigger = false;
        });
    }
    public void DisableRagdoll()
    {
        ragdoll.ForEach(x =>
        {
            x.isKinematic = true;
            x.GetComponent<Collider>().isTrigger = true;
        });
    }
}
