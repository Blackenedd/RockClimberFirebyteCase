using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;
using UnityEngine.Events;

public class Ragdoll : MonoBehaviour
{
    private List<Rigidbody> rigidbodies;

    private Rigidbody hip;
    private ConfigurableJoint cjHip;

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
            x.interpolation = RigidbodyInterpolation.Interpolate;

            x.constraints = RigidbodyConstraints.FreezePositionZ;

            // converts to configurablejoint
            {
                //if(x.GetComponent<CharacterJoint>() != null)
                //{
                //    ConfigurableJoint cj = x.gameObject.AddComponent<ConfigurableJoint>();
                //    cj.connectedBody = x.gameObject.GetComponent<CharacterJoint>().connectedBody;

                //    cj.xMotion = cj.yMotion = cj.zMotion = ConfigurableJointMotion.Locked;
                //    cj.angularXMotion = cj.angularYMotion = cj.angularZMotion = ConfigurableJointMotion.Limited;

                //    Destroy(x.GetComponent<CharacterJoint>());
                //}
            }
        });
        Disable();
    }
    public void Enable()
    {
        rigidbodies.ForEach(x => 
        {
            x.isKinematic = false;
            x.GetComponent<Collider>().isTrigger = false;
        });
    }
    public void Disable(UnityAction onComplete = null)
    {
        rigidbodies.ForEach(x =>
        {
            x.isKinematic = true;
            x.GetComponent<Collider>().isTrigger = true;
        });

        onComplete?.Invoke();
    }
    public void LaunchRagdoll(Vector3 point)
    {
        Vector3 direction = (point - hip.position).normalized;

        direction.z = 0;

        rigidbodies.ForEach(x => x.AddForce(direction * GameController.instance.settings.launchingForce));
    }
    public void ConnectRagdoll(Rigidbody rb)
    {
        cjHip = hip.gameObject.AddComponent<ConfigurableJoint>();

        cjHip.connectedBody = rb;
        cjHip.xMotion = cjHip.yMotion = cjHip.zMotion = ConfigurableJointMotion.Limited;
        cjHip.angularXMotion = cjHip.angularYMotion = cjHip.angularZMotion = ConfigurableJointMotion.Locked;

        cjHip.autoConfigureConnectedAnchor = false;
        cjHip.connectedAnchor = Vector3.zero;
        cjHip.anchor = Vector3.zero;

        SoftJointLimit limit = new SoftJointLimit();
        limit.limit = 0.3f;

        cjHip.linearLimit = limit;
    }
    public void DisconnectRagdoll()
    {
        if (cjHip != null)
        {
            Destroy(cjHip);
        }
    }
}
