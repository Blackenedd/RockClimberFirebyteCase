using UnityEngine;
using System.Collections.Generic;

public class Hand : MonoBehaviour
{
    private Player player;

    private Rigidbody rock;
    private ConfigurableJoint cj;

    private bool connected = false;

    private void Start()
    {
        player = GetComponentInParent<Player>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Rock")) Connect(other.GetComponent<Rock>());
    }
    public void Connect(Rock _rock)
    {
        _rock.GotConnect();
        rock = _rock.GetComponent<Rigidbody>();

        cj = gameObject.AddComponent<ConfigurableJoint>();

        cj.connectedBody = rock;

        cj.xMotion = cj.yMotion = cj.zMotion = cj.angularXMotion = cj.angularYMotion = cj.angularZMotion = ConfigurableJointMotion.Locked;

        cj.autoConfigureConnectedAnchor = false;
        cj.connectedAnchor = Vector3.zero;
        cj.anchor = Vector3.zero;

        player.connectEvent.Invoke(_rock);
        connected = true;
    }
    public void Disable()
    {
        GetComponent<Collider>().enabled = false;

        if (cj != null) 
        {
            Destroy(cj);
            Destroy(GetComponent<Rigidbody>());
        }
        
        connected = false;
        rock = null;
    }
    public void Release()
    {
        if (cj != null)
        {
            Destroy(cj);
            Destroy(GetComponent<Rigidbody>());
        }

        connected = false;
        rock = null;
    }
}
