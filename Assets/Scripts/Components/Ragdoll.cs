using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;

public class Ragdoll : MonoBehaviour
{
    private List<Rigidbody> rigidbodies;

    private Rigidbody hip;

    public void Construct()
    {
        rigidbodies = GetComponentsInChildren<Rigidbody>().Where(x => x.gameObject != this.gameObject).ToList();
        hip = rigidbodies[0];
        SetSpecs();
    }
    public void SetSpecs()
    {
        rigidbodies.ForEach(x => 
        {
            x.gameObject.layer = 3;
            x.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
            x.constraints = RigidbodyConstraints.FreezePositionZ;
        });
    }
    public void Enable()
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
    public void LaunchRagdoll(Vector3 point)
    {
        Vector3 direction = (point - hip.position).normalized;

        rigidbodies.ForEach(x => x.AddForce(direction * GameController.instance.settings.launchingForce));
    }
    public void LerpRagdoll(Vector3 point)
    {
        hip.isKinematic = true;
        hip.transform.DOMove(point, 0.7f).SetEase(Ease.OutQuad).OnComplete(() => 
        {
            hip.isKinematic = false;
        });
    }
}
