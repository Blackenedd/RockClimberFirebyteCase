using UnityEngine;
using System.Collections.Generic;

public class Hand : MonoBehaviour
{
    private Player player;

    private List<Transform> joints = new List<Transform>();
    private Transform rock;
    
    private Vector3 originalLocalPosition;

    private bool connected = false;

    private void Start()
    {
        player = GetComponentInParent<Player>();
        joints.Add(transform.parent);
        joints.Add(transform.parent.parent);
        originalLocalPosition = transform.localPosition;
    }
    private void LateUpdate()
    {
        if (joints.Count != 0 && connected)
        {
            for(int i = 1; i < joints.Count + 1; i++)
            {
                joints[i - 1].position = rock.position + Vector3.down * i * 0.1f;
                joints[i - 1].rotation = Quaternion.Euler(90, 0, 0);
            }    
        }
        if(rock != null)
        {
            transform.position = rock.position;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Rock")) Connect(other.GetComponent<Rock>());
    }
    public void Connect(Rock _rock)
    {
        _rock.GotConnect();

        rock = _rock.transform;

        joints.ForEach(x => x.GetComponent<Rigidbody>().isKinematic = true);

        player.connectEvent.Invoke();

        connected = true;
    }
    public void Disable()
    {
        GetComponent<Collider>().enabled = false;
        connected = false;
        transform.localPosition = originalLocalPosition;
        joints.ForEach(x => x.GetComponent<Rigidbody>().isKinematic = false);
        rock = null;
    }
    public void Release()
    {
        connected = false;
        joints.ForEach(x => x.GetComponent<Rigidbody>().isKinematic = false);
        transform.localPosition = originalLocalPosition;
        rock = null;
    }
}
