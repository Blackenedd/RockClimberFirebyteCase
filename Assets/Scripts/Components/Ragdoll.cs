using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Ragdoll : MonoBehaviour
{
    private List<Rigidbody> rigidbodies;

    public void Construct()
    {
        rigidbodies = GetComponentsInChildren<Rigidbody>().Where(x => x.gameObject != this.gameObject).ToList();
        SetSpecs();
    }
    public void SetSpecs()
    {
        rigidbodies.ForEach(x => 
        {
            x.gameObject.layer = 3;
            x.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        });
    }
    public void Active()
    {
        rigidbodies.ForEach(x => 
        {
            x.isKinematic = false;
            x.GetComponent<Collider>().isTrigger = false;
        });
    }
    public void Disable()
    {
        rigidbodies.ForEach(x =>
        {
            x.isKinematic = true;
            x.GetComponent<Collider>().isTrigger = true;
        });
    }
}
